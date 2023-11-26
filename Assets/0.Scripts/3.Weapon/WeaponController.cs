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
        
        private List<WeaponInGameBase> weaponInGames = new();
        private WeaponInGameBase currentWeapon;
        public int currentWeaponNum => weaponInGames.Count;
        public int currentWeaponIndex => weaponInGames.IndexOf(currentWeapon);
        public Vector2 aimAtDirection => weaponRoot.localScale.x > 0 ? weaponRoot.right : -weaponRoot.right;
        public float scaleIndex => weaponRoot.localScale.x > 0 ? 1f : -1f;

        public int nextWeaponIndex
        {
            get
            {
                if (currentWeaponNum == 0) return -1;
                if (currentWeaponNum == 1) return currentWeaponIndex;
                return (currentWeaponIndex + 1) % currentWeaponNum;
            }
        }

        private void Start()
        {
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
            if(currentWeapon != null && input.attackInput)
                currentWeapon.Attack(aimAtDirection);
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
            if (currentWeaponNum >= weaponUpLimit)
                RemoveCurrentWeapon();
            WeaponInGameBase newWeapon = GetWeaponInstance(wd);
            currentWeapon = newWeapon;
            AddWeapon(newWeapon);
            
            SwitchWeapon(currentWeapon);
        }

        private WeaponInGameBase GetWeaponInstance(WeaponDefinitionBase wd)
        {
            GameObject newWeaponGo = Instantiate(blankWeaponPrefab,weaponRoot);
            Type relevantType = WeaponComponentTypeGetter.Instance.GetRelevantType(wd.weaponId);
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
            SwitchWeapon(nextWeaponIndex); 
        }

        #endregion

        private void SetFaceDirection()
        {
            if (info.target != null)
            {
                weaponRoot.localScale = info.TargetDirectionCoefficient == 1 ? weaponScale : inverseWeaponScale;
                weaponRoot.right = info.TargetDirection * -scaleIndex;
            }
            else
            {
                if (rb.velocity.x > Consts.TinyNum)
                    weaponRoot.localScale = weaponScale;
                else if (rb.velocity.x < -Consts.TinyNum)
                    weaponRoot.localScale = inverseWeaponScale;
                if(input.moveInput != Vector2.zero)
                    weaponRoot.right = input.moveInput * scaleIndex;
            }
        }
    }
}
