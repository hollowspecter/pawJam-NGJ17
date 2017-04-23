using System.Collections.Generic;
using UnityEngine;

public class DynamigGIObj : MonoBehaviour
{
    MeshRenderer meshrendererThis;
    public Color currentEmissiveColor;
    float colorSpeed = 0.2f;
    // Use this for initialization
    void Start()
    {
        meshrendererThis = GetComponent<MeshRenderer>();
        currentEmissiveColor = meshrendererThis.material.GetColor("_EmissionColor");

    }

    // Update is called once per frame
    void Update()
    {
        float h, s, v;
        Color.RGBToHSV(currentEmissiveColor, out h, out s, out v);
        h += colorSpeed * Time.deltaTime;
        h = h % 360;
        currentEmissiveColor = Color.HSVToRGB(h, s, v);
        meshrendererThis.material.SetColor("_EmissionColor", currentEmissiveColor);
        //DynamicGI.SetEmissive(meshrendererThis, currentEmissiveColor);
        RendererExtensions.UpdateGIMaterials(meshrendererThis);
    }
}