using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moru
{
    [System.Serializable]
    public class DeliveryManager
    {
        public GameObject computer;

        public DeliveryItem ? deliveryItem;
        public bool isDeliveryItem => deliveryItem != null? true : false;

        /// <summary>
        /// ����� �������� �� ȣ��˴ϴ�.
        /// </summary>
        public void Arrive()
        {
            //�ù� ������ ������ �ִ��������� ������Ʈ�մϴ�.
            var gameManager = Moru.GameManager.Instance;
            gameManager.curCoin = Moru.GameManager.MaxCoin;

            //����� ������ ����� ���۴ϴ�.
            deliveryItem = null;

            //����� �׸��� ���� ���
            if(isDeliveryItem)
            {
                //��ǻ�� ������Ʈ���� �˶��� ����� Ŭ������ ã�� �˶��� �ߵ���ŵ�ϴ�.


                //������ ������ �����۵��� ���� ��ǰ���ٰ� �߰��Ͽ� ���մϴ�.
                var manager = GameManager.Instance;
                for(int i = 0; i < deliveryItem.Value.items.Length; i++)
                {
                    manager.storageBox[(MoruDefine.Product)i].ProductKeep(deliveryItem.Value.items[i]);
                }
            }

        }


        /// <summary>
        /// ���ָ� ���� ��� �ش� �޼��尡 ����ǵ��� �մϴ�.
        /// </summary>
        /// <param name="items"></param>
        public void OnOrderItem(int[] items)
        {
            deliveryItem = new DeliveryItem(items);
        }




        public struct DeliveryItem
        {
            public int[] items;
            public DeliveryItem(int[] items)
            {
                this.items = items;
            }
        }

    }
}