using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RecyclableObject : MonoBehaviour
{
    private ObjectPool objectPool;
    public int configuredTransformIndex;

    internal void Configure(ObjectPool objPool, int configTranformIndex)
    {
        this.objectPool = objPool;
        configuredTransformIndex = configTranformIndex;
    }

    public void Recycle()
    {
        objectPool.RecycleGameObject(this);
    }
    internal abstract void Init();
    internal abstract void Release();
}
