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
        /// 배달이 도착했을 때 호출됩니다.
        /// </summary>
        public void Arrive()
        {
            //택배 도착시 코인을 최대코인으로 업데이트합니다.
            var gameManager = Moru.GameManager.Instance;
            gameManager.curCoin = Moru.GameManager.MaxCoin;

            //배달할 아이템 목록을 없앱니다.
            deliveryItem = null;

            //배달할 항목이 있을 경우
            if(isDeliveryItem)
            {
                //컴퓨터 오브젝트에서 알람을 담당할 클래스를 찾아 알람을 발동시킵니다.


                //실제로 발주한 아이템들을 각각 상품에다가 추가하여 더합니다.
                var manager = GameManager.Instance;
                for(int i = 0; i < deliveryItem.Value.items.Length; i++)
                {
                    manager.storageBox[(MoruDefine.Product)i].ProductKeep(deliveryItem.Value.items[i]);
                }
            }

        }


        /// <summary>
        /// 발주를 넣을 경우 해당 메서드가 실행되도록 합니다.
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