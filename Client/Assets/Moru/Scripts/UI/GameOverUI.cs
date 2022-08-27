using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameOverUI : MonoBehaviour
{
    public Text text;
    public TextMeshProUGUI result_Text;
    public Text score;
    bool isClick = false;
    public Animator anim;
    public Image Simbol;
    public List<Sprite> simbol_sprite;

    public enum GameValue { Perfect, Normal, Bad }
    public void SetGameOverView(GameValue value, int score)
    {
        isClick = false;
        this.score.text = score.ToString() + "��";
        switch (value)
        {
            case GameValue.Perfect:
                result_Text.text = "Happy Ending";
                text.text = "��� �����ڵ��� ���� ���ڸ� �޾Ҿ��!\n" +
                            "�Ϻ��� ���� ���¿� �����ںе��� ��ź�� ��ġ ���߽��ϴ�.\n" +
                            "������ ���п� ǳ���� �������� �Ǿ����~!";
                Simbol.sprite = simbol_sprite[0];
                break;
            case GameValue.Normal:
                result_Text.text = "Normal Ending";
                text.text = "��κ��� �����ڵ��� ���������� ���� ���ڸ� �޾ҽ��ϴ�!\n"+
                            "�ٸ�, ���� ���� ��ƼĿ�� �� ���� ���ڵ��� �־����.\n"+
                            "���� ���� ��� �����ڵ��� ��ڰ� ������ ���� �� �ְ���?";
                Simbol.sprite = simbol_sprite[1];
                break;
            case GameValue.Bad:
                result_Text.text = "Bad Ending";
                text.text = "���� ���ڸ� ���� ���� �����ڰ� �־����.\n"+
                            "��ǰ��� ���� �����ڵ��� ���� ���� �ɷ��� ���������Ƚ��ϴ�..\n"+
                            "�������� �� ��ο��� ���� ����� ������ �� �ֱ�!";
                Simbol.sprite = simbol_sprite[2];
                break;
        }
        PerpectGOODGOOD();

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
