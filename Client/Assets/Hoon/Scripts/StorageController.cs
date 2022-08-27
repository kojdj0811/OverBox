using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StorageController : MonoBehaviour
{
    private void Start()
    {
        updateProductCnt();
        Moru.MoruDefine.delegate_UpdateStorage += updateProductCnt;
    }
    public void updateProductCnt()
    {
        
        for(int i=0;i<(int)Moru.MoruDefine.Product.MAX;i++)
        {
            transform.GetChild(i).Find("ProductCnt").GetComponent<TextMeshProUGUI>().text = Moru.GameManager.Instance.storageBox[(Moru.MoruDefine.Product)i].CurSavedCount.ToString();
        }

    }
}
