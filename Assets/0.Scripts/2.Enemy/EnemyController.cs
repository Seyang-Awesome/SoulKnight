using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    [Header("Base")]
    
    [SerializeField] protected EnemyInfo info;
    [SerializeField] protected Animator animator;
    [SerializeField] protected new Collider2D collider;
    [SerializeField] protected Collider2D trigger;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected BtRunner btRunner;
    
    [SerializeField] protected EnemyDieHandler enemyDieHandlerPrefab;
    
    private Dictionary<AnimationType, string> animationNameDic;
    
    private Vector2 enemyScale;
    private Vector2 inverseEnemyScale;

    private const float RayLength = 1f;
    
    protected virtual void Start()
    {
        animationNameDic = new()
        {
            { AnimationType.Idle, "Idle" },
            { AnimationType.Move, "Move" },
            { AnimationType.Die, "Die" },
        };

        info.onEnemyDie += OnEnemyDie;
        
        enemyScale = transform.localScale;
        inverseEnemyScale = new Vector3(-enemyScale.x, enemyScale.y);
    }

    private void OnDestroy()
    {
        info.onEnemyDie -= OnEnemyDie;
    }

    protected virtual void Update()
    {
        // SetFaceDirection();
        // Debug.Log(GetWallDirection());
        // DetectPlayer();
    }

    public void SetFaceDirection()
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

    public int GetWallDirection()
    {
        int result = 0;
        Vector2 CenterPos = info.CenterPos;
        if (Physics2D.Raycast(CenterPos, Vector2.up,
                Consts.WallDetectLength, 1 << Consts.MapLayer))
        {
            result |= (int)Direction.Up;
        }
        
        if (Physics2D.Raycast(CenterPos, Vector2.down,
                Consts.WallDetectLength, 1 << Consts.MapLayer))
        {
            result |= (int)Direction.Down;
        }
        
        if (Physics2D.Raycast(CenterPos, Vector2.left,
                Consts.WallDetectLength, 1 << Consts.MapLayer))
        {
            result |= (int)Direction.Left;
        }
        
        if (Physics2D.Raycast(CenterPos, Vector2.right,
                Consts.WallDetectLength, 1 << Consts.MapLayer))
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

    public virtual void Attack()
    {
        
    }

    private void OnEnemyDie(EnemyDieInfo info)
    {
        PoolManager.Instance.PushGameObject(this.gameObject);
        EnemyDieHandler enemyDieHandler = PoolManager.Instance.GetGameObject<EnemyDieHandler>(enemyDieHandlerPrefab);
        enemyDieHandler.Init(info);
        enemyDieHandler.transform.position = transform.position;
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

