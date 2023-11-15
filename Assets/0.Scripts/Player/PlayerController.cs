using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInfo info;
    private Rigidbody2D rb;
    private Vector3 playerScale;
    
    private void Start()
    {
        info = GetComponent<PlayerInfo>();
        rb = GetComponent<Rigidbody2D>();
        playerScale = transform.localScale;
    }

    public void SetFaceDirection(Vector3 velocity)
    {
        if (velocity.x > 0)
        {
            transform.localScale = playerScale;
        }
        else if (velocity.x < 0)
        {
            transform.localScale = new Vector3(-playerScale.x, playerScale.y, playerScale.z);
        }
    }

    public void SetVelocity(Vector2 velocity, float dragFactor = 0, bool isWithFaceAdjust = true)
    {
        rb.velocity = velocity * (1 - dragFactor);
    }

    public void SetVelocityX(float velocity, float dragFactor = 0, bool isWithFaceAdjust = true)
    {
        SetVelocity(new Vector2(velocity, rb.velocity.y), dragFactor, isWithFaceAdjust);
    }

    public void SetVelocityY(float velocity, float dragFactor = 0, bool isWithFaceAdjust = true)
    {
        SetVelocity(new Vector2(rb.velocity.x, velocity), dragFactor, isWithFaceAdjust);
    }
}