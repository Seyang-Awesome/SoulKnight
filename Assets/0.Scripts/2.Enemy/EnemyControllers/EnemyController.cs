using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class EnemyController : MonoBehaviour
{
    [Header("Base")]
    
    protected EnemyInfo info;
    protected Animator animator;
    protected new Collider2D collider;
    protected Collider2D trigger;
    
    protected Rigidbody2D rb;
    protected BtRunner btRunner;
    
    private Dictionary<AnimationType, string> animationNameDic;
    
    protected Vector2 enemyScale;
    protected Vector2 inverseEnemyScale;
    public float scaleIndex => transform.localScale.x > 0 ? 1f : -1f;

    private const float RayLength = 1f;

    #region Unity

    protected virtual void Awake()
    {
        info = GetComponent<EnemyInfo>();
        animator = transform.GetChild(Consts.EnemySpriteIndex).GetComponent<Animator>();
        collider = transform.GetChild(Consts.EnemyColliderIndex).GetComponent<Collider2D>();
        trigger = transform.GetChild(Consts.EnemyTriggerIndex).GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        btRunner = GetComponent<BtRunner>();
        
        animationNameDic = new()
        {
            { AnimationType.Idle, "Idle" },
            { AnimationType.Move, "Move" },
            { AnimationType.Die, "Die" },
        };
        
        
        enemyScale = transform.localScale;
        inverseEnemyScale = new Vector3(-enemyScale.x, enemyScale.y);
    }
    

    #endregion

    #region Set

    public virtual void SetFaceDirection()
    {
        if (rb.velocity.x > Consts.TinyNum)
            transform.localScale = enemyScale;
        else if (rb.velocity.x < -Consts.TinyNum)
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

    public void PlayAnimation(AnimationType type)
    {
        animator.Play(animationNameDic[type]);
    }

    #endregion

    #region Get

    public int GetWallDirection()
    {
        int result = 0;
        Vector2 CenterPos = info.CenterPos;
        if (Physics2D.Raycast(CenterPos, Vector2.up,
                Consts.WallDetectLength, Consts.MapLayerMask))
        {
            result |= (int)Direction.Up;
        }
        
        if (Physics2D.Raycast(CenterPos, Vector2.down,
                Consts.WallDetectLength, Consts.MapLayerMask))
        {
            result |= (int)Direction.Down;
        }
        
        if (Physics2D.Raycast(CenterPos, Vector2.left,
                Consts.WallDetectLength, Consts.MapLayerMask))
        {
            result |= (int)Direction.Left;
        }
        
        if (Physics2D.Raycast(CenterPos, Vector2.right,
                Consts.WallDetectLength, Consts.MapLayerMask))
        {
            result |= (int)Direction.Right;
        }

        return result;
    }

    public bool DetectPlayer()
    {
        var detecteds = Physics2D.OverlapCircleAll(info.CenterPos, info.detectPlayerRadius, Consts.EnemyTargetLayerMask);
        
        if (detecteds.Length == 0)
        {
            info.target = null;
            return false;
        }
        else if (detecteds.Length == 1)
        {
            info.target = detecteds[0];
            return true;
        }
        else
        {
            float minDistance = Vector2.Distance(info.CenterPos, detecteds[0].bounds.center);
            Collider2D target = detecteds[0];
            for(int i = 1; i < detecteds.Length; i++)
            {
                float distance = Vector2.Distance(detecteds[i].bounds.center,info.CenterPos);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    target = detecteds[i];
                }
            }
            info.target = target;
            return true;
        }
    }

    #endregion

    public virtual void Attack()
    {
        
    }
    
    
// #if UNITY_EDITOR
//
//     private void OnDrawGizmos()
//     {
//         Gizmos.color = Color.yellow;
//         Gizmos.DrawWireSphere(info.CenterPos, info.detectPlayerRadius);
//     }
//
// #endif
}

