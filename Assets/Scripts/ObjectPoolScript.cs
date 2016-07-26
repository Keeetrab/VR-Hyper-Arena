using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolScript : MonoBehaviour {
    public GameObject boltModel;
    public GameObject enemyBoltModel;
    public GameObject explosionModel;
    public GameObject hitEffectModel;
    public GameObject enemyModel;
    public GameObject flyingEnemyModel;
    public GameObject scoreCanvasModel;

    public int pooledBoltsAmount = 10;
    public int pooledEnemyBoltsAmount = 5;
    public int pooledExplosionsAmount = 7;
    public int pooledHitEffectsAmount = 4;
    public int pooledeEnemiesAmount = 5;
    public int pooledFlyingEnemiesAmount = 5;
    public int pooledScoreCanvasesAmount = 5;
    public bool willGrow = true;

    public List<GameObject> pooledBolts;
    public List<GameObject> pooledEnemyBolts;
    public List<GameObject> pooledExplosions;
    public List<GameObject> pooledHitEffects;
    public List<GameObject> pooledEnemies;
    public List<GameObject> pooledFlyingEnemies;
    public List<GameObject> pooledScoreCanvases;
 
    void Start() {

        pooledBolts = CreatePool(boltModel, pooledBoltsAmount);
        pooledEnemyBolts = CreatePool(enemyBoltModel, pooledEnemyBoltsAmount);
        pooledExplosions = CreatePool(explosionModel, pooledExplosionsAmount);
        pooledEnemies = CreatePool(enemyModel, pooledeEnemiesAmount);
        pooledFlyingEnemies = CreatePool(flyingEnemyModel, pooledFlyingEnemiesAmount);
        pooledHitEffects = CreatePool(hitEffectModel, pooledHitEffectsAmount);
        pooledScoreCanvases = CreatePool(scoreCanvasModel, pooledScoreCanvasesAmount);
    }

    GameObject GetPooledObject(List<GameObject> pooledObjects, GameObject pooledObject) {
        for (int i = 0; i < pooledObjects.Count; i++) {
            if (pooledObjects[i] == null) {
                GameObject obj = (GameObject)Instantiate(pooledObject);
                obj.SetActive(false);
                pooledObjects[i] = obj;
                return pooledObjects[i];
            }
            if (!pooledObjects[i].activeInHierarchy) {
                return pooledObjects[i];
            }
        }

        if (willGrow) {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            pooledObjects.Add(obj);
            return obj;
        }

        return null;
    }

    private List<GameObject> CreatePool (GameObject model, int amount) {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < amount; i++) {
            GameObject obj = (GameObject)Instantiate(model);
            obj.SetActive(false);
            list.Add(obj);
        }
        return list;
    }


    public GameObject GetPooledBolt() {
        return GetPooledObject(pooledBolts, boltModel);
    }

    public GameObject GetPooledEnemyBolt() {
        return GetPooledObject(pooledEnemyBolts, enemyBoltModel);
    }

    public GameObject GetPooledExplosion() {
        return GetPooledObject(pooledExplosions, explosionModel);
    }

    public GameObject GetPooledHitEffect() {
        return GetPooledObject(pooledHitEffects, hitEffectModel);
    }

    public GameObject GetPooledEnemy() {
        return GetPooledObject(pooledEnemies, enemyModel);
    }

    public GameObject GetFlyingEnemy() {
        return GetPooledObject(pooledFlyingEnemies, flyingEnemyModel);
    }

    public GameObject GetScoreCanvas() {
        return GetPooledObject(pooledScoreCanvases, scoreCanvasModel);
    }

    public void DisableAllEnemies() {
        foreach(GameObject enemy in pooledEnemies) {
            enemy.SetActive(false);
        }

        foreach(GameObject enemy in pooledFlyingEnemies) {
            enemy.SetActive(false);
        }
    }
}
