using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Lumberjack prefab;
    private ObjectPool objectPool;
    [Tooltip("Positions in the scene where the prefab clones are gonna be spawned.")]
    [SerializeField] private List<Transform> spawnPositions;

    private void Awake()
    {
        Assert.IsTrue(spawnPositions.Count > 0, "Spawn Positions has no positions @ Spawner.cs");
        objectPool = new ObjectPool(prefab, spawnPositions);
        objectPool.Init(spawnPositions.Count);
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Pause))
        {
            objectPool.Spawn<Lumberjack>();
        }
    }
}
