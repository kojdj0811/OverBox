using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Moru
{
    public class GameManager : SingleToneMono<GameManager>
    {

        #region Field

        #region Timer

        /// <summary>
        /// 게임종료는 타이머가 모두 종료되거나 오더 리스트가 0이 되면 게임이 종료되도록 구현합니다.
        /// </summary>
        [BoxGroup("난이도 관련"), LabelText("최대 플레이 타임")]
        public float startPlayTime = 180f;
        /// <summary>
        /// 현재 진행중인 게임의 플레이타임입니다.
        /// </summary>
        [BoxGroup("난이도 관련"), LabelText("현재 플레이 타임")]
        public float cur_PlayTime = 0f;
        [BoxGroup("난이도 관련"), LabelText("남은 손님")]
        public int least_Costomer;
        [BoxGroup("난이도 관련"), LabelText("받아야 하는 손님")]
        public int max_Costomer = 30;
        [BoxGroup("난이도 관련"), LabelText("최고점 기준점수")]
        public int PerfectScore = 60000;
        [BoxGroup("난이도 관련"), LabelText("중박 기준점수")]
        public int NormalScore = 40000;

        public bool isGameOver = false;


        #endregion

        #region Score
        /// <summary>
        /// 유저의 현재 스코어입니다.
        /// </summary>
        /// 
        [BoxGroup("점수 관련"), LabelText("유저 현재점수")]
        public static int curScore = 0;

        [BoxGroup("점수 관련"), LabelText("유저가 획득하는 기본점수")]
        public int base_Score = 2000;

        [BoxGroup("점수 관련"), LabelText("유저가 물건당 추가로 획득하는 점수")]
        public int prd_Score = 20;

        [BoxGroup("점수 관련"), LabelText("유저가 아재기믹 틀리면 하나당 깎이는 점수")]
        public float discount_Score = 0.5f;

        #endregion

        #region Delivery

        /// <summary>
        /// 유저의 현재 코인입니다.
        /// </summary>
        [BoxGroup("발주 관련"), LabelText("유저 현재 발주가능 코인")]
        public int curCoin = 0;
        /// <summary>
        /// 유저가 보유할 수 있는 최대 코인입니다.
        /// </summary>
        [BoxGroup("발주 관련"), LabelText("유저 최대 발주가능 코인"), ShowInInspector]
        public const int MaxCoin = 10;

        /// <summary>
        /// 발주한 물품의 배달 주기입니다.
        /// </summary>
        [BoxGroup("발주 관련"), LabelText("배달 주기")]
        public float delivery_Time = 5f;

        /// <summary>
        /// 현재 배달중인 시간의 주기입니다.
        /// </summary>
        [BoxGroup("발주 관련"), LabelText("현재 배달진행상황 시간")]
        private float cur_Delivery_Time = 5f;

        [BoxGroup("발주 관련"), LabelText("딜리버리 매니저")]
        public DeliveryManager deliveryManager = new DeliveryManager();

        #endregion

        #region Order

        /// <summary>
        /// 손님들의 오더가 담겨있는 CSV파일입니다.
        /// </summary>
        [BoxGroup("오더 관련"), LabelText("손님들의 오더 종류 CSV"), SerializeField]
        private TextAsset ProductPatten_CSV;
        /// <summary>
        /// TextAsset을 읽어올 수 있도록 변환한 파일입니다.
        /// </summary>
        [BoxGroup("오더 관련"), LabelText("손님들의 오더 종류 CSV"), HideInInspector]
        private CSV.CSVReader.CSVData productCSV_Data;
        /// <summary>
        /// 오더 종류입니다.
        /// </summary>
        [BoxGroup("오더 관련"), LabelText("오더 종류")]
        public MoruDefine.OrderRequest[] requests;

        [BoxGroup("오더 관련"), LabelText("오더들 이름")]
        public List<string> orderistNames;

        #endregion

        #region Inventory

        [BoxGroup("보관함"), LabelText("보관함"), ShowInInspector]
        public Dictionary<MoruDefine.Product, MoruDefine.StorageBox> storageBox = new Dictionary<MoruDefine.Product, MoruDefine.StorageBox>();

        #endregion

        #region BOX

        /// <summary>
        /// 박스 프리팹 원본입니다.
        /// </summary>
        /// 
        [BoxGroup("박스 규칙"), LabelText("박스 프리팹")]
        public GameObject Box_prefap;

        /// <summary>
        /// 박스가 최대로 존재할 수 있는 개수입니다.
        /// </summary>
        [BoxGroup("박스 규칙"), LabelText("박스가 최대 몇개까지 존재가능한가")]
        public int maxCount_BoxExist;
        [BoxGroup("박스 규칙"), LabelText("현재 박스가 존재하는 수"), ReadOnly]
        public int cur_Box_Exist_Count = 0;

        private float cur_spawnBoxTime = 0;
        [BoxGroup("박스 규칙"), LabelText("박스소환 주기")]
        public float spawnBox_MinimumTerm = 0.5f;


        [BoxGroup("박스 규칙"), LabelText("박스소환장소")]
        public SpawnBoxDesk spawnDesk;
        #endregion

        #endregion

        #region UIReference

        /// <summary>
        /// 팝업될 발주시의 UI입니다.
        /// </summary>
        [BoxGroup("UI"), LabelText("발주용 팝업 UI")]
        public GameObject Pop_OrderUI;
        [BoxGroup("UI"), LabelText("미니게임용 팝업 UI")]
        public GameObject Pop_MiniGameUI;
        [BoxGroup("UI"), LabelText("상품 아이콘들"), SerializeField]
        private List<Sprite> Icon_Images;

        [BoxGroup("UI"), LabelText("게임종료 UI"), SerializeField]
        private GameObject Pop_GameOverUI;

        [BoxGroup("UI"), LabelText("옵션 UI"), SerializeField]
        private GameObject Pop_OptionUI;

        #endregion
        private static bool isFirstOpen = true;
        protected override void Awake()
        {
            base.Awake();
            //레시피패턴 할당

            ProductPatten_CSV = Resources.Load<TextAsset>("recipePatten");
            productCSV_Data = CSV.CSVReader.Initialize_TextAsset(ProductPatten_CSV, MoruDefine.ProductPatten_DicKey);
            requests = new MoruDefine.OrderRequest[productCSV_Data.columnCount - 2];
            for (int i = 1; i < productCSV_Data.columnCount - 1; i++)
            {
                if (i == 0) { }
                else
                {
                    requests[i - 1] = new MoruDefine.OrderRequest(productCSV_Data.GetData(i));
                }
            }

            //딜리버리 매니저를 초기화합니다.
            var Computers = FindObjectsOfType<Computer>(true);
            deliveryManager.OnInitialize(Computers);
            //발주 주문 델리게이트 등록
            MoruDefine.delegate_Delivery += deliveryManager.OnOrderItem;

            //각 항목에 따른 아이템 보관함을 초기화합니다. 최대저장공간용량과 최초 게임시작시 가지고 있을 용량을 임의의 값 30, 0으로 우선 결정해두었습니다.
            if (storageBox.Count != (int)MoruDefine.Product.MAX)
            {
                for (int i = 0; i < (int)MoruDefine.Product.MAX; i++)
                {
                    storageBox.Add((MoruDefine.Product)i, new MoruDefine.StorageBox(30, 0));
                }
            }
            Icon_Images = new List<Sprite>();
            for (int i = 0; i < (int)MoruDefine.Product.MAX; i++)
            {
                var img = Resources.Load<Sprite>($"Icons/Product/{i}");
                Icon_Images.Add(img);
            }
            MoruDefine.Item_Icon = Icon_Images;
            least_Costomer = max_Costomer;
        }


        private void Start()
        {
            cur_Delivery_Time = delivery_Time;

            //박스를 소환할 수 있는 스포너배열 할당
            spawnDesk = FindObjectOfType<SpawnBoxDesk>(true);
            //박스를 소환합니다.
            spawnDesk.OnSpawn(Box_prefap);

        }

        private void Update()
        {
            cur_Delivery_Time += Time.deltaTime;
            if (cur_Delivery_Time >= delivery_Time)
            {
                deliveryManager.Arrive();
                cur_Delivery_Time = 0;
                //Debug.LogError("상품 도착!");
            }

            cur_spawnBoxTime += Time.deltaTime;
            if (cur_Box_Exist_Count < maxCount_BoxExist)
            {
                UpdateSpawn();
            }

            //플레이타임이 초과되었을 때
            cur_PlayTime += Time.deltaTime;
            if (cur_PlayTime >= startPlayTime || least_Costomer == 0)
            {
                //게임오버가 됩니다.
                GameOver();
            }

            //옵션키를 열고닫음
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Pop_OptionUI?.SetActive(!Pop_OptionUI.activeInHierarchy);
            }
        }


        public void OnOrderItem(int[] items)
        {
            deliveryManager.OnOrderItem(items);
        }

        /// <summary>
        /// 박스를 없앨때 호출되도록 합니다.
        /// </summary>
        public void OnRemoveBox()
        {
            cur_Box_Exist_Count--;
        }

        private void UpdateSpawn()
        {
            if (spawnDesk.isBoxExsist)
            {
                return;
            }
            else
            {
                spawnDesk.OnSpawn(Box_prefap);
                cur_spawnBoxTime = 0;
            }

        }

        /// <summary>
        /// 게임매니저의 점수를 업데이트합니다.
        /// </summary>
        /// <param name="prd_Arry">박스가 상품을 담은 배열</param>
        /// <param name="DiscorreCount">아재기믹 틀린 수</param>
        public void GetScore(int[] prd_Arry, int DiscorreCount)
        {
            int getScore = 0;

            //베이스 점수 더하기
            getScore += base_Score;

            //물건 수만큼 더하기
            int i = 0;
            foreach (var inteager in prd_Arry)
            {
                i += inteager;
            }
            getScore += i * prd_Score;

            //아재기믹 틀린 개수만큼 ^1/2
            for (int j = 0; j < DiscorreCount; j++)
            {
                getScore *= (int)discount_Score;
            }

            //점수 업데이트
            curScore += getScore;
            MoruDefine.delegate_UpdateScore?.Invoke();
        }

        /// <summary>
        /// 게임의 승/패와 관계없이 게임오버가 호출됩니다.
        /// </summary>
        public void GameOver()
        {
            isGameOver = true;
            Pop_GameOverUI.SetActive(true);
            var cmp = Pop_GameOverUI.GetComponent<GameOverUI>();
            if (least_Costomer > 0 || curScore < NormalScore)
            {
                cmp.SetGameOverView(GameOverUI.GameValue.Bad, curScore);
                //배드 엔딩
            }
            //게임 점수에 따라서 차등적인 엔딩이 보여집니다.
            else if (curScore >= PerfectScore)
            {
                cmp.SetGameOverView(GameOverUI.GameValue.Perfect, curScore);
                //최고의 엔딩
            }
            else if (curScore >= NormalScore && curScore < PerfectScore)
            {
                cmp.SetGameOverView(GameOverUI.GameValue.Normal, curScore);
                //노말 엔딩
            }
        }


        public void TestGameOver()
        {
            curScore = 1000000;
            least_Costomer = 0;
            GameOver();
        }
    }
}