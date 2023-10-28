using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ObjectPool
{
    private readonly RecyclableObject prefab;
    private readonly HashSet<RecyclableObject> instantiateObjects;
    private Queue<RecyclableObject> recycledObjects;

    public ObjectPool(RecyclableObject prefab)
    {
        this.prefab = prefab;
        instantiateObjects = new HashSet<RecyclableObject>();
    }

    public void Init(int numberOfInitialObjects)
    {
        recycledObjects = new Queue<RecyclableObject>(numberOfInitialObjects);

        for (var i = 0; i < numberOfInitialObjects; i++)
        {
            var instance = InstantiateNewInstance(Vector3.zero, Quaternion.identity);
            instance.gameObject.SetActive(false);
            recycledObjects.Enqueue(instance);
        }
    }

    private RecyclableObject InstantiateNewInstance(Vector3 position, Quaternion rotation)
    {
        var instance = Object.Instantiate(prefab, position, rotation);
        instance.Configure(this);
        return instance;
    }

    public T Spawn<T>(Vector3 position, Quaternion rotation)
    {
        var recyclableObject = GetInstance(position, rotation);
        instantiateObjects.Add(recyclableObject);
        recyclableObject.gameObject.SetActive(true);
        recyclableObject.Init();
        return recyclableObject.GetComponent<T>();
    }

    private RecyclableObject GetInstance(Vector3 position, Quaternion rotation)
    {
        if (recycledObjects.Count > 0)
        {
            var recyclableObject = recycledObjects.Dequeue();
            var transform = recyclableObject.transform;
            transform.position = position;
            transform.rotation = rotation;
            return recyclableObject;
        }

        Debug.LogWarning($"Not enough recycled objets for {prefab.name} consider increase the initial number of objets");
        var instance = InstantiateNewInstance(position, rotation);
        return instance;
    }

    public void RecycleGameObject(RecyclableObject gameObjectToRecycle)
    {
        var wasInstantiated = instantiateObjects.Remove(gameObjectToRecycle);
        Assert.IsTrue(wasInstantiated, $"{gameObjectToRecycle.name} was not instantiate on {prefab.name} pool");

        gameObjectToRecycle.gameObject.SetActive(false);
        gameObjectToRecycle.Release();
        recycledObjects.Enqueue(gameObjectToRecycle);
    }
}
