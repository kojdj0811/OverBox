using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Moru
{
    public class GameManager : SingleToneMono<GameManager>
    {

        #region Field

        /// <summary>
        /// ������ ���� ���ھ��Դϴ�.
        /// </summary>
        /// 
        [LabelText("���� ��������")]
        public static int curScore = 0;

        /// <summary>
        /// ������ ���� �����Դϴ�.
        /// </summary>
        [BoxGroup("���� ����"), LabelText("���� ���� ���ְ��� ����")]
        public int curCoin = 0;
        /// <summary>
        /// ������ ������ �� �ִ� �ִ� �����Դϴ�.
        /// </summary>
        [BoxGroup("���� ����"), LabelText("���� �ִ� ���ְ��� ����"), ShowInInspector]
        public const int MaxCoin = 10;

        /// <summary>
        /// ������ ��ǰ�� ��� �ֱ��Դϴ�.
        /// </summary>
        [BoxGroup("���� ����"), LabelText("��� �ֱ�")]
        public float delivery_Time = 5f;

        /// <summary>
        /// ���� ������� �ð��� �ֱ��Դϴ�.
        /// </summary>
        [BoxGroup("���� ����"), LabelText("���� ��������Ȳ �ð�")]
        private float cur_Delivery_Time = 5f;

        [BoxGroup("���� ����"), LabelText("�������� �Ŵ���")]
        public DeliveryManager deliveryManager = new DeliveryManager();

        /// <summary>
        /// �մԵ��� ������ ����ִ� CSV�����Դϴ�.
        /// </summary>
        [BoxGroup("���� ����"), LabelText("�մԵ��� ���� ���� CSV"), SerializeField]
        private TextAsset ProductPatten_CSV;
        /// <summary>
        /// TextAsset�� �о�� �� �ֵ��� ��ȯ�� �����Դϴ�.
        /// </summary>
        [BoxGroup("���� ����"), LabelText("�մԵ��� ���� ���� CSV"), HideInInspector]
        private CSV.CSVReader.CSVData productCSV_Data;
        /// <summary>
        /// ���� �����Դϴ�.
        /// </summary>
        [BoxGroup("���� ����"),LabelText("���� ����")]
        public MoruDefine.OrderRequest[] requests;



        #endregion

        protected override void Awake()
        {
            base.Awake();
            //���������� �Ҵ�
            ProductPatten_CSV = Resources.Load<TextAsset>("recipePatten");
            productCSV_Data = CSV.CSVReader.Initialize_TextAsset(ProductPatten_CSV, MoruDefine.ProductPatten_DicKey);
            requests = new MoruDefine.OrderRequest[productCSV_Data.columnCount - 1];
            for(int i = 1; i < productCSV_Data.columnCount-1; i++)
            {
                if (i == 0) { }
                else
                {
                    requests[i - 1] = new MoruDefine.OrderRequest(productCSV_Data.GetData(i));
                }
            }


            //���� �ֹ� ��������Ʈ ���
            MoruDefine.delegate_Delivery += deliveryManager.OnOrderItem;
        }


        private void Start()
        {
            cur_Delivery_Time = delivery_Time;

        }

        private void Update()
        {
            cur_Delivery_Time += Time.deltaTime;
            if(cur_Delivery_Time >= delivery_Time)
            {
                deliveryManager.Arrive();
                cur_Delivery_Time = 0;
            }
        }
    }
}