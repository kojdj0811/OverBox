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
        /// ������ ���� ���ھ��Դϴ�.
        /// </summary>
        /// 
        [BoxGroup("���� ����"), LabelText("���� ��������")]
        public static int curScore = 0;

        [BoxGroup("���� ����"), LabelText("������ ȹ���ϴ� �⺻����")]
        public static int base_Score = 2000;

        [BoxGroup("���� ����"), LabelText("������ ���Ǵ� �߰��� ȹ���ϴ� ����")]
        public static int prd_Score = 20;

        [BoxGroup("���� ����"), LabelText("������ ������ Ʋ���� �ϳ��� ���̴� ����")]
        public static int discount_Score = 2000;

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

        #endregion

        #region Order

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
        [BoxGroup("���� ����"), LabelText("���� ����")]
        public MoruDefine.OrderRequest[] requests;

        #endregion

        #region Inventory

        [BoxGroup("������"), LabelText("������"), ShowInInspector]
        public Dictionary<MoruDefine.Product, MoruDefine.StorageBox> storageBox = new Dictionary<MoruDefine.Product, MoruDefine.StorageBox>();

        #endregion


        #region BOX

        /// <summary>
        /// �ڽ� ������ �����Դϴ�.
        /// </summary>
        /// 
        [BoxGroup("�ڽ� ��Ģ"), LabelText("�ڽ� ������")]
        public GameObject Box_prefap;

        /// <summary>
        /// �ڽ��� �ִ�� ������ �� �ִ� �����Դϴ�.
        /// </summary>
        [BoxGroup("�ڽ� ��Ģ"), LabelText("�ڽ��� �ִ� ����� ���簡���Ѱ�")]
        public int maxCount_BoxExist;
        [BoxGroup("�ڽ� ��Ģ"), LabelText("���� �ڽ��� �����ϴ� ��"), ReadOnly]
        public int cur_Box_Exist_Count = 0;

        private float cur_spawnBoxTime = 0;
        [BoxGroup("�ڽ� ��Ģ"), LabelText("�ڽ���ȯ �ֱ�")]
        public float spawnBox_MinimumTerm = 0.5f;


        [BoxGroup("�ڽ� ��Ģ"), LabelText("�ڽ���ȯ���")]
        public SpawnBoxDesk spawnDesk;
        #endregion


        #endregion

        #region UIReference

        /// <summary>
        /// �˾��� ���ֽ��� UI�Դϴ�.
        /// </summary>
        [BoxGroup("UI"), LabelText("���ֿ� �˾� UI")]
        public GameObject Pop_OrderUI;
        [BoxGroup("UI"), LabelText("��ǰ �����ܵ�"), SerializeField]
        private List<Sprite> Icon_Images;

        #endregion

        protected override void Awake()
        {
            base.Awake();
            //���������� �Ҵ�
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

            //�������� �Ŵ����� �ʱ�ȭ�մϴ�.
            var Computers = FindObjectsOfType<Computer>(true);
            deliveryManager.OnInitialize(Computers);
            //���� �ֹ� ��������Ʈ ���
            MoruDefine.delegate_Delivery += deliveryManager.OnOrderItem;

            //�� �׸� ���� ������ �������� �ʱ�ȭ�մϴ�. �ִ���������뷮�� ���� ���ӽ��۽� ������ ���� �뷮�� ������ �� 30, 0���� �켱 �����صξ����ϴ�.
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

            //�ڽ��� ��ȯ�� �� �ִ� �����ʹ迭 �Ҵ�
            spawnDesk = FindObjectOfType<SpawnBoxDesk>(true);
            //�ڽ��� ��ȯ�մϴ�.
            spawnDesk.OnSpawn(Box_prefap);
            
        }

        private void Update()
        {
            cur_Delivery_Time += Time.deltaTime;
            if (cur_Delivery_Time >= delivery_Time)
            {
                deliveryManager.Arrive();
                cur_Delivery_Time = 0;
                Debug.LogError("��ǰ ����!");
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
        /// ���ӸŴ����� ������ ������Ʈ�մϴ�.
        /// </summary>
        /// <param name="prd_Arry">�ڽ��� ��ǰ�� ���� �迭</param>
        /// <param name="DiscorreCount">������ Ʋ�� ��</param>
        public void GetScore(int[] prd_Arry, int DiscorreCount)
        {
            int getScore = 0;

            //���̽� ���� ���ϱ�
            getScore += base_Score;

            //���� ����ŭ ���ϱ�
            int i = 0;
            foreach(var inteager in prd_Arry)
            {
                i += inteager;
            }
            getScore += i * prd_Score;

            //������ Ʋ�� ������ŭ ���� ����
            getScore -= DiscorreCount * discount_Score;

            //���� ������Ʈ
            curScore += getScore;
        }
    }
}