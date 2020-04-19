using System;
using UnityEngine;
using Enemy;
using System.Collections;

public enum SpawnPoint { Left, Right }
public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager instance;

    private Queue spawnQueue;
    private Coroutine spawnLoop;

    private int spawnCount = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public Transform leftSpawn, rightSpawn, attackPosLeft, attackPosRight;

    public float minSpawnTime, maxSpawnTime, spawnFrequency = 0;

    public GameObject enemyPrefab;

    public void SpawnEnemy(bool leftEnemyWasKilled)
    {
        if (spawnQueue == null)
            spawnQueue = new Queue();

        spawnQueue.Enqueue(leftEnemyWasKilled);

        if (spawnLoop == null)
            spawnLoop = StartCoroutine(SpawnLoop());
        
    }

    private IEnumerator SpawnLoop()
    {
        while (spawnQueue.Count > 0)
        {
            yield return StartCoroutine(Spawn(spawnQueue.Peek() as Nullable<bool>));
            spawnQueue.Dequeue();
        }
        spawnLoop = null;
    }

    private IEnumerator Spawn(bool? spawnLeft)
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.0f, 2.0f));

        Transform spawnPoint = spawnLeft.Value ? leftSpawn : rightSpawn;
        Transform attackPos = spawnLeft.Value ? attackPosLeft : attackPosRight;
        Debug.Log("Enemy spawned at: " + spawnPoint.name + " - Left enemy was killed = " + spawnLeft.Value);

        EnemyController enemy = Pool.instance.GetEnemyByController();
        enemy.gameObject.transform.position = spawnPoint.position;
        enemy.Init(spawnLeft.Value, attackPos.position);
        spawnCount++;
    }
}
