using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
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
    
    private MeshRenderer m_rendererMouseColor;
    private MeshRenderer m_rendererTurnKeyColor;
    private Rigidbody m_rigid;
    private Transform m_transform;
    public bool Dead { get { return dead; } }
    private bool dead = false;
    private GameObject laser;
    private float h = 0f, v = 0f;

    public static int MiceAlive { get { return miceAlive; } }
    private static int miceAlive = 0;

    private Vector3 movement = Vector3.zero;

	void Awake()
    {
        m_rigid = GetComponent<Rigidbody>();
        m_transform = GetComponent<Transform>();
        miceAlive++;

        m_rendererMouseColor = transform.GetChild(0).FindChild("mouse").GetComponent<MeshRenderer>();
        m_rendererTurnKeyColor = transform.GetChild(0).FindChild("turn_key").GetComponent<MeshRenderer>();
        Assert.IsNotNull<MeshRenderer>(m_rendererMouseColor);
        Assert.IsNotNull<MeshRenderer>(m_rendererTurnKeyColor);
    }

    void SpawnLaser()
    {
        laser = Instantiate<GameObject>(m_prefabLaser);
        laser.transform.position = new Vector3(m_transform.position.x, laserHeight, m_transform.position.z);
        laser.GetComponentInChildren<LaserController>().SetMouseTransform(m_transform);
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.CompareTag("Mouse"))
        {
            //bump!
            c.rigidbody.AddForce(transform.forward * m_fForceMultiplier);
        }
        else if (c.collider.CompareTag("Paw"))
        {
            Kill();
        }
    }
	
    void Update()
    {
        // fetch movement from axis
        h = Input.GetAxisRaw("Horizontal_" + m_iControllerNumber);
        v = Input.GetAxisRaw("Vertical_" + m_iControllerNumber);
    }

	void FixedUpdate () {

        bool walking = h != 0f || v != 0f;
        Move(h, v);
        Turning(h, v, walking);

        // debug
        if (m_debugText != null)
            m_debugText.text = "X: " + h + "; Z: " + v;
    }

    void Move(float h, float v)
    {
        movement = new Vector3(h, 0f, v);
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
        miceAlive--;
        GameManager.Instance.MouseDied();

        if (miceAlive > 0)
        {
            SpawnLaser();
            // turn of mesh renderers and colliders
            //GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            // LAST MOUSE GOT KILLED, DO NOT TURN INTO A LAZOR
            //print("Last Mouse got killed");
        }
    }

    public void SetPlayerNumber(int number)
    {
        m_iControllerNumber = number;

        Material mat = Resources.Load<Material>("Materials/mouse/colors/col" + number);
        //if (mat == null) print("couldnt load");
        m_rendererMouseColor.material = mat;
        Material[] mats = m_rendererTurnKeyColor.materials;
        mats[1] = mat;
        m_rendererTurnKeyColor.materials = mats;
    }
}
