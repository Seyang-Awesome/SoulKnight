using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BattleManager : MonoSingleton<BattleManager>
{
    [SerializeField] private EnemyConfig enemyConfig;
    [SerializeField] private EnemySpawner enemySpawnerPrefab;
    [SerializeField] private EnemyDieHandler enemyDieHandlerPrefab;
    
    private RoomBattleInfo currentRoomBattleInfo;
    private void Start()
    {
        EventManager.Instance.OnEnemyDie += OnEnemyDie;
        EventManager.Instance.OnRequestSpawnEnemy += OnRequestSpawnEnemy;
    }

    private void OnDestroy()
    {
        EventManager.Instance.OnEnemyDie -= OnEnemyDie;
        EventManager.Instance.OnRequestSpawnEnemy -= OnRequestSpawnEnemy;
    }
    
    private async void OnRequestSpawnEnemy(List<Vector2> enemySpawnPosList, int enemyNum)
    {
        List<Vector2> spawnPosList = new List<Vector2>(enemySpawnPosList);
        
        for (int i = 0; i < enemyNum; i++)
        {
            Vector2 enemySpawnPos = spawnPosList[Random.Range(0, spawnPosList.Count)];
            spawnPosList.Remove(enemySpawnPos);
            
            GameObject enemy = enemyConfig.GetRandomEnemy();
            EnemySpawner enemySpawner = PoolManager.Instance.GetGameObject(enemySpawnerPrefab);
            enemySpawner.Init(enemy);
            enemySpawner.transform.position = enemySpawnPos;
        }

        currentRoomBattleInfo = new RoomBattleInfo(enemyNum);
    }

    private void OnEnemyDie(EnemyDieInfo info)
    {
        PoolManager.Instance.PushGameObject(info.EnemyGameObject);
        EnemyDieHandler enemyDieHandler = PoolManager.Instance.GetGameObject<EnemyDieHandler>(enemyDieHandlerPrefab);
        enemyDieHandler.Init(info);
        enemyDieHandler.transform.position = info.Position;
        
        currentRoomBattleInfo.MinusEnemy();
        if (currentRoomBattleInfo.CurrentEnemyNum <= 0)
            EventManager.Instance.OnEnemyClear.Invoke();
    }
}

