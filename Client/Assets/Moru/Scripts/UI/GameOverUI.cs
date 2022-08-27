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
        this.score.text = score.ToString() + "��";
        switch (value)
        {
            case GameValue.Perfect:
                text.text = "��� �����ڵ��� ���� ���ڸ� �޾Ҿ��!\n" +
                            "�Ϻ��� ���� ���¿� �����ںе��� ��ź�� ��ġ ���߽��ϴ�.\n" +
                            "������ ���п� ǳ���� �������� �Ǿ����~!";
                PerpectGOODGOOD();
                break;
            case GameValue.Normal:
                text.text = "��κ��� �����ڵ��� ���������� ���� ���ڸ� �޾ҽ��ϴ�!\n"+
                            "�ٸ�, ���� ���� ��ƼĿ�� �� ���� ���ڵ��� �־����.\n"+
                            "���� ���� ��� �����ڵ��� ��ڰ� ������ ���� �� �ְ���?";
                break;
            case GameValue.Bad:
                text.text = "���� ���ڸ� ���� ���� �����ڰ� �־����.\n"+
                            "��ǰ��� ���� �����ڵ��� ���� ���� �ɷ��� ���������Ƚ��ϴ�..\n"+
                            "�������� �� ��ο��� ���� ����� ������ �� �ֱ�!";
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
