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
        [LabelText("유저 현재점수")]
        public static int curScore = 0;

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

        #endregion

        #region UIReference
        public GameObject Pop_OrderUI;

        #endregion

        protected override void Awake()
        {
            base.Awake();
            //레시피패턴 할당
            ProductPatten_CSV = Resources.Load<TextAsset>("recipePatten");
            productCSV_Data = CSV.CSVReader.Initialize_TextAsset(ProductPatten_CSV, MoruDefine.ProductPatten_DicKey);
            requests = new MoruDefine.OrderRequest[productCSV_Data.columnCount - 1];
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
        }


        private void Start()
        {
            cur_Delivery_Time = delivery_Time;

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
        }


        public void OnOrderItem(int[] items)
        {
            deliveryManager.OnOrderItem(items);
        }

    }
}