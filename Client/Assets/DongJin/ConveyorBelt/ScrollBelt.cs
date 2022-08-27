using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBelt : MonoBehaviour
{
    public Material beltMat;
    public float scrollSpeed = 0.3f;
    private float textureOffsetY;

    private void OnValidate() {
        beltMat = transform.Find("Belt").GetComponent<Renderer>().sharedMaterial;
    }

    private void Update() {
        textureOffsetY += Time.deltaTime * scrollSpeed;
        beltMat.mainTextureOffset = Vector2.up * textureOffsetY;
    }
}
