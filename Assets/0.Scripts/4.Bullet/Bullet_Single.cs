using System;
using UnityEngine;
using WeaponSystem;
using Random = UnityEngine.Random;

public class Bullet_Single : BulletBase
{
    [SerializeField] private float velocity;

    [SerializeField] private GameObject particle;
    
    [SerializeField] private Animator animator;

    private GameObject light;
    private float appearCounter;
    private bool isActive;

    private BulletInfo info;
    private Rigidbody2D rb;
    
    public override void Init(BulletInfo info)
    {
        base.Init(info);
        this.info = info;
        rb = GetComponent<Rigidbody2D>();
        
        light = transform.GetChild(0).gameObject;
        light.SetActive(true);
        
        transform.right = info.Direction;
        transform.Rotate(new Vector3(0,0,Random.Range((float)-info.Offset, (float)info.Offset)));
        rb.velocity = transform.right.normalized * velocity;

        appearCounter = Consts.BulletDisappearTime;
        isActive = true;
    }

    private void OnTouchEntity()
    {
        light.SetActive(false);
        animator.Play("Die");
        rb.velocity = Vector2.zero;

        GameObject newParticle = PoolManager.Instance.GetGameObject(particle);
        newParticle.transform.position = transform.position;
        
        isActive = false;
    }

    private void OnDestroyBullet()
    {
        PoolManager.Instance.PushGameObject(this.gameObject);
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
        
        if (other.CompareTag(Consts.EnemyTeamTag) || other.CompareTag(Consts.PlayerTeamTag) ||
            other.CompareTag(Consts.BoxTag))
        {
            HurtInfo hurtInfo = new(info.Damage,other.bounds.center - transform.position,1f);
            other.GetComponent<Hurtable>().Hurt(hurtInfo);
        }
        
        OnTouchEntity();
    }
}