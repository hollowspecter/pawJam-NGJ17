using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class MouseController : MonoBehaviour {

    //[Range(1,4)]
    [SerializeField]
    private int m_iControllerNumber = 1;
    [SerializeField]
    private Text m_debugText;
    [SerializeField]
    private float m_fSpeed;
    [SerializeField]
    private bool m_bMoveByForce = false;
    [SerializeField]
    private float m_fForceMultiplier = 10f;
    [SerializeField]
    private GameObject m_prefabLaser;
    [SerializeField]
    private float laserHeight = 15f;

    private Rigidbody m_rigid;
    private Transform m_transform;
    public bool Dead { get { return dead; } }
    private bool dead = false;
    private GameObject laser;

	void Awake()
    {
        m_rigid = GetComponent<Rigidbody>();
        m_transform = GetComponent<Transform>();
    }

    void SpawnLaser()
    {
        laser = Instantiate<GameObject>(m_prefabLaser);
        laser.transform.position = new Vector3(m_transform.position.x, laserHeight, m_transform.position.z);
        laser.GetComponentInChildren<LaserController>().SetMouseTransform(m_transform);
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.CompareTag("Paw"))
        {
            Kill();
        }
    }
	
	void FixedUpdate () {
        // fetch movement from axis
        float h = Input.GetAxis("Horizontal_" + m_iControllerNumber);
        float v = Input.GetAxis("Vertical_" + m_iControllerNumber);

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

    public void Kill()
    {
        dead = true;

        SpawnLaser();

        // turn of mesh renderers and colliders
        GetComponent<MeshRenderer>().enabled = false;
        transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    public void SetPlayerNumber(int number)
    {
        m_iControllerNumber = number;
    }
}
