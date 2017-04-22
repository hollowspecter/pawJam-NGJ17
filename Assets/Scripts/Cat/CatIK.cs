using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatIK : MonoBehaviour {

    [SerializeField]
    private bool ikActive = false;
    [SerializeField]
    private Transform head = null;
    [SerializeField]
    private Transform lookObj = null;

    private Animator m_anim;

    void Awake()
    {
        m_anim = GetComponent<Animator>();
    }
}
