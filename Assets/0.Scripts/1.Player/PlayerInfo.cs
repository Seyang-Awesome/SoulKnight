using System;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public new Collider2D collider;
    [SerializeField] public Collider2D trigger;
    [SerializeField] public SpriteRenderer playerSprite;
    [SerializeField] public Animator playerAnimator;
    private GameInput input;
    
    public float moveSpeed;
    public bool MoveAction => input.moveInput != Vector2.zero;
    public bool IsIdling => input.moveInput == Vector2.zero;

    public Vector2 CurrentPos => trigger.bounds.center;
    
    public bool hurt;
    public HurtInfo hurtInfo;

    public float detectRadius;
    public Collider2D target;
    public Vector2 TargetPos => target.bounds.center;
    public Vector2 TargetDirection => CurrentPos - TargetPos;
    public int TargetDirectionCoefficient => target.bounds.center.x > CurrentPos.x ? 1 : -1;

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
        Collider2D[] detects = Physics2D.OverlapCircleAll(CurrentPos, detectRadius, 1 << Consts.EnemyTriggerLayer);
        if (detects.Length == 0)
            target = null;
        else
        {
            float minDistance = Consts.LargeNum;
            Collider2D target = detects[0];
            foreach (var detect in detects)
            {
                float distance = Vector2.Distance(CurrentPos, detect.bounds.center);
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