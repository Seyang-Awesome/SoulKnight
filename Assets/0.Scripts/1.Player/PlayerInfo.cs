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
    
    public bool hurt;
    public HurtInfo hurtInfo;

    private void Start()
    {
        input = GameInput.Instance;
        hurt = false;
    }

    public void Hurt(HurtInfo hurtInfo)
    {
        hurt = true;
        this.hurtInfo = hurtInfo;
    }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     Debug.Log(555);
    // }


}