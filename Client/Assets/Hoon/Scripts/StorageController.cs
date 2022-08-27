using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StorageController : MonoBehaviour
{
    // Start is called before the first frame update
    public int[] productCnt = new int[6];

    private void Start()
    {
        updateProductCnt(productCnt);
    }
    public void updateProductCnt(int[] pCnt)
    {
        for(int i=0;i<6;i++)
        {
            transform.GetChild(i).Find("ProductCnt").GetComponent<TextMeshProUGUI>().text = pCnt[i].ToString();
        }
    }
}
