using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public float particleTime;
    ParticleSystem particle;
    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }
    private void OnEnable()
    {
        particle.Play();
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            if (time > particleTime)
            {
                OnDestroyParticle();
                yield break;
            }
            yield return null;
        }
    }

    private void OnDestroyParticle()
    {
        PoolManager.instance.PushGameObject(gameObject);
    }
}
