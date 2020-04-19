using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Enemy;

public class Pool : MonoBehaviour
{
    public static Pool instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public int enemyCount;
    public GameObject enemyPrefab;
    public List<EnemyController> enemies;

    public int projectileCount;
    public GameObject projectilePrefab;
    public List<Projectile> projectiles;
    
    public void InitPool()
    {
        enemies = new List<EnemyController>();

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject go = Instantiate(enemyPrefab);
            EnemyController enemy = go.GetComponent<EnemyController>();
            enemies.Add(enemy);
            enemy.OnInstantiate();
            go.SetActive(false);
        }

        projectiles = new List<Projectile>();

        for (int i = 0; i < projectileCount; i++)
        {
            GameObject go = Instantiate(projectilePrefab);
            Projectile proj = go.GetComponent<Projectile>();
            projectiles.Add(proj);
            go.SetActive(false);
        }
    }

    public GameObject GetEnemy()
    {
        var obj = enemies.Where(e => !e.gameObject.activeInHierarchy).First().gameObject;
        if (obj == null)
            Debug.LogError("No inactive enemy in pool!");
        obj.SetActive(true);

        return obj;
    }

    public EnemyController GetEnemyByController()
    {
        var obj = enemies.Where(e => !e.gameObject.activeInHierarchy).First();
        if (obj == null)
            Debug.LogError("No inactive enemy in pool!");
        obj.gameObject.SetActive(true);

        return obj;
    }

    public Projectile GetProjectile()
    {
        var obj = projectiles.Where(p => !p.gameObject.activeInHierarchy).FirstOrDefault();
        if (obj == null)
            Debug.LogError("No inactive projectiles in pool!");
        obj.gameObject.SetActive(true);

        return obj;
    }

    public void Restart()
    {
        enemies.ForEach(e => e.Restart());
        projectiles.ForEach(p => p.gameObject.SetActive(false));
    }
}
