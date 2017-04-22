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
    public GameObject paw;
    float pawHeight;

	// Use this for initialization
	void Start () {
        m_meshR = this.GetComponent<MeshRenderer>();
        m_meshR.material.color = basic;
        paw = GameObject.FindGameObjectWithTag("Paw");
        pawHeight = paw.transform.position.y;
	}  
	
	// Update is called once per frame
	void Update () {
        if(!string.IsNullOrEmpty(triggerKey)) {
            if (Input.GetKeyDown(triggerKey)) {
                paw.transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
                m_meshR.material.color = lighted;

            }
            if(Input.GetKeyUp(triggerKey)) {
                paw.transform.position = new Vector3(transform.position.x, pawHeight, transform.position.z);
                m_meshR.material.color = basic;

            }
        }   
	}


}
