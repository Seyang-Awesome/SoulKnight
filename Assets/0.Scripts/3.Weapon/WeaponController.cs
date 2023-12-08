using System;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private int weaponUpLimit;
        [SerializeField] private Transform weaponRoot;
        [SerializeField] private GameObject blankWeaponPrefab;
        [SerializeField] private WeaponDefinitionBase testWeapon1;
        [SerializeField] private WeaponDefinitionBase testWeapon2;

        private GameInput input;
        private PlayerInfo info;
        private Rigidbody2D rb;
        private Vector2 weaponScale;
        private Vector2 inverseWeaponScale;
        private bool isLastFrameAttack;
        
        private Dictionary<int, Type> weaponTypeDic = new();
        private List<WeaponInGameBase> weaponInGames = new();
        private WeaponInGameBase currentWeapon;
        private int CurrentWeaponNum => weaponInGames.Count;
        private int CurrentWeaponIndex => weaponInGames.IndexOf(currentWeapon);
        private Vector2 AimAtDirection => weaponRoot.right * ScaleIndex;
        private float ScaleIndex => weaponRoot.localScale.x > 0 ? 1f : -1f;
        private int NextWeaponIndex
        {
            get
            {
                if (CurrentWeaponNum == 0) return -1;
                if (CurrentWeaponNum == 1) return CurrentWeaponIndex;
                return (CurrentWeaponIndex + 1) % CurrentWeaponNum;
            }
        }
        
        
        private void Start()
        {
            weaponTypeDic = new()
            {
                {11001,typeof(WeaponInGame_OneShoot)},
                {12001,typeof(WeaponInGame_Rifle)},
                {13001,typeof(WeaponInGame_Knife_ZeroConsume)},
                {14001,typeof(WeaponInGame_Laser)},
            };
            
            currentWeapon = null;
            input = GameInput.Instance;
            info = GetComponent<PlayerInfo>();
            rb = GetComponent<Rigidbody2D>();
            
            weaponScale = transform.localScale;
            inverseWeaponScale = new Vector2(-weaponScale.x, weaponScale.y);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Q))
                PickUpWeapon(testWeapon1);
            if(Input.GetKeyDown(KeyCode.E))
                PickUpWeapon(testWeapon2);
            
            if (currentWeapon != null && input.attackInput)
            {
                currentWeapon.Attack(AimAtDirection);
                isLastFrameAttack = true;
            }
            else if (isLastFrameAttack)
            {
                currentWeapon.StopAttack();
                isLastFrameAttack = false;
            }
            
            SetFaceDirection();
        }


        #region 武器的拾取，丢弃，切换

        private void AddWeapon(WeaponInGameBase weapon)
        {
            if (weaponInGames.Count >= weaponUpLimit) return;
            weaponInGames.Add(weapon);
        }

        private void RemoveWeapon(WeaponInGameBase weapon)
        {
            if (!weaponInGames.Contains(weapon)) return;
            weaponInGames.Remove(weapon);
        }

        private void RemoveCurrentWeapon()
        {
            RemoveWeapon(currentWeapon);
        }

        private void PickUpWeapon(WeaponDefinitionBase wd)
        {
            if (CurrentWeaponNum >= weaponUpLimit)
                RemoveCurrentWeapon();
            WeaponInGameBase newWeapon = GetWeaponInstance(wd);
            currentWeapon = newWeapon;
            AddWeapon(newWeapon);
            
            SwitchWeapon(currentWeapon);
        }

        private WeaponInGameBase GetWeaponInstance(WeaponDefinitionBase wd)
        {
            GameObject newWeaponGo = Instantiate(blankWeaponPrefab,weaponRoot);
            Type relevantType = GetRelevantType(wd.weaponId);
            WeaponInGameBase newWeapon = newWeaponGo.AddComponent(relevantType) as WeaponInGameBase;
            newWeapon.Init(wd);
            return newWeapon;
        }

        private void SwitchWeapon(WeaponInGameBase wig)
        {
            if(currentWeapon != null) currentWeapon.OnUnloadWeapon();
            currentWeapon = wig;
            currentWeapon.OnLoadWeapon();
        }

        private void SwitchWeapon(int index)
        {
            SwitchWeapon(weaponInGames[index]);
        }

        private void SwitchNextWeapon()
        {
            SwitchWeapon(NextWeaponIndex); 
        }
        
        private Type GetRelevantType(int i)
        {
            return weaponTypeDic[i];
        }

        #endregion

        private void SetFaceDirection()
        {
            if (info.target != null)
            {
                weaponRoot.localScale = info.TargetDirectionCoefficient > 0 ? weaponScale : inverseWeaponScale;
                weaponRoot.right = info.TargetDirection * ScaleIndex;
            }
            else
            {
                if (rb.velocity.x > Consts.TinyNum)
                    weaponRoot.localScale = weaponScale;
                else if (rb.velocity.x < -Consts.TinyNum)
                    weaponRoot.localScale = inverseWeaponScale;
                if(input.moveInput != Vector2.zero)
                    weaponRoot.right = input.moveInput * ScaleIndex;
            }
        }
    }
}
