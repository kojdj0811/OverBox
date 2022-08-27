using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{

    public int minute;
    public int second;
    private bool isOver;

    public void tik()
    {
        if (minute == 0)
            isOver = true;
        else
            minute--;
    }

    public void tok()
    {
        if (second == 0)
        {
            tik();
            if (isOver) return;
            second = 59;
        }
        else second--;
    }

    public float deliveryLimit;

    private void Start()
    {
        isOver = false;
        StartCoroutine(tiktok());

    }

    IEnumerator tiktok()
    {
        var timeOutIMG = transform.Find("DeliveryLimit").GetComponent<Image>();
        while (!isOver)
        {
            tok();
            transform.Find("TimeCnt").GetComponent<TextMeshProUGUI>().text = minute.ToString() + " : " + second.ToString();
            if (timeOutIMG.fillAmount == 1)
                timeOutIMG.fillAmount = 0;
            timeOutIMG.fillAmount += 1/ deliveryLimit;
            Debug.Log(deliveryLimit);
            yield return new WaitForSeconds(1.0f);
        }
       
    }

}
