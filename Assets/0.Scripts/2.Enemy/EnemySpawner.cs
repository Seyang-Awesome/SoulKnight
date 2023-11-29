using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject enemy;
    public void Init(GameObject enemy)
    {
        this.enemy = enemy;
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = PoolManager.Instance.GetGameObject(enemy);
        newEnemy.transform.position = this.transform.position;
    }

    private void HideSelf()
    {
        PoolManager.Instance.PushGameObject(gameObject);
    }
}

