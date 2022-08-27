using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Moru
{
    public class Box : MonoBehaviour
    {
        /// <summary>
        /// ���� �ڽ��� ��� �ִ� ��ǰ��ϵ��Դϴ�.
        /// </summary>
        [ShowInInspector]
        public Dictionary<MoruDefine.Product, int> cur_PuttingItem = new Dictionary<MoruDefine.Product, int>();

        public int DiscorrectCount = 0;


        /// <summary>
        /// �ڽ��� ����� ���������� üũ�ϴ� �Ҹ�
        /// </summary>
        public bool isPacking = false;

        private void Start()
        {
            if (cur_PuttingItem.Count != (int)MoruDefine.Product.MAX)
            {
                for (int i = 0; i < (int)MoruDefine.Product.MAX; i++)
                {
                    cur_PuttingItem.Add((MoruDefine.Product)i, 0);
                }
            }
        }

        /// <summary>
        /// �ڽ��� ������ �� ���ӸŴ����� �����ϴ� �ڽ��� �ܷ��� ������Ʈ�˴ϴ�.
        /// </summary>
        private void OnDestroy()
        {
            //���⼭ �߻��ϴ� ������ �����ص� �˴ϴ�.
            GameManager.Instance.OnRemoveBox();
        }

        /// <summary>
        /// ��ǰ�� �ڽ��� ����ϴ�. �Ű�������ŭ ���� �� �ֽ��ϴ�. (�⺻�� = 1)
        /// </summary>
        /// <param name="product">��ǰ��</param>
        /// <param name="howMay">�󸶳� ���� ���̳�</param>
        public void Put_Product(MoruDefine.Product product, int howMay = 1)
        {
            int count = cur_PuttingItem[product];
            count += howMay;
            cur_PuttingItem[product] = count;

            Debug.Log($"�ڽ��� {product}�� {howMay}��ŭ ��ҽ��ϴ�. ���� ī��� : {count}" +
                $"\n �׽�Ʈ�� �������� ���ԵǴ� ���� �����ϼ���.");
        }

        public void CompletePacking(int DiscorrectCount)
        {
            isPacking = true;
            this.DiscorrectCount = DiscorrectCount;
        }
    }
}