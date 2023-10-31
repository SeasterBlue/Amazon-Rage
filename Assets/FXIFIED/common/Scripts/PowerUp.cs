using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public GameObject pickupEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }

    void Pickup()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
        //Destroy(pickupEffect, 3); // puede consumir muchos recursos


        gameObject.SetActive(false);
    }
}
