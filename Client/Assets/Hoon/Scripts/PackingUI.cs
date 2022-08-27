using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackingUI : MonoBehaviour
{
    public List<KeyCode> keyCodeList;
    public Transform settingPos;
    private int maxCode = 7;
    private Image[] checkSuccessColor;

    private void OnEnable()
    {
        settingPos = transform.Find("AuditionPanel");
        checkSuccessColor = new Image[maxCode];
        for (int i=0;i<maxCode;i++)
        {
            checkSuccessColor[i] = settingPos.GetChild(i).GetChild(1).GetComponent<Image>();
            checkSuccessColor[i].color = new Color32(0, 0, 0, 0);
        }
    }



    public void setKeyCodeList(List<KeyCode> list)
    {
        keyCodeList.Clear();
        foreach (var code in list)
            keyCodeList.Add(code);
    }

    public void showKeyCode()
    {
        for(int i=0;i<maxCode;i++)
        {
            settingPos.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = keyCodeList[i].ToString();
        }
    }

    public void updateSuccess(bool isCorrect,int idx)
    {
        if(isCorrect)
        {
            settingPos.GetChild(idx).GetChild(1).GetComponent<Image>().color = new Color32(0, 255, 0, 100);
        }
        else
        {
            settingPos.GetChild(idx).GetChild(1).GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            StartCoroutine(stayAWhile());
        }
    }

    IEnumerator stayAWhile()
    {
        yield return new WaitForSeconds(1.0f);
        for(int i=0;i<maxCode;i++)
        {
            checkSuccessColor[i].color= new Color32(0, 0, 0, 0);
        }
        

    }
}
