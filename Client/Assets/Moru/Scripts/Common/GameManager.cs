using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Moru
{
    public class GameManager : SingleToneMono<GameManager>
    {

        #region Field

        #region Delivery
        /// <summary>
        /// 유저의 현재 스코어입니다.
        /// </summary>
        /// 
        [BoxGroup("점수 관련"), LabelText("유저 현재점수")]
        public static int curScore = 0;

        [BoxGroup("점수 관련"), LabelText("유저가 획득하는 기본점수")]
        public static int base_Score = 2000;

        [BoxGroup("점수 관련"), LabelText("유저가 물건당 추가로 획득하는 점수")]
        public static int prd_Score = 20;

        [BoxGroup("점수 관련"), LabelText("유저가 아재기믹 틀리면 하나당 깎이는 점수")]
        public static int discount_Score = 2000;

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
        [BoxGroup("UI"), LabelText("상품 아이콘들"), SerializeField]
        private List<Sprite> Icon_Images;

        #endregion

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
                Debug.LogError("상품 도착!");
            }

            cur_spawnBoxTime += Time.deltaTime;
            if(cur_Box_Exist_Count < maxCount_BoxExist)
            {
                UpdateSpawn();
            }
        }


        public void OnOrderItem(int[] items)
        {
            deliveryManager.OnOrderItem(items);
        }

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
            foreach(var inteager in prd_Arry)
            {
                i += inteager;
            }
            getScore += i * prd_Score;

            //아재기믹 틀린 개수만큼 점수 빼기
            getScore -= DiscorreCount * discount_Score;

            //점수 업데이트
            curScore += getScore;
        }
    }
}