using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyInfo info;
    private Animator animator;
    private Rigidbody2D rb;
    private Dictionary<AniamtionType, string> animationNameDic;
    
    private Vector2 enemyScale;
    private Vector2 inverseEnemyScale;
    
    private void Start()
    {
        animationNameDic = new()
        {
            { AniamtionType.Idle, "Idle" },
            { AniamtionType.Move, "Move" },
            { AniamtionType.Die, "Die" },
        };
        
        info = GetComponent<EnemyInfo>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
        enemyScale = transform.localScale;
        inverseEnemyScale = new Vector3(-enemyScale.x, enemyScale.y);
    }

    // private void Update()
    // {
    //     // SetFaceDirection();
    // }

    public void SetFaceDirection()
    {
        if (rb.velocity.x > Consts.tinyNum)
            transform.localScale = enemyScale;
        else if (rb.velocity.x < -Consts.tinyNum)
            transform.localScale = inverseEnemyScale;
    }

    public void SetVelocity(Vector2 velocity, float dragFactor = 0)
    {
        rb.velocity = velocity * (1 - dragFactor);
    }

    public void SetVelocityX(float velocity, float dragFactor = 0)
    {
        SetVelocity(new Vector2(velocity, rb.velocity.y), dragFactor);
    }

    public void SetVelocityY(float velocity, float dragFactor = 0)
    {
        SetVelocity(new Vector2(rb.velocity.x, velocity), dragFactor);
    }

    public void PlayAnimation(AniamtionType type)
    {
        animator.Play(animationNameDic[type]);
    }
}

