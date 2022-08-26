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

            }

        }


        /// <summary>
        /// ���ָ� ���� ��� ��������Ʈ �޼��尡 ����˴ϴ�.
        /// </summary>
        /// <param name="items"></param>
        public void OnOrderItem(int[] items)
        {
            deliveryItem = new DeliveryItem(items);
        }




        public struct DeliveryItem
        {
            int[] items;
            public DeliveryItem(int[] items)
            {
                this.items = items;
            }
        }

    }
}