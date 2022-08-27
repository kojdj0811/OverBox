using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using UnityEngine.UI;


public class TimeLimit
{
    public int minute;
    public int second;
    public bool isOver;

    public TimeLimit(float startTime)
    {
        isOver = false;
        minute = (int)(startTime / 60.0f);
        second = (int)startTime % 60;
    }
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
}

public class UITimer : MonoBehaviour
{

    public TimeLimit timeLimit;
    private float deliveryLimit;

    private void Start()
    {

        timeLimit = new TimeLimit(Moru.GameManager.Instance.startPlayTime);
        deliveryLimit = Moru.GameManager.Instance.delivery_Time;
        StartCoroutine(tiktok());

    }

    IEnumerator tiktok()
    {
        var timeOutIMG = transform.Find("DeliveryLimit").GetComponent<Image>();
        while (!timeLimit.isOver)
        {
            timeLimit.tok();
            transform.Find("TimeCnt").GetComponent<TextMeshProUGUI>().text = timeLimit.minute.ToString() + " : " + timeLimit.second.ToString();
            if (timeOutIMG.fillAmount == 1)
                timeOutIMG.fillAmount = 0;
            timeOutIMG.fillAmount += 1 / deliveryLimit;
            yield return new WaitForSeconds(1.0f);
        }

    }

}
