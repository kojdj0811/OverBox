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
        /// 미니게임에 사용될 키이며 동시에 보여질 텍스트입니다.
        /// </summary>
        public enum KEY { Q, W, E, A, S, D, MAX }

        /// <summary>
        /// 미니게임이 플레이중인지 체크하는 불린입니다.
        /// </summary>
        private bool isPlay;

        /// <summary>
        /// 배열에서 몇번째의 키입력을 판단하기 위한 위치 adress
        /// </summary>
        private int index;

        /// <summary>
        /// 잘못 눌러서 게임이 재시작된 횟수입니다. 0~2 (최대 3회 재시작)
        /// </summary>
        private int wrongCnt;

        private const int MaxWrongCnt = 3;

        /// <summary>
        /// 현재시간
        /// </summary>
        [SerializeField] private float cur_Time;

        /// <summary>
        /// 한 사이클당 최대 플레이가능시간
        /// </summary>
        [SerializeField] private const float Maxtime = 3f;

        /// <summary>
        /// 0~6의 인덱스값들을 무작위로 넣는 기준 길이
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
            // 오답 키를 눌렀다면 재시작합니다.
            /*old
            if (isPlay)
            {
                cur_Time -= Time.deltaTime;
                //시간안에 입력못함
                if (cur_Time <= 0)
                {
                    isPlay = false;
                    cur_Time = Maxtime;
                    Debug.Log("시간초과");
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
                //플레이중에는 시간이 흐릅니다.
                cur_Time += Time.deltaTime;
                //플레이중의 로직 (다른 키입력엔 동작하지 않습니다.)
                if (GetPressKeyValue(out KEY curKEY))
                {
                    //잘누른 케이스
                    if(cur_pattern[index].Equals(curKEY))
                    {
                        //UI에 잘 눌렀다고 업데이트하기
                        packingUI.UpdateSuccess(true, index);
                        index++;
                    }
                    //잘못 누른 케이스
                    else
                    {
                        //플레이 못함
                        packingUI.UpdateSuccess(false, index);
                        isPlay = false;
                        TryRestart();
                    }
                }


                //시간안에 입력못함
                if (cur_Time >= Maxtime)
                {
                    //플레이중이 아님
                    isPlay = false;
                    TryRestart();
                }
                //시간안에 모두 누르는데 성공함
                else if (cur_Time < Maxtime && index == MaxLevel)
                {
                    //플레이중이 아님
                    isPlay = false;
                    //게임 종료
                    Exit_Minigame();
                }
            }
        }

        /// <summary>
        /// MAXLEVEL만큼의 무작위 키코드배열을 생성합니다.
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
            Debug.Log($"패턴 내용 : {deb_str}");
        }

        /// <summary>
        /// 플레이어가 재도전을 시도합니다.
        /// wrongcount를 초과할 경우 재도전을 시도하지 못하고 미니게임이 종료됩니다.
        /// </summary>
        /// <returns></returns>
        private void TryRestart()
        {
            //잘못했어요
            wrongCnt++;
            //최대 시도까지 도전했다면 게임 종료루트
            if (wrongCnt >= MaxWrongCnt)
            {
                Exit_Minigame();
            }
            else
            {
                //일부 게임로직을 초기화하고 리스타트합니다.
                //현재 자신이 위치한 인덱스를 맨 앞으로 돌립니다.
                index = 0;
                cur_Time = 0;
                //약간의 대기시간을 가집니다.
                StartCoroutine(Restart());
            }
        }

        /// <summary>
        /// 미니게임을 종료합니다.
        /// </summary>
        private void Exit_Minigame()
        {
            //플레이어의 박스 컴포넌트를 초기화합니다.
            pl_box.CompletePacking(wrongCnt);
            //플레이어가 박스를 내려놓았으므로 null로 바꿈
            pl_box = null;
            StartCoroutine(End());
        }

        /// <summary>
        /// 플레이어가 어떤 키를 눌렀는지 확인합니다. 올바른 키를 누르면 true, 아니면 fale, 어떤 키를 눌렀는지 key_value로 반환됩니다.
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



        // 잠시 멈췄다가 인덱스를 초기화하여 게임을 재시작합니다.
        IEnumerator Restart()
        {
            yield return new WaitForSeconds(0.5f);
            packingUI.Initialized(cur_pattern);
            isPlay = true;
            //UI를 재도전할 수 있도록 업데이트합니다.
        }

        // 잠시 멈췄다가 게임을 종료합니다.
        IEnumerator End()
        {
            yield return new WaitForSeconds(0.5f);
            packingUI.gameObject.SetActive(false);
        }
    }

}