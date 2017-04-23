using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEvent {
    public delegate void MouseDiedEvent(GameObject go);
    public static event MouseDiedEvent OnMouseDeath;

    public static void Send(GameObject go) {
        if(null != go && OnMouseDeath != null) {
            OnMouseDeath(go);
        }
    }

}
