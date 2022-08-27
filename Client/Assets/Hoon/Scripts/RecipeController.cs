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
                //Debug.Log()
                var obj = transform.GetChild(recipeCnt);
                Debug.Log(obj);
                obj.GetComponent<Image>().sprite = Moru.MoruDefine.Item_Icon[i];
                obj.gameObject.SetActive(true);
                recipeCnt++;

            }
            
        }
    }
}
