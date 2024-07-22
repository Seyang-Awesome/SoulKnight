using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace WeaponSystem
{
    /// <summary>
    /// 武器执行逻辑的基类
    /// 1. 根据玩家输入执行武器的相关逻辑
    /// 2. 检测武器的执行状态
    /// 3. 武器的效果展示，包括武器特效，发射子弹
    /// </summary>
    public abstract class WeaponInGameBase : MonoBehaviour
    {
        protected WeaponDefinitionBase weaponDefinitionBase;
        protected WeaponDefinitionBase Wd => weaponDefinitionBase;
        // protected object[] skillData;
        protected bool IsCanAttack => !IsCooling;
        protected bool IsCooling { get; private set; }
        protected bool IsInComboTime { get; private set; }
        protected bool IsShaking { get; set; }
        protected int ComboIndex{ get; private set; }
        protected Vector2 WeaponDirection => transform.right * scaleIndex;
        protected int scaleIndex => weaponRoot.localScale.x > 0 ? 1 : -1;
        
        [SerializeField] protected Transform weaponRoot;
        [SerializeField] protected Transform weaponSpriteTransform;
        [SerializeField] protected Transform bulletExitPosTransform;
        [SerializeField] protected Transform weaponEffectTransform;
        
        [SerializeField] protected SpriteRenderer weaponSr;
        [SerializeField] protected SpriteRenderer weaponEffectSr;
        [SerializeField] protected Animator weaponSpriteAnimator;
        [SerializeField] protected Animator weaponEffectAnimator;
        
        private float attackIntervalCounter;
        private float comboCounter;

        public event Action onLoadWeapon;
        public event Action onUnloadWeapon;
        public event Action onAttack;
        public event Action onStopAttack;
        public event Action<int> onTriggerCombo;

        private void Update()
        {
            AttackCooling();
            Combo();
        }

        public virtual void Init(WeaponDefinitionBase weaponDefinitionBase)
        {
            this.weaponDefinitionBase = weaponDefinitionBase;

            IsCooling = false;
            IsInComboTime = false;
            IsShaking = false;
            ComboIndex = 0;
            attackIntervalCounter = 0;
            comboCounter = 0;

            weaponRoot = transform.parent;
            weaponSpriteTransform = transform.GetChild(0);
            bulletExitPosTransform = transform.GetChild(1);
            weaponEffectTransform = transform.GetChild(2);
            weaponSr = weaponSpriteTransform.GetComponent<SpriteRenderer>();
            weaponEffectSr = weaponEffectTransform.GetComponent<SpriteRenderer>();
            weaponSpriteAnimator = weaponSpriteTransform.GetComponent<Animator>();
            weaponEffectAnimator = weaponEffectTransform.GetComponent<Animator>();

            weaponSr.sprite = weaponDefinitionBase.inGameSprite;
        }

        public virtual void Attack(Vector2 _)
        {
            if (!IsCanAttack) return;

            Shake();

            IsCooling = true;
            IsInComboTime = true;
            ComboIndex++;
            onTriggerCombo?.Invoke(ComboIndex);
            
            comboCounter = Wd.comboTime;
            attackIntervalCounter = Wd.attackInterval;
            
            onAttack?.Invoke();
        }

        public virtual void StopAttack()
        {
            onStopAttack?.Invoke();
        }
        
        public virtual void OnLoadWeapon()
        {
            weaponSr.sortingOrder = 1;
            onLoadWeapon?.Invoke();
        }

        public virtual void OnUnloadWeapon()
        {
            weaponSr.sortingOrder = -1;
            onUnloadWeapon?.Invoke();
        }
        
        protected abstract void Shake();

        private void AttackCooling()
        {
            if (Wd.attackInterval <= 0) return;
            if (!IsCanAttack)
            {
                attackIntervalCounter -= Time.deltaTime;
                if (attackIntervalCounter <= 0)
                    IsCooling = false;
            }
        }

        private void Combo()
        {
            if (Wd.comboTime <= 0) return;
            if (IsInComboTime)
            {
                comboCounter -= Time.deltaTime;
                if (comboCounter <= 0)
                {
                    IsInComboTime = false;
                    ComboIndex = 0;
                    onTriggerCombo?.Invoke(ComboIndex);
                }
            }
        }

    }
}