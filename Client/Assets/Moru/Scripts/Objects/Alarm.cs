using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alarm : MonoBehaviour
{
    Canvas canvas;
    public Transform Looker;
    public Image Icon;
    public Vector3 Offset;
    protected void Awake()
    {
        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
        }
        canvas.worldCamera = Camera.main;
        canvas.sortingOrder = 15;

    }

    void Update()
    {
        LookAtCam();
    }

    [ContextMenu("ī�޶� �ٶ󺸰� �ϱ�")]
    protected void LookAtCam()
    {
        Looker.LookAt(Camera.main.transform);
    }

    public virtual void Alarming(Sprite sprite, Transform targetTransform)
    {
        this.gameObject.SetActive(true);
        Icon.sprite = sprite;
        this.transform.position = targetTransform.position + Offset;
    }
}
