using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyerin
{
    public class MiniGame : MonoBehaviour
    {
        public PackingUI packingUI;

        private bool isPlay;
        private int index;
        private int score;
        private int wrongCnt;
        private float time;
        List<KeyCode> keys = new List<KeyCode>();
        List<KeyCode> pattern = new List<KeyCode>();

        private void Start()
        {
            isPlay = true;
            index = 0;
            score = 100;
            wrongCnt = 0;
            time = 3f;
            keys.Add(KeyCode.Q);
            keys.Add(KeyCode.W);
            keys.Add(KeyCode.E);
            keys.Add(KeyCode.A);
            keys.Add(KeyCode.S);
            keys.Add(KeyCode.D);
            MakePattern();
            packingUI.gameObject.SetActive(true);
            packingUI.setKeyCodeList(pattern);
            packingUI.showKeyCode();
        }

        private void Update()
        {
            time -= Time.deltaTime;
            // ���� Ű�� �����ٸ� ������մϴ�.
            if (isPlay)
            {
                if(time <= 0)
                {
                    isPlay=false;
                    time = 3f;
                    Debug.Log("�ð��ʰ�");
                    score -= 100;
                    wrongCnt++;
                    if (wrongCnt < 3) StartCoroutine(Restart());
                }
                if (Input.anyKeyDown)
                {
                    bool is_correct = Input.GetKeyDown(pattern[index]);
                    packingUI.updateSuccess(is_correct, index);
                    index++;
                    if (!is_correct)
                    {
                        isPlay = false;
                        score -= 100;
                        wrongCnt++;
                        if (wrongCnt < 3) StartCoroutine(Restart());
                    }
                }
                if (index == 7 || wrongCnt==3)
                {
                    isPlay = false;
                    StartCoroutine(End());
                }
            }
        }

        // 7���� Ű�� �������� �����մϴ�. (�ߺ� ���)
        private void MakePattern()
        {
            for(int i=0; i<7; i++)
            {
                int rand = Random.Range(0, keys.Count);
                pattern.Add(keys[rand]);
            }
        }

        // ��� ����ٰ� �ε����� �ʱ�ȭ�Ͽ� ������ ������մϴ�.
        IEnumerator Restart()
        {
            yield return new WaitForSeconds(0.5f);
            index = 0;
            //packingUI.Restart();
            isPlay = true;
            yield return null;
        }

        // ��� ����ٰ� ������ �����մϴ�.
        IEnumerator End()
        {
            yield return new WaitForSeconds(0.5f);
            packingUI.gameObject.SetActive(false);
            Player.Instance.carryingObject.GetComponent<Moru.Box>().CompletePacking(score);
            yield return null;
        }
    }

}