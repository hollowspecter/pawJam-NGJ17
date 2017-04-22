using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LaserController : MonoBehaviour {

    [SerializeField]
    private float m_fMovementThreshold = 0.1f;
    [SerializeField]
    private LayerMask m_layer;
    [SerializeField]
    private float m_rayLength = 20f;

    private Transform m_transformMouse;
    private Transform m_transformSpot;
    private LineRenderer m_line;
    private Transform m_transform;

    void Awake()
    {
        m_transform = GetComponent<Transform>();

        m_line = GetComponent<LineRenderer>();
        Assert.IsNotNull<LineRenderer>(m_line);
        m_transformSpot = m_transform.parent.FindChild("Spot").transform;
        Assert.IsNotNull<Transform>(m_transformSpot);

        m_line.useWorldSpace = false;
        m_line.positionCount = 2;
    }

	// Use this for initialization
	void Start () {
		
	}
	
    void FixedUpdate()
    {
        m_transform.LookAt(m_transformMouse);

        RaycastHit hit;
        if (Physics.Raycast(m_transform.position, m_transform.forward, out hit, m_rayLength, m_layer))
        {
            m_transformSpot.position = hit.point + hit.normal * 0.01f;
            m_transformSpot.rotation = Quaternion.LookRotation(-hit.normal);
            m_line.SetPosition(1, new Vector3(0f, 0f, hit.distance));
        }
        else
        {
            m_transformSpot.position = new Vector3(0f, 15f, 0f);
            m_line.SetPosition(1, new Vector3(0f, 0f, m_rayLength));
        }
    }

    public void SetMouseTransform (Transform mouse)
    {
        m_transformMouse = mouse;
    }
}
