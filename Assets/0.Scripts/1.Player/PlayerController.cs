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
    private Dictionary<AniamtionType, string> animationNameDic;
    
    private Vector2 playerScale;
    private Vector2 inversePlayerScale;

    private void Awake()
    {
        animationNameDic = new()
        {
            { AniamtionType.Idle, "Idle" },
            { AniamtionType.Move, "Move" },
            { AniamtionType.Die, "Die" },
        };
        
        info = GetComponent<PlayerInfo>();
        animator = info.playerAnimator;
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
        if (rb.velocity.x > Consts.tinyNum)
            playerSrTransfrom.localScale = playerScale;
        else if (rb.velocity.x < -Consts.tinyNum)
            playerSrTransfrom.localScale = inversePlayerScale;
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