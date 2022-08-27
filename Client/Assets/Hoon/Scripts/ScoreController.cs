using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        updateScore();
        Moru.MoruDefine.delegate_UpdateScore += updateScore;
    }

    public void updateScore()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Moru.GameManager.curScore.ToString();
    }
}
