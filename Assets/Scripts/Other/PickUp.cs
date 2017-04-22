using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

	void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Mouse"))
        {
            Activate();
            Destroy(gameObject);
        }
    }

    void Activate()
    {
        // do the action here
        print("pickup");
    }
}
