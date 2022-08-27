using Hyerin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Moru
{
    /// <summary>
    /// 플레이어가 컴퓨터에서 상호작용 시, 발주한 상품이 도착해있다면 발주상품을 습득하고,
    /// 발주를 시작합니다. 발주는 1회만 가능하며, 택배차량이 도착했다면 다시 발주가 가능합니다.
    /// </summary>
    public class Computer : Obstacle
    {
        public bool isOrderable = true;
        public bool isAlarm { get; private set; }
        static DeliveryManager.DeliveryItem deliveryItem;

        public Alarm alarm;
        public Sprite sprite;

        void Start()
        {
            //델리게이트 등록
            MoruDefine.delegate_Alarm += Alarm;
            MoruDefine.delegate_Alarm += OnArrive;
            MoruDefine.delegate_Delivery += OnOrderEnded;
        }

        void Update()
        {

        }

        /// <summary>
        /// 배달오는 차량이 도착하면 함께 호출될 알람용 메서드입니다.
        /// </summary>
        public void Alarm()
        {
            isAlarm = true;         //알람이 울리는 상태로 설정
            Debug.Log("알람이 울립니다");
            if (alarm != null)
            {
                alarm.Alarming(sprite, this.transform);
            }
        }

        /// <summary>
        /// 배달오는 차량이 도착하면 주문가능한 상태로 변경하는 메서드입니다.
        /// </summary>
        public void OnArrive()
        {
            isOrderable = true;     //오더가 가능한 상태로 변경
        }

        /// <summary>
        /// 플레이어가 오더를 마칠 경우 자동으로 실행됩니다.
        /// </summary>
        /// <param name="items"></param>
        public void OnOrderEnded(int[] items)
        {
            isOrderable = false;
        }

        /// <summary>
        /// 발주한 아이템을 컴퓨터로 옮깁니다.
        /// </summary>
        /// <param name="_deliveryItem"></param>
        public void SetDeliveryItem(DeliveryManager.DeliveryItem _deliveryItem)
        {
            deliveryItem = _deliveryItem;
        }


        #region ABSTACT

        public override void OnHit(Collision collision)
        {

        }

        public override void OnInteractive(Player pl)
        {
            //발주한 물건들을 습득하는 처리
            if (isAlarm)
            {
                if (deliveryItem != null)
                {
                    //실제로 발주한 아이템들을 각각 상품에다가 추가하여 더합니다.
                    for (int i = 0; i < deliveryItem.items.Length; i++)
                    {
                        GameManager.Instance.storageBox[(MoruDefine.Product)i].ProductKeep(deliveryItem.items[i]);
                    }
                    Debug.Log("딜리버리가 물건을 보관함으로 옮깁니다.");
                    //보관함의 잔량이 업데이트됩니다.
                    MoruDefine.delegate_UpdateStorage?.Invoke();
                }
                else Debug.Log("딜리버리가 널입니다.");
                //알람을 끕니다.
                isAlarm = false;
                alarm?.gameObject.SetActive(false);
            }


            //오더 관련 처리
            //만일 플레이어가 물건을 들고 있다면 상호작용이 불가능합니다.
            if (pl)
            {
                //return;
            }
            //만일 플레이어가 오더가 가능한 상태라면 
            else if (isOrderable)
            {
                //플레이가 조작이 불가능한 상태되도록
                pl.state = Player.State.Computer;
                //오더창 UI를 폅니다.
                GameManager.Instance.Pop_OrderUI?.SetActive(true);

            }
            else
            {
                //물건을 들고 있거나 주문이 불가능한 상태입니다.

            }



        }
        #endregion
    }
}