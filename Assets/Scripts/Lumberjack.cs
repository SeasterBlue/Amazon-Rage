using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Lumberjack : RecyclableObject
{
    private int health;

    internal override void Init()
    {
        health = 100;
        Invoke(nameof(Recycle), 5);
    }

    internal override void Release()
    {
        // TODO
    }
}
