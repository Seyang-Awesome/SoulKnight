using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInfo : EntityInfo
{
    [Header("玩家模块")]
    
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Animator animator;
    
    private GameInput input;
    public bool MoveAction => input.moveInput != Vector2.zero;
    public bool IsIdling => input.moveInput == Vector2.zero;
    
    public bool hurt;
    public HurtInfo hurtInfo;

    public float detectRadius;

    private void Start()
    {
        input = GameInput.Instance;
        hurt = false;
    }

    private void Update()
    {
        DetectEnemy();
    }

    public void Hurt(HurtInfo hurtInfo)
    {
        hurt = true;
        this.hurtInfo = hurtInfo;
    }

    private void DetectEnemy()
    {
        Collider2D[] detects = Physics2D.OverlapCircleAll(CenterPos, detectRadius, 1 << Consts.EnemyTriggerLayer);
        if (detects.Length == 0)
            target = null;
        else
        {
            float minDistance = Consts.LargeNum;
            Collider2D target = detects[0];
            foreach (var detect in detects)
            {
                float distance = Vector2.Distance(CenterPos, detect.bounds.center);
                if (distance < minDistance)
                {
                    target = detect;
                    minDistance = distance;
                }
            }

            this.target = target;
        }
    }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     Debug.Log(555);
    // }


}