using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ObjectPool
{
    /// <summary>
    ///Object to instantiate, must be a recyclable object.
    /// </summary>
    private readonly RecyclableObject prefab;

    /// <summary>
    /// Objects that are currently active in scene
    /// </summary>
    private readonly HashSet<RecyclableObject> instantiatedObjects;

    /// <summary>
    /// Objects that are instantiated but not active in scene
    /// </summary>
    private Queue<RecyclableObject> recycledObjects;
    
    private List<Transform> transformsToSpawn;
    private List<bool> activePlaces;
    private int occupiedPlaces;

    public ObjectPool(RecyclableObject prefab, List<Transform> placesToSpawn)
    {
        this.prefab = prefab;
        transformsToSpawn = placesToSpawn;
        instantiatedObjects = new HashSet<RecyclableObject>();
        activePlaces = new List<bool>();
        for(int i = 0; i < placesToSpawn.Count; i++)
            activePlaces.Add(false);
        occupiedPlaces = 0;
    }

    /// <summary>
    /// Initialize the object pool instatiating (n) objects
    /// </summary>
    /// <param name="numberOfInitialObjects">amount (n) of objects to instantiate </param>
    public void Init(int numberOfInitialObjects)
    {
        recycledObjects = new Queue<RecyclableObject>(numberOfInitialObjects);

        for (var i = 0; i < numberOfInitialObjects; i++)
        {
            int place = getFreePosToSpawn();
            var instance = InstantiateNewInstance(place);
            instance.gameObject.SetActive(false);
            recycledObjects.Enqueue(instance);
        }
    }

    /// <summary>
    /// Creates a new instance at the specified transform
    /// </summary>
    /// <param name="transformIndex">index of the transform at transformsToSpawn to use at instantiating</param>
    /// <returns>The created instance</returns>
    private RecyclableObject InstantiateNewInstance(int transformIndex)
    {
        var trans = transformsToSpawn[transformIndex];
        var instance = Object.Instantiate(prefab, trans.position, trans.rotation);
        instance.Configure(this, transformIndex);
        return instance;
    }

    /// <summary>
    /// Sets a created or new object as active and initializes it.
    /// </summary>
    /// <returns>The RecyclableObject Component of the spawned object</returns>
    public T Spawn<T>()
    {
        int pos = getFreePosToSpawn();
        var recyclableObject = GetInstance(pos);
        instantiatedObjects.Add(recyclableObject);
        recyclableObject.gameObject.SetActive(true);
        recyclableObject.Init();
        activePlaces[pos] = true;
        occupiedPlaces++;
        return recyclableObject.GetComponent<T>();
    }

    /// <summary>
    /// Retrieves an already created object, if there are no created objects create a new one.
    /// Then sets a new position an rotation to the object.
    /// The object is not initialized.
    /// </summary>
    /// <param name="toSetTransformIndex">Index of the transform at transformsToSpawn that will be used fot the object </param>
    /// <returns>The created and unitialized object</returns>
    private RecyclableObject GetInstance(int toSetTransformIndex)
    {
        if (recycledObjects.Count > 0)
        {
            var recyclableObject = recycledObjects.Dequeue();
            recyclableObject.configuredTransformIndex = toSetTransformIndex;
            recyclableObject.transform.position = transformsToSpawn[toSetTransformIndex].position;
            recyclableObject.transform.rotation = transformsToSpawn[toSetTransformIndex].rotation;
            return recyclableObject;
        }

        Debug.LogWarning($"Not enough recycled objets for {prefab.name} consider increase the initial number of objets");
        var instance = InstantiateNewInstance(toSetTransformIndex);
        return instance;
    }

    /// <summary>
    /// Sets the received gameObject as not active and ready to be recycled
    /// </summary>
    /// <param name="gameObjectToRecycle">Active gameObject to be recycled</param>
    public void RecycleGameObject(RecyclableObject gameObjectToRecycle)
    {
        var wasInstantiated = instantiatedObjects.Remove(gameObjectToRecycle);
        Assert.IsTrue(wasInstantiated, $"{gameObjectToRecycle.name} was not instantiate on {prefab.name} pool");

        gameObjectToRecycle.gameObject.SetActive(false);
        activePlaces[gameObjectToRecycle.configuredTransformIndex] = false;
        occupiedPlaces--;
        gameObjectToRecycle.Release();
        recycledObjects.Enqueue(gameObjectToRecycle);
    }

    /// <summary>
    /// Retrieves a random index of a free position at activePlaces
    /// </summary>
    /// <returns>index of activePlaces where the object is false</returns>
    private int getFreePosToSpawn()
    {
        Assert.IsTrue(occupiedPlaces >= 0 && occupiedPlaces < activePlaces.Count, "Not available spaces for spawning.");
        int pos = 0;
        List<int> indexes = new List<int>();
        for(int i = 0; i < activePlaces.Count; i++)
        {
            if(!activePlaces[i])
                indexes.Add(i);
        }
        int rand = Random.Range(0, indexes.Count);
        pos = indexes[rand];
        return pos;
    }
}
