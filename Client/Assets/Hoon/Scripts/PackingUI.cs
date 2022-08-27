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
    private int sequence = 0;

    private void Awake()
    {
        settingPos = transform.Find("AuditionPanel");
    }
    // Start is called before the first frame update
    private void Start()
    {

    }
    private void Update()
    {
        
    }

    public void setKeyCodeList(List<KeyCode> list)
    {
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

    public void updateSuccess(bool isCorrect)
    {
        if(isCorrect)
        {
            settingPos.GetChild(sequence++).GetChild(1).GetComponent<Image>().color = new Color32(0, 255, 0, 100);
        }
        else
        {
            settingPos.GetChild(sequence++).GetChild(1).GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            StartCoroutine(stayAWhile());
        }
    }

    IEnumerator stayAWhile()
    {
        yield return new WaitForSeconds(1.0f);

    }
}