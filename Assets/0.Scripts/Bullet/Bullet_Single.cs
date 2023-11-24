using System;
using UnityEngine;
public class Bullet_Single : BulletBase
{
    [SerializeField] private float velocity;

    [SerializeField] private GameObject particle;

    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private Animator animator;

    private float appearCounter;
    private bool isActive;
    
    public void Init(Vector2 direction)
    {
        animator.Play("Fly");
        rb.velocity = direction.normalized * velocity;
        transform.right = direction;

        appearCounter = Consts.bulletDisapperTime;
        isActive = true;
    }

    private void OnTouchEntity()
    {
        animator.Play("Die");
        rb.velocity = Vector2.zero;

        GameObject newParticle = PoolManager.instance.GetGameObject(particle);
        newParticle.transform.position = transform.position;
        
        isActive = false;
    }

    private void OnDestroyBullet()
    {
        PoolManager.instance.PushGameObject(this.gameObject);
    }

    private void Update()
    {
        appearCounter -= Time.deltaTime;
        if(appearCounter <= 0)
            OnDestroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive) return;
        
        //TODO：判断是不是敌人，如果是敌人，则对它造成伤害
        
        OnTouchEntity();
    }
}