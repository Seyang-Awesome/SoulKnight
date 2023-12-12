using System;
using System.Collections.Generic;
using UnityEngine;

public class CommonBox : Hurtable
{
    [SerializeField] private ParticleManager particle;
    
    private int currentHealth;

    private void Start()
    {
        currentHealth = 5;
    }

    public override void Hurt(HurtInfo info)
    {
        currentHealth -= info.Damage;
        if (currentHealth <= 0)
            DestroyBox();
    }

    protected virtual void DestroyBox()
    {
        particle = PoolManager.Instance.GetGameObject(particle);
        particle.transform.position = transform.position + new Vector3(0.5f,0.5f);
        
        PoolManager.Instance.PushGameObject(transform.root.gameObject);
    }
}

