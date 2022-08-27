using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage)), ExecuteAlways]
public class UiGlowScript : MonoBehaviour
{
    public bool glowEnable;

    [Range(0.0f, 1.0f)]
    public float glowIntensity;

    [Range(0.0f, 50.0f)]
    public float glowTichkness;

    public Color glowColor;

    private Material mat;


    private void OnEnable() {
        if(mat == null) {
            mat = new Material(Shader.Find("Hidden/UiGlowShader"));
            GetComponent<RawImage>().material = mat;
        }
    }

    void Update()
    {
        mat.SetFloat("_GlowEnable", glowEnable ? 1.0f : 0.0f);
        mat.SetFloat("_GlowIntensity", glowIntensity);
        mat.SetFloat("_GlowThickness", glowTichkness);
        mat.SetColor("_GlowColor", glowColor);
    }
}
