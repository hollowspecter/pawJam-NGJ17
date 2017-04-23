using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMusic : MonoBehaviour {

    public static SingleMusic Instance;
	// Use this for initialization
	void Awake () {
        if (Instance == null) Instance = this;
        else
            Destroy(this.gameObject);
	}
}
