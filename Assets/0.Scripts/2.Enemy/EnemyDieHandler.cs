using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieHandler : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Collider2D collider;
    
    private float currentVelocityCeoffcient => 
        Mathf.Pow((counter/Consts.EnemyDieFlyTime),2) * Consts.EnemyDieBasicVelocity;
    
    private Vector2 direction;
    private float counter;
    private bool isFlyOver;
    public void Init(EnemyDieInfo info)
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        
        sr.sprite = info.EnemeyDieSprite;
        rb.velocity = info.Direction * Consts.EnemyDieBasicVelocity;
        
        direction = info.Direction.normalized;
        counter = Consts.EnemyDieFlyTime;
        isFlyOver = false;
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * currentVelocityCeoffcient;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        int directionInfo = GetWallDirection();
        if ((directionInfo & (int)Direction.Up) != 0 || (directionInfo & (int)Direction.Down) != 0)
            direction = new Vector2(direction.x, -direction.y);
        if ((directionInfo & (int)Direction.Left) != 0 || (directionInfo & (int)Direction.Right) != 0)
            direction = new Vector2(-direction.x, direction.y);
    }

    private void Update()
    {
        if (isFlyOver) return;
        
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            isFlyOver = true;
            rb.velocity = Vector2.zero;
            this.enabled = false;
        }
    }
    
    public int GetWallDirection()
    {
        int result = 0;
        Vector2 CenterPos = collider.bounds.center;
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
}

