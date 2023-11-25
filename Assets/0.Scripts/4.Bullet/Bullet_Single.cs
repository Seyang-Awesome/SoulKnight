using System;
using UnityEngine;
using WeaponSystem;
using Random = UnityEngine.Random;

public class Bullet_Single : BulletBase
{
    [SerializeField] private float velocity;

    [SerializeField] private GameObject particle;

    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private Animator animator;

    private float appearCounter;
    private bool isActive;

    private WeaponDefinitionBase wd;
    
    public void Init(Vector2 direction, int accuracy, WeaponDefinitionBase wd)
    {
        animator.Play("Fly");
        transform.right = direction;
        transform.Rotate(new Vector3(0,0,Random.Range((float)-accuracy, (float)accuracy)));
        rb.velocity = transform.right.normalized * velocity;

        appearCounter = Consts.BulletDisapperTime;
        isActive = true;

        this.wd = wd;
    }

    private void OnTouchEntity()
    {
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
        
        //TODO：判断是不是敌人，如果是敌人，则对它造成伤害
        if (other.CompareTag(Consts.EnemyTeamTag))
        {
            Debug.Log("Touch");
            HurtInfo hurtInfo = new(wd.damage,other.bounds.center - transform.position);
            other.GetComponent<Hurtable>().Hurt(hurtInfo);

        }
        
        OnTouchEntity();
    }
}