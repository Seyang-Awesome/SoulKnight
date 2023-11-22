using Cysharp.Threading.Tasks;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class WeaponInGameBase : MonoBehaviour
    {
        protected WeaponDefinitionBase weaponDefinitionBase;
        protected WeaponDefinitionBase wd => weaponDefinitionBase;
        protected object[] skillData;
        public bool isCanAttack => !isCooling;
        public bool isCooling { get; private set; }
        public bool isInComboTime { get; private set; }
        public bool isShaking { get; protected set; }
        public int currentComboIndex{ get; private set; }

        // [SerializeField]
        // protected Transform weaponRoot;
        [SerializeField]
        protected Transform weaponSpriteTransform;
        [SerializeField]
        protected Transform bulletExitPosTransform;
        [SerializeField]
        protected Transform weaponEffectTransform;
        
        [SerializeField]
        protected SpriteRenderer weaponSr;
        [SerializeField]
        protected SpriteRenderer weaponEffectSr;

        private float attackIntervalCounter;
        private float comboCounter;

        private void Update()
        {
            AttackCooling();
            Combo();
        }

        public virtual void Init(WeaponDefinitionBase weaponDefinitionBase)
        {
            this.weaponDefinitionBase = weaponDefinitionBase;

            isCooling = false;
            isInComboTime = false;
            isShaking = false;
            currentComboIndex = 0;
            attackIntervalCounter = 0;
            comboCounter = 0;

            weaponSpriteTransform = transform.GetChild(0);
            bulletExitPosTransform = transform.GetChild(1);
            weaponEffectTransform = transform.GetChild(2);
            weaponSr = weaponSpriteTransform.GetComponent<SpriteRenderer>();
            weaponEffectSr = weaponEffectTransform.GetComponent<SpriteRenderer>();
        }

        public virtual void Attack()
        {
            if (!isCanAttack) return;

            Shake();

            if (wd.attackInterval <= 0f) return;

            isCooling = true;
            isInComboTime = true;
            comboCounter++;
            
            comboCounter = wd.comboTime;
            attackIntervalCounter = wd.attackInterval;
        }
        
        public virtual void OnLoadWeapon()
        {
            weaponSr.sortingOrder = 1;
        }

        public virtual void OnUnloadWeapon()
        {
            weaponSr.sortingOrder = -1;
        }
        
        protected abstract UniTask Shake();

        private void AttackCooling()
        {
            if (wd.attackInterval <= 0) return;
            if (!isCanAttack)
            {
                attackIntervalCounter -= Time.deltaTime;
                if (attackIntervalCounter <= 0)
                    isCooling = false;
            }
        }

        private void Combo()
        {
            if (wd.comboTime <= 0) return;
            if (isInComboTime)
            {
                comboCounter -= Time.deltaTime;
                if (comboCounter <= 0)
                {
                    isInComboTime = false;
                    currentComboIndex = 0;
                }
            }
        }

    }
}