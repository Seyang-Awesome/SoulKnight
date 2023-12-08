using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Hurtable : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer sr;
    [SerializeField] protected Rigidbody2D rb;
    public EntityInfo EntityInfo { get; private set; }

    private void Start()
    {
        EntityInfo = GetComponentInParent<EntityInfo>();
    }

    public abstract void Hurt(HurtInfo hurtInfo);

    protected async virtual UniTask Flash()
    {
        sr.material.SetFloat(Consts.FlashAmount,1);
        await UniTask.Delay(TimeSpan.FromSeconds(Consts.FlashTime));
        sr.material.SetFloat(Consts.FlashAmount,0);
    }

    protected async virtual void Back(HurtInfo info)
    {
        Vector2 last = rb.velocity;
        rb.velocity = info.DamageDirection * Consts.BackVelocity;
        await UniTask.Delay(TimeSpan.FromSeconds(Consts.BackTime));
        rb.velocity = last;
    }
}

