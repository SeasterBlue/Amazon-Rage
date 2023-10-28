using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Lumberjack prefab;
    private ObjectPool objectPool;

    private void Awake()
    {
        objectPool = new ObjectPool(prefab);
        objectPool.Init(3);
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            objectPool.Spawn<Lumberjack>(Vector3.zero, Quaternion.identity);
        }
    }
}
