using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField]
    private Collider2D self;
    public Collider2D target;
    public Vector2 CenterPos => self.bounds.center;
    public Vector2 TargetPos => target.bounds.center;
    
    public float moveSpeed;
    public float dragFactor;
    public float detectPlayerRadius;

    public int basicHealth;
    public int attackPower;
    public Sprite enemyDieSprite;
    public InvokableAction<EnemyDieInfo> onEnemyDie = new();

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
    }
    
    public void Hurt(HurtInfo hurtInfo)
    {
        currentHealth -= hurtInfo.Damage;
        if (currentHealth <= 0)
            onEnemyDie?.Invoke(new EnemyDieInfo(hurtInfo.DamageDirection, enemyDieSprite));
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(CenterPos, detectPlayerRadius);
    }

#endif
    
}

