using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(LineRenderer))]
public class Tail : MonoBehaviour {

    [SerializeField]
    private int m_iNumberOfTailpoints = 20;
    [Range(0.01f, 1f)]
    [SerializeField]
    private float m_fMinimumDistance = 0.2f;

    private LineRenderer m_line;
    private FixedSizeQueue<Vector3> m_points;
    private Vector3 m_lastPoint = Vector3.zero;
    private Transform m_transform;

    void Awake()
    {
        m_points = new FixedSizeQueue<Vector3>(m_iNumberOfTailpoints);
        m_line = GetComponent<LineRenderer>();
        Assert.IsNotNull<LineRenderer>(m_line);
        m_transform = transform;
    }
	
	void Update ()
    {
		if (Vector3.Distance(m_lastPoint,m_transform.position) > m_fMinimumDistance)
        {
            m_lastPoint = m_transform.position;
            m_points.Enqueue(m_lastPoint);
            m_line.positionCount = m_points.Count;
            m_line.SetPositions(m_points.ToArray());
        }
	}
}
