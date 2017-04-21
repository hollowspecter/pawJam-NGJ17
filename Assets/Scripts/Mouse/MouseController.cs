using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class MouseController : MonoBehaviour {

    [SerializeField]
    private Text m_debugText;
    [SerializeField]
    private float m_fSpeed;
    [SerializeField]
    private float m_fAxisThreshold;
    [SerializeField]
    //private float m_f

    private Rigidbody m_rigid;
    private Transform m_transform;

	void Awake()
    {
        m_rigid = GetComponent<Rigidbody>();
        m_transform = GetComponent<Transform>();
    }
	
	void Update () {
        // fetch movement from axis
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        
        // make it snappy


        Vector3 newPosition = m_transform.position + movement * Time.deltaTime * m_fSpeed;
        m_rigid.MovePosition(newPosition);

        if (m_debugText != null)
            m_debugText.text = "X: " + x + "; Y: " + y;
    }
}
