using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public Text text;
    public Text score;
    bool isClick = false;
    public Animator anim;
    public enum GameValue { Perfect, Normal, Bad }
    public void SetGameOverView(GameValue value, int score)
    {
        isClick = false;
        this.score.text = score.ToString() + "점";
        switch (value)
        {
            case GameValue.Perfect:
                text.text = "모든 참가자들이 보급 상자를 받았어요!\n" +
                            "완벽한 포장 상태에 참가자분들이 감탄을 금치 못했습니다.\n" +
                            "포포님 덕분에 풍족한 게임잼이 되었어요~!";
                PerpectGOODGOOD();
                break;
            case GameValue.Normal:
                text.text = "대부분의 참가자들이 만족스러운 보급 상자를 받았습니다!\n"+
                            "다만, 개봉 금지 스티커가 안 붙은 상자들이 있었어요.\n"+
                            "다음 번엔 모든 참가자들이 기쁘게 보급을 받을 수 있겠죠?";
                break;
            case GameValue.Bad:
                text.text = "보급 상자를 받지 못한 참가자가 있었어요.\n"+
                            "상실감을 느낀 참가자들의 게임 개발 능률이 떨어져버렸습니다..\n"+
                            "다음에는 꼭 모두에게 상자 배달을 성공할 수 있길!";
                break;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnClick_ESC();
        }
    }

    public void OnClick_ESC()
    {
        if(!isClick)
        {
            isClick = true;
            SceneManager.LoadScene(0);
        }
    }
    private void PerpectGOODGOOD()
    {
        anim.Play("Perfect");
    }


}
