using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell :MonoBehaviour {

    [SerializeField]
    private Color lighted;
    [SerializeField]
    private Color basic;
    [SerializeField]
    private int bpm_forGlow = 117;
    [SerializeField]
    private Vector2 minMaxEmission = new Vector2(0.2f, 1f);
    [SerializeField]
    private AnimationCurve curve;

    private MeshRenderer m_meshR;
    public string triggerKey { get; set; }
    public GameObject paw;
    float pawHeight;
    private float emissionFactor;

    // Use this for initialization
    void Start() {
        m_meshR = this.GetComponent<MeshRenderer>();
        m_meshR.material.color = basic;
        paw = GameObject.FindGameObjectWithTag("Paw");
        pawHeight = paw.transform.position.y;
        emissionFactor = minMaxEmission.x;
    }

    // Update is called once per frame
    void Update() {
        CalculateCurrentEmissionFactor();
        HandleKeyInput();
    }

    //private float timer = 0f;
    void CalculateCurrentEmissionFactor()
    {
        //timer += Time.deltaTime;
        emissionFactor = Mathf.PingPong(Time.time * bpm_forGlow / 60f, 1f);
        emissionFactor = curve.Evaluate(emissionFactor);
        emissionFactor = Mathf.Lerp(minMaxEmission.x, minMaxEmission.y, emissionFactor);
        //emissionFactor = Mathf.PingPong(Time.time * bpm_forGlow/60f, minMaxEmission.y - minMaxEmission.x) + minMaxEmission.x;
        m_meshR.material.SetColor("_EmissionColor", basic * emissionFactor);
        RendererExtensions.UpdateGIMaterials(m_meshR);
    }

    void HandleKeyInput()
    {
        if (!string.IsNullOrEmpty(triggerKey))
        {
            if (Input.GetKeyDown(triggerKey))
            {
                KeyEvent.Send(this, true);
            }
            else if (Input.GetKeyUp(triggerKey))
            {
                KeyEvent.Send(this, false);
            }
            // manage lighting
            if (Input.GetKey(triggerKey))
            {
                m_meshR.material.SetColor("_EmissionColor", lighted * emissionFactor);
                RendererExtensions.UpdateGIMaterials(m_meshR);
            }
        }
    }
}
