using UnityEngine;

namespace WeaponSystem
{
    public abstract class WeaponDefinitionBase : ScriptableObject
    {
        [Header("通用武器模块")] 
        
        public int weaponId;
        public string weaponName;
    
        public Sprite inGameSprite;
        public Sprite uiSprite;

        public int damage;
        public int powerConsume;
        public int criticalChance;
        public int accuracy;
    
        public float attackInterval;
        public float comboTime;

        public UnityEngine.Object others;
    }
}