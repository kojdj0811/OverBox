using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeController : MonoBehaviour
{
    public void showProducts(int[] requestList)
    {
        int recipeCnt = 0;
        for(int i=0;i<6;i++)
        {
            for(int j=0;j< requestList[i];j++)
            {
                var obj = transform.GetChild(recipeCnt++);
                Moru.MoruDefine.itemIcon[];
                //obj.GetComponent<Image>().sprite = request.productsIMG[i];
                obj.gameObject.SetActive(true);

            }
            
        }
    }
}
