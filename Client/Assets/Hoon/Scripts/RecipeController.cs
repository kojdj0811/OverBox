using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeController : MonoBehaviour
{
    public TextMeshProUGUI nameZone;
    public TextMeshProUGUI JobZone;

    public void showProducts(int[] requestList, string name = "", int job = 1)
    {
        int recipeCnt = 0;
        for(int i=0;i<6;i++)
        {
            for(int j=0;j< requestList[i];j++)  
            {
                //Debug.Log()
                var obj = transform.GetChild(recipeCnt);
                obj.GetComponent<Image>().sprite = Moru.MoruDefine.Item_Icon[i];
                obj.gameObject.SetActive(true);
                recipeCnt++;
            }
        }
        nameZone.text = name;
        string job_to_str = "";
        switch (job)
        {
            case 1:
                job_to_str = "��ȹ";
                break;
            case 2:
                job_to_str = "���α׷���";
                break;
            case 3:
                job_to_str = "�׷���";
                break;

        }

        JobZone.text = job_to_str;
    }
}
