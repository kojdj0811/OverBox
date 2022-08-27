using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class FloorGenerator : MonoBehaviour
{
    [Title("Requirements")]
    [Required]
    public GameObject pfFloor;
    [Required]
    public Material matFloor_0;
    [Required]
    public Material matFloor_1;


    [Title("Floor Size")]
    [MinValue(2)]
    public int width;
    [MinValue(2)]
    public int height;


    [Button("Generate Floor")]
    public void GenerateFloor () {
        foreach (var item in GetComponentsInChildren<Renderer>())
        {
            DestroyImmediate(item.gameObject);
        }

        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                GameObject goFloor = Instantiate(pfFloor);
                goFloor.name = $"floor_{x},{y}";

                Transform transFloor = goFloor.transform;
                transFloor.SetParent(transform);

                transFloor.localPosition = new Vector3(width * 0.5f - x - 0.5f, 0.0f, height * 0.5f - y - 0.5f);
                transFloor.localRotation = Quaternion.identity;
                transFloor.localScale = Vector3.one;

                transFloor.GetComponent<Renderer>().sharedMaterial = (y%2 == x%2) ? matFloor_0 : matFloor_1;
            }
        }
    }
}
