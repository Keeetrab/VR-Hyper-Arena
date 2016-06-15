using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolScript : MonoBehaviour {
    public GameObject pooledBolt;
    public GameObject pooledDestroyedSphere;

    public int pooledBoltsAmount = 10;
    public int pooledDestroyedSpheresAmount = 7;
    public bool willGrow = true;

    public List<GameObject> pooledBolts;
    public List<GameObject> pooledDestroyedSpheres;

    void Start() {

        pooledBolts = new List<GameObject>();
        for (int i = 0; i < pooledBoltsAmount; i++) {
            GameObject obj = (GameObject)Instantiate(pooledBolt);
            obj.SetActive(false);
            pooledBolts.Add(obj);
        }

        pooledDestroyedSpheres = new List<GameObject>();
        for (int i = 0; i < pooledDestroyedSpheresAmount; i++) {
            GameObject obj = (GameObject)Instantiate(pooledDestroyedSphere);
            obj.SetActive(false);
            pooledDestroyedSpheres.Add(obj);
        }

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

    public GameObject GetPooledBolt() {
        return GetPooledObject(pooledBolts, pooledBolt);
    }

    public GameObject GetPooledDestroyedSphere() {
        return GetPooledObject(pooledDestroyedSpheres, pooledDestroyedSphere);
    }

}
