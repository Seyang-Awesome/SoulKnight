using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private Transform playerSrTransfrom;
    
    private PlayerInfo info;
    private Animator animator;
    private Rigidbody2D rb;
    private Dictionary<AnimationType, string> animationNameDic;
    
    private Vector2 playerScale;
    private Vector2 inversePlayerScale;

    private void Awake()
    {
        animationNameDic = new()
        {
            { AnimationType.Idle, "Idle" },
            { AnimationType.Move, "Move" },
            { AnimationType.Die, "Die" },
        };
        
        info = GetComponent<PlayerInfo>();
        animator = info.animator;
        rb = info.rb;
    }
    private void Start()
    {
        playerScale = transform.localScale;
        inversePlayerScale = new Vector3(-playerScale.x, playerScale.y);
    }

    private void Update()
    {
        SetFaceDirection();
    }

    public void SetFaceDirection()
    {
        if (info.target != null)
            playerSrTransfrom.localScale = info.TargetDirection.x >= 0 ? playerScale : inversePlayerScale;
        else
        {
            if (rb.velocity.x > Consts.TinyNum)
                playerSrTransfrom.localScale = playerScale;
            else if (rb.velocity.x < -Consts.TinyNum)
                playerSrTransfrom.localScale = inversePlayerScale;
        }
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

    public void PlayAnimation(AnimationType type)
    {
        animator.Play(animationNameDic[type]);
    }
}