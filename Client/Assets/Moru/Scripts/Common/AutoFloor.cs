using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFloor : MonoBehaviour
{
    /// <summary>
    /// x�� �׸��尪
    /// </summary>
    public int x;
    /// <summary>
    /// z�� �׸��尪
    /// </summary>
    public int z;

    public GameObject Floor;

    public void Start()
    {
        OnInitiailzed();
    }

    [ContextMenu("Ÿ�ϸ����")]
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
            }
        }
    }
}
