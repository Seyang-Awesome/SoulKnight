using System;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public BoxCollider2D collider;
    [SerializeField] public SpriteRenderer playerSprite;
    [SerializeField] public Animator playerAnimator;

    private GameInput input;
    public float moveSpeed;

    public bool moveAction => input.moveInput != Vector2.zero;
    public bool isIdling => input.moveInput == Vector2.zero;
    

    private void Start()
    {
        input = GameInput.instance;
    }
}