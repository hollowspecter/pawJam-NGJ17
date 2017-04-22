using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Portal : MonoBehaviour {

    [SerializeField]
    private Transform connectedTo;

    void Awake()
    {
        Assert.IsNotNull<Transform>(connectedTo);
    }

	void OnTriggerEnter(Collider c)
    {
        // is this a mouse?
        if (c.CompareTag("Mouse"))
        {
            // teleport it!
            c.transform.position = new Vector3(connectedTo.position.x, 0f, connectedTo.position.z);

            // reset the tail
            c.GetComponentInChildren<Tail>().Reset();
        }
    }
}
