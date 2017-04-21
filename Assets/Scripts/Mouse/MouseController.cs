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
    [Range(0f, 1f)]
    [SerializeField]
    private float m_fMinimumSpeedfactor = 0.5f;
    [SerializeField]
    private bool m_bMoveByForce = false;
    [SerializeField]
    private float m_fForceMultiplier = 10f;

    private Rigidbody m_rigid;
    private Transform m_transform;

	void Awake()
    {
        m_rigid = GetComponent<Rigidbody>();
        m_transform = GetComponent<Transform>();
    }
	
	void FixedUpdate () {
        // fetch movement from axis
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        bool walking = h != 0f || v != 0f;
        Move(h, v);
        Turning(h, v, walking);

        // debug
        if (m_debugText != null)
            m_debugText.text = "X: " + h + "; Z: " + v;
    }

    void Move(float h, float v)
    {
        Vector3 movement = new Vector3(h, 0f, v);
        movement.Set(h, 0f, v);
        movement = movement.normalized * m_fSpeed * Time.deltaTime;

        if (!m_bMoveByForce)
            m_rigid.MovePosition(transform.position + movement); //current pos + movement
        else
            m_rigid.AddForce(movement * m_fForceMultiplier);
    }

    void Turning(float h, float v, bool isWalking)
    {
        if (isWalking)
        {
            Vector3 movement = new Vector3(h, 0.0f, v);
            Quaternion newRotation = Quaternion.LookRotation(movement);
            m_rigid.MoveRotation(newRotation);
        }
    }
}
