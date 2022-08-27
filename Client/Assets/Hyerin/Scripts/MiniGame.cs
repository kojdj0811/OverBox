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

        public Moru.Box pl_box;


        private void OnEnable()
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

        public void OnDisable()
        {
            if(Player.Instance)
            {
                Player.Instance.state = Player.State.Movable;
            }
        }

        private void Update()
        {

            // ���� Ű�� �����ٸ� ������մϴ�.
            if (isPlay)
            {
                time -= Time.deltaTime;
                //�ð��ȿ� �Է¸���
                if (time <= 0)
                {
                    isPlay = false;
                    time = 3f;
                    Debug.Log("�ð��ʰ�");
                    score -= 100;
                    wrongCnt++;
                    packingUI.updateSuccess(false, index);
                    if (wrongCnt < 3) StartCoroutine(Restart());
                }
                if (Input.anyKeyDown && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
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
                if (index == 7 || wrongCnt == 3)
                {
                    isPlay = false;
                    StartCoroutine(End());
                }
            }
        }

        // 7���� Ű�� �������� �����մϴ�. (�ߺ� ���)
        private void MakePattern()
        {
            pattern.Clear();
            for (int i = 0; i < 7; i++)
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
            pl_box.CompletePacking(score);
            pl_box = null;
            yield return null;
        }
    }

}