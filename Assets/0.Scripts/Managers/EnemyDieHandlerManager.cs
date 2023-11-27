using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieHandlerManager : MonoSingleton<EnemyDieHandlerManager>
{
    [SerializeField] private EnemyDieHandler enemyDieHandlerPrefab;
    public void OnEnemyDie(GameObject gameObject,EnemyDieInfo info)
    {
        PoolManager.Instance.PushGameObject(gameObject);
        EnemyDieHandler enemyDieHandler = PoolManager.Instance.GetGameObject<EnemyDieHandler>(enemyDieHandlerPrefab);
        enemyDieHandler.Init(info);
        enemyDieHandler.transform.position = info.Position;
    }
}

