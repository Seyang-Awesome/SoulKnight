using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Poison : MonoBehaviour
{
    private bool isReady;
    private float existCounter;
    private float detectCounter;
    
    private async void OnEnable()
    {
        isReady = false;
        existCounter = Consts.PoisonAndFireExistTime;
        detectCounter = Consts.PoisonAndFireDetectTime;
        transform.localScale = Vector2.zero;
        transform.DOScale(new Vector2(Consts.PoisonAndFireDetectRadius, Consts.PoisonAndFireDetectRadius),
            (Consts.PoisonAndFireAppearTime));

        await UniTask.Delay(TimeSpan.FromSeconds(Consts.PoisonAndFireAppearTime));

        isReady = true;
    }

    private void Update()
    {
        if (isReady)
        {
            existCounter -= Time.deltaTime;
            detectCounter -= Time.deltaTime;
            if (existCounter <= 0)
                OnTimeUp();
            if (detectCounter <= 0)
                Detect();
        }
    }

    private void Detect()
    {
        Collider2D[] detects = Physics2D.OverlapCircleAll(transform.position,Consts.PoisonAndFireDetectRadius, Consts.BuffTargetLayerMask);
        detects.ToList().ForEach(detect =>
        {
            Hurtable target = detect.GetComponent<Hurtable>();
            BuffManager.Instance.AddBuff(new BuffInfo(target, 2 ,3 ,(int)BuffType.Poison));
        });
    }

    private async void OnTimeUp()
    {
        isReady = false;
        transform.DOScale(Vector2.zero, Consts.PoisonAndFireAppearTime);
        
        await UniTask.Delay(TimeSpan.FromSeconds(Consts.PoisonAndFireAppearTime));
        
        PoolManager.Instance.PushGameObject(this.gameObject);
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Consts.PoisonAndFireDetectRadius);
    }

#endif    
    
}



