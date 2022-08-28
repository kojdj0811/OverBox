using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Hyerin;

public class PackingUI : MonoBehaviour
{
    [SerializeField] private Image[] ColorShower;
    [SerializeField] private TextMeshProUGUI[] target_Keycode;
    [SerializeField] private Color Normal_Cor;
    [SerializeField] private Color Correct_Cor;
    [SerializeField] private Color Wrong_Cor;


    /// <summary>
    /// Ű���� �ؽ�Ʈ�� �÷��� ���� ���ۻ��·� �ʱ�ȭ�մϴ�.
    /// </summary>
    /// <param name="keys"></param>
    public void Initialized(MiniGame.KEY[] keys)
    {
        for(int i = 0; i < keys.Length; i++)
        {
            ColorShower[i].color = Normal_Cor;
            target_Keycode[i].text = keys[i].ToString();
        }
    }

    /// <summary>
    /// ���°�� ���� ���ߴ��� ���ߴ��� �Ǵ��� UI Showing�� ������Ʈ�մϴ�.
    /// </summary>
    /// <param name="isCorrect"></param>
    /// <param name="idx"></param>
    public void UpdateSuccess(bool isCorrect,int idx)
    {
        if(isCorrect)
        {
            ColorShower[idx].color = Correct_Cor;
        }
        else
        {
            ColorShower[idx].color = Wrong_Cor;
        }
    }
}
