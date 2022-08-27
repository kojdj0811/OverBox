using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(UiGlowScript))]
[RequireComponent(typeof(Selectable))]
public class UISelectable : MonoBehaviour
{
    public Selectable myComp;
    public UiGlowScript glow;
    public Graphic[] targetGraphics;

    private void Awake()
    {
        myComp = GetComponent<Selectable>();
        glow = GetComponent<UiGlowScript>();
        glow.glowEnable = true;
    }

    public void OnSelectedCallback()
    {
        foreach (var target in targetGraphics)
        {
            target.material = glow.mat;
        }
    }

    public void OnDiselecedCallback()
    {
        foreach (var target in targetGraphics)
        {
            target.material = null;
        }
    }


}
