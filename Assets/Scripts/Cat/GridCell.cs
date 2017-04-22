using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour {

    [SerializeField]
    private Color lighted;
    [SerializeField]
    private Color basic;
    private MeshRenderer m_meshR;

    public string triggerKey { get; set; }

	// Use this for initialization
	void Start () {
        m_meshR = this.GetComponent<MeshRenderer>();
        m_meshR.material.color = basic;
	}  
	
	// Update is called once per frame
	void Update () {
        if(!string.IsNullOrEmpty(triggerKey)) {
            if (Input.GetKeyDown(triggerKey)) {
                m_meshR.material.color = lighted;
            }
            if(Input.GetKeyUp(triggerKey)) {
                m_meshR.material.color = basic;
            }
        }   
	}


}
