using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Hurtable : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer sr;

    [SerializeField] protected Rigidbody2D rb;

    public virtual void Hurt(HurtInfo hurtInfo)
    {
        Flash();
        Back(hurtInfo.DamageDirection);
    }

    protected async virtual UniTask Flash()
    {
        sr.material.SetFloat(Consts.FlashAmout,1);
        await UniTask.Delay(TimeSpan.FromSeconds(Consts.FlashTime));
        sr.material.SetFloat(Consts.FlashAmout,0);
    }

    protected async virtual void Back(Vector2 direction)
    {
        Vector2 last = rb.velocity;
        
        rb.velocity = direction * Consts.BackCoffient;
        await UniTask.Delay(TimeSpan.FromSeconds(Consts.FlashTime));
        rb.velocity = last;
    }
    
    
}

