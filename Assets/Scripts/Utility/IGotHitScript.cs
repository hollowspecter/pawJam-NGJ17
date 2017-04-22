using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGotHitScript : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Paw")) {
            MiceManager.s_instance.CheckToStartGame();
        }
    }

}
