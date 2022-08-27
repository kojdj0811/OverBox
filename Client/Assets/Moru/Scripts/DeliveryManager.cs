using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moru
{
    [System.Serializable]
    public class DeliveryManager
    {
        public Computer[] computer;

        public DeliveryItem deliveryItem;
        public bool isDeliveryItem => deliveryItem != null? true : false;

        /// <summary>
        /// ������������ �Ŵ������� ��ǻ�͸� �����մϴ�.
        /// </summary>
        /// <param name="computers"></param>
        public void OnInitialize(Computer[] computers)
        {
            computer = computers;
        }


        /// <summary>
        /// ����� �������� �� ȣ��˴ϴ�.
        /// </summary>
        public void Arrive()
        {
            //�ù� ������ ������ �ִ��������� ������Ʈ�մϴ�.
            var gameManager = Moru.GameManager.Instance;
            gameManager.curCoin = Moru.GameManager.MaxCoin;


            //����� �׸��� ���� ���
            if(isDeliveryItem)
            {
                //��ǻ�� ������Ʈ���� �˶��� ����� Ŭ������ ã�� �˶��� �ߵ���ŵ�ϴ�.
                MoruDefine.delegate_Alarm?.Invoke();

                //�� ��ǻ�Ͱ� ���������������� ������ ���� �� �ֵ��� �����մϴ�.
                foreach(var _computer in computer)
                {
                    _computer.SetDeliveryItem(deliveryItem);
                }

            }

            //����� ������ ����� ���۴ϴ�.
            deliveryItem = null;

        }


        /// <summary>
        /// ���ָ� ���� ��� �ش� �޼��尡 ����ǵ��� �մϴ�.
        /// </summary>
        /// <param name="items"></param>
        public void OnOrderItem(int[] items)
        {
            deliveryItem = new DeliveryItem(items);
        }




        public class DeliveryItem
        {
            public int[] items;
            public DeliveryItem(int[] items)
            {
                this.items = items;
            }
        }

    }
}