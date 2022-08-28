using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AutoFloor : MonoBehaviour
{
    [Title("Requirements")]
    [Required]
    public Material matFloor_0;
    [Required]
    public Material matFloor_1;

    /// <summary>
    /// x축 그리드값
    /// </summary>
    public int x;
    /// <summary>
    /// z축 그리드값
    /// </summary>
    public int z;

    public GameObject Floor;

    public void Start()
    {
        OnInitiailzed();
    }

    [ContextMenu("타일만들기")]
    public void OnInitiailzed()
    {
        GameObject floorParent = new GameObject("FloorParent");
        floorParent.transform.position = Vector3.zero;
        for(int _x = -x; _x < x; _x++)
        {
            for (int _z = -z; _z < z; _z++)
            {
                GameObject floor = Instantiate(Floor);
                    //new GameObject($"floor ({_x})({_z})");
                
                floor.transform.SetParent(floorParent.transform);
                floor.transform.position = new Vector3(_x, 0, _z);

                floor.GetComponent<Renderer>().sharedMaterial = (Mathf.Abs(_z) % 2 == Mathf.Abs(_x) % 2) ? matFloor_0 : matFloor_1;
            }
        }
    }
}
