using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolScript : MonoBehaviour {
    public GameObject boltModel;
    public GameObject destroyedSphereModel;
    public GameObject sphereModel;

    public int pooledBoltsAmount = 10;
    public int pooledDestroyedSpheresAmount = 7;
    public int pooledSpheresAmount = 5;
    public bool willGrow = true;

    public List<GameObject> pooledBolts;
    public List<GameObject> pooledDestroyedSpheres;
    public List<GameObject> pooledSpheres;

    void Start() {

        pooledBolts = CreatePool(boltModel, pooledBoltsAmount);
        pooledDestroyedSpheres = CreatePool(destroyedSphereModel, pooledDestroyedSpheresAmount);
        pooledSpheres = CreatePool(sphereModel, pooledSpheresAmount);
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

    public GameObject GetPooledDestroyedSphere() {
        return GetPooledObject(pooledDestroyedSpheres, destroyedSphereModel);
    }

    public GameObject GetPooledSphere() {
        return GetPooledObject(pooledSpheres, sphereModel);
    }
}
