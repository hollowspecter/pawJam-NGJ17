using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

	void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Mouse"))
        {
            StartCoroutine(Activate());
            Destroy(GetComponent<Collider>());
            Destroy(GetComponentInChildren<MeshRenderer>());
        }
    }

    IEnumerator Activate()
    {
        float time = CONSTANTS.SCRAMBLED_KEYS_TIME;
        GridCreator.Instance.assignKeys(GridCreator.Instance.scrambledKeys);
        while(time > 0.0f) {
            Debug.Log(time);
            yield return null;
            time -= Time.deltaTime;
        }
        GridCreator.Instance.assignKeys(GridCreator.Instance.baseKeys);
    }
}
