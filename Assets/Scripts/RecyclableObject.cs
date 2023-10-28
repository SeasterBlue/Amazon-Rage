using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RecyclableObject : MonoBehaviour
{
    private ObjectPool objectPool;

    internal void Configure(ObjectPool objPool)
    {
        this.objectPool = objPool;
    }

    public void Recycle()
    {
        objectPool.RecycleGameObject(this);
    }
    internal abstract void Init();
    internal abstract void Release();
}
