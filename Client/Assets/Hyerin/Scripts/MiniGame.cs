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
        List<KeyCode> keys = new List<KeyCode>();
        List<KeyCode> pattern = new List<KeyCode>();

        private void Start()
        {
            isPlay = true;
            index = 0;
            score = 100;
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
            // ���� Ű�� �����ٸ� ������մϴ�.
            if (isPlay)
            {
                if (Input.anyKeyDown)
                {
                    bool is_correct = Input.GetKeyDown(pattern[index++]);
                    packingUI.updateSuccess(is_correct, index);
                    if (!is_correct)
                    {
                        score -= 100;
                        StartCoroutine(Restart());
                    }
                }
                if (index == 7)
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