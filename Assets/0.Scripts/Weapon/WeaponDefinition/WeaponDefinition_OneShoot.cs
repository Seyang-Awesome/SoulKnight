using UnityEngine;

namespace WeaponSystem
{
    [CreateAssetMenu(fileName = "WeaponDefinition", menuName = "Weapon/OneShoot")]
    public class WeaponDefinition_OneShoot : WeaponDefinitionBase
    {
        [Header("单发模块")] 
    
        public BulletBase[] bullets;

        public Vector2 bulletExitPos;
        public float shakeIntensity;
        public float shakeTime;
        public float backTime;

    }
}
