using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hyerin
{
    public class MiniGame : MonoBehaviour
    {
        public PackingUI packingUI;

        /// <summary>
        /// �̴ϰ��ӿ� ���� Ű�̸� ���ÿ� ������ �ؽ�Ʈ�Դϴ�.
        /// </summary>
        public enum KEY { Q, W, E, A, S, D, MAX }

        /// <summary>
        /// �̴ϰ����� �÷��������� üũ�ϴ� �Ҹ��Դϴ�.
        /// </summary>
        private bool isPlay;

        /// <summary>
        /// �迭���� ���°�� Ű�Է��� �Ǵ��ϱ� ���� ��ġ adress
        /// </summary>
        private int index;

        /// <summary>
        /// �߸� ������ ������ ����۵� Ƚ���Դϴ�. 0~2 (�ִ� 3ȸ �����)
        /// </summary>
        private int wrongCnt;

        private const int MaxWrongCnt = 3;

        /// <summary>
        /// ����ð�
        /// </summary>
        [SerializeField] private float cur_Time;

        /// <summary>
        /// �� ����Ŭ�� �ִ� �÷��̰��ɽð�
        /// </summary>
        [SerializeField] private const float Maxtime = 3f;

        /// <summary>
        /// 0~6�� �ε��������� �������� �ִ� ���� ����
        /// </summary>
        [SerializeField] private const int MaxLevel = 7;

        //List<KeyCode> cur_pattern = new List<KeyCode>(MaxLevel);
        KEY[] cur_pattern = new KEY[MaxLevel];

        public Image slider;
        public Moru.Box pl_box;


        private void OnEnable()
        {
            isPlay = true;
            index = 0;
            wrongCnt = 0;
            cur_Time = 0f;

            Init_Pattern();
            packingUI.Initialized(cur_pattern);
        }

        private void Start()
        {
        }

        public void OnDisable()
        {
            if (Player.Instance)
            {
                Player.Instance.state = Player.State.Movable;
            }
        }

        private void Update()
        {
            slider.fillAmount = (Maxtime-cur_Time) / Maxtime;
            // ���� Ű�� �����ٸ� ������մϴ�.
            /*old
            if (isPlay)
            {
                cur_Time -= Time.deltaTime;
                //�ð��ȿ� �Է¸���
                if (cur_Time <= 0)
                {
                    isPlay = false;
                    cur_Time = Maxtime;
                    Debug.Log("�ð��ʰ�");
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
            */
            ///new
            ///
            if (isPlay)
            {
                //�÷����߿��� �ð��� �帨�ϴ�.
                cur_Time += Time.deltaTime;
                //�÷������� ���� (�ٸ� Ű�Է¿� �������� �ʽ��ϴ�.)
                if (GetPressKeyValue(out KEY curKEY))
                {
                    //�ߴ��� ���̽�
                    if(cur_pattern[index].Equals(curKEY))
                    {
                        //UI�� �� �����ٰ� ������Ʈ�ϱ�
                        packingUI.UpdateSuccess(true, index);
                        index++;
                    }
                    //�߸� ���� ���̽�
                    else
                    {
                        //�÷��� ����
                        packingUI.UpdateSuccess(false, index);
                        isPlay = false;
                        TryRestart();
                    }
                }


                //�ð��ȿ� �Է¸���
                if (cur_Time >= Maxtime)
                {
                    //�÷������� �ƴ�
                    isPlay = false;
                    TryRestart();
                }
                //�ð��ȿ� ��� �����µ� ������
                else if (cur_Time < Maxtime && index == MaxLevel)
                {
                    //�÷������� �ƴ�
                    isPlay = false;
                    //���� ����
                    Exit_Minigame();
                }
            }
        }

        /// <summary>
        /// MAXLEVEL��ŭ�� ������ Ű�ڵ�迭�� �����մϴ�.
        /// </summary>
        private void Init_Pattern()
        {
            string deb_str = "";
            for (int i = 0; i < 7; i++)
            {
                int rand = Random.Range(0, (int)KEY.MAX);
                cur_pattern[i] = (KEY)rand;
                deb_str += (KEY)rand;
            }
            Debug.Log($"���� ���� : {deb_str}");
        }

        /// <summary>
        /// �÷��̾ �絵���� �õ��մϴ�.
        /// wrongcount�� �ʰ��� ��� �絵���� �õ����� ���ϰ� �̴ϰ����� ����˴ϴ�.
        /// </summary>
        /// <returns></returns>
        private void TryRestart()
        {
            //�߸��߾��
            wrongCnt++;
            //�ִ� �õ����� �����ߴٸ� ���� �����Ʈ
            if (wrongCnt >= MaxWrongCnt)
            {
                Exit_Minigame();
            }
            else
            {
                //�Ϻ� ���ӷ����� �ʱ�ȭ�ϰ� ����ŸƮ�մϴ�.
                //���� �ڽ��� ��ġ�� �ε����� �� ������ �����ϴ�.
                index = 0;
                cur_Time = 0;
                //�ణ�� ���ð��� �����ϴ�.
                StartCoroutine(Restart());
            }
        }

        /// <summary>
        /// �̴ϰ����� �����մϴ�.
        /// </summary>
        private void Exit_Minigame()
        {
            //�÷��̾��� �ڽ� ������Ʈ�� �ʱ�ȭ�մϴ�.
            pl_box.CompletePacking(wrongCnt);
            //�÷��̾ �ڽ��� �����������Ƿ� null�� �ٲ�
            pl_box = null;
            StartCoroutine(End());
        }

        /// <summary>
        /// �÷��̾ � Ű�� �������� Ȯ���մϴ�. �ùٸ� Ű�� ������ true, �ƴϸ� fale, � Ű�� �������� key_value�� ��ȯ�˴ϴ�.
        /// </summary>
        /// <returns></returns>
        private bool GetPressKeyValue(out KEY key_vlaue)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            { key_vlaue = KEY.Q; return true; }
            else if (Input.GetKeyDown(KeyCode.W))
            { key_vlaue = KEY.W; return true; }
            else if (Input.GetKeyDown(KeyCode.E))
            { key_vlaue = KEY.E; return true; }
            else if (Input.GetKeyDown(KeyCode.A))
            { key_vlaue = KEY.A; return true; }
            else if (Input.GetKeyDown(KeyCode.S))
            { key_vlaue = KEY.S; return true; }
            else if (Input.GetKeyDown(KeyCode.D))
            { key_vlaue = KEY.D; return true; }
            else
            {
                key_vlaue = KEY.Q;
                return false;
            }
        }



        // ��� ����ٰ� �ε����� �ʱ�ȭ�Ͽ� ������ ������մϴ�.
        IEnumerator Restart()
        {
            yield return new WaitForSeconds(0.5f);
            packingUI.Initialized(cur_pattern);
            isPlay = true;
            //UI�� �絵���� �� �ֵ��� ������Ʈ�մϴ�.
        }

        // ��� ����ٰ� ������ �����մϴ�.
        IEnumerator End()
        {
            yield return new WaitForSeconds(0.5f);
            packingUI.gameObject.SetActive(false);
        }
    }

}