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

    public bool moveAction => input.moveInput != Vector2.zero;
    public bool isIdling => input.moveInput == Vector2.zero;
    

    private void Start()
    {
        input = GameInput.Instance;
    }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     Debug.Log(555);
    // }


}