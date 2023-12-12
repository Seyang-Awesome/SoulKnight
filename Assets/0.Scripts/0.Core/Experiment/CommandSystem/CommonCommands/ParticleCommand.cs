using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class ParticleCommand : ICommand
{
    [SerializeField] private ParticleManager particle;
    [SerializeField] private Vector3 generateOffset;

    public void OnInvoke(GameObject gameObject)
    {
        ParticleManager newParticle = PoolManager.Instance.GetGameObject(particle);
        newParticle.transform.position = gameObject.transform.position + generateOffset;
    }
}

