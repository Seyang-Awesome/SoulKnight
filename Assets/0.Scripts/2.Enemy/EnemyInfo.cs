using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public partial class EventManager : MonoSingleton<EventManager>
{
    public InvokableAction<EnemyDieInfo> OnEnemyDie = new();
}

public class EnemyInfo : EntityInfo
{
    [Header("敌人模块")]
    public float detectPlayerRadius;

    public int basicHealth;
    public Sprite enemyDieSprite;
    public InvokableAction<EnemyDieInfo> onThisEnemyDie = new();

    [SerializeField]
    private int _currentHealth;
    private int currentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            if (_currentHealth < 0)
                _currentHealth = 0;
        }
    }

    public bool backable;
    
    private void OnEnable()
    {
        Init();
    }
    
    public void Init()
    {
        currentHealth = basicHealth;
        backable = true;

        trigger = transform.GetChild(Consts.EnemyTriggerIndex).GetComponent<Collider2D>();
    }
    
    public void Hurt(HurtInfo hurtInfo)
    {
        currentHealth -= hurtInfo.Damage;
        if (currentHealth <= 0)
        {
            EnemyDieInfo info = new EnemyDieInfo(CenterPos, hurtInfo.DamageDirection, enemyDieSprite,gameObject);
            onThisEnemyDie.Invoke(info);
            EventManager.Instance.OnEnemyDie.Invoke(info);
        }
    }
    

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     Debug.Log(111);
    // }
    //
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     Debug.Log(222);
    // }
    
#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(CenterPos, detectPlayerRadius);
    }

#endif
    
}

