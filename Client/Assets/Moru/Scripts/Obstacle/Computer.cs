using Hyerin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Moru
{
    /// <summary>
    /// �÷��̾ ��ǻ�Ϳ��� ��ȣ�ۿ� ��, ������ ��ǰ�� �������ִٸ� ���ֻ�ǰ�� �����ϰ�,
    /// ���ָ� �����մϴ�. ���ִ� 1ȸ�� �����ϸ�, �ù������� �����ߴٸ� �ٽ� ���ְ� �����մϴ�.
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
            //��������Ʈ ���
            MoruDefine.delegate_Alarm += Alarm;
            MoruDefine.delegate_Alarm += OnArrive;
            MoruDefine.delegate_Delivery += OnOrderEnded;
        }

        void Update()
        {

        }

        /// <summary>
        /// ��޿��� ������ �����ϸ� �Բ� ȣ��� �˶��� �޼����Դϴ�.
        /// </summary>
        public void Alarm()
        {
            isAlarm = true;         //�˶��� �︮�� ���·� ����
            Debug.Log("�˶��� �︳�ϴ�");
            if (alarm != null)
            {
                alarm.Alarming(sprite, this.transform);
            }
        }

        /// <summary>
        /// ��޿��� ������ �����ϸ� �ֹ������� ���·� �����ϴ� �޼����Դϴ�.
        /// </summary>
        public void OnArrive()
        {
            isOrderable = true;     //������ ������ ���·� ����
        }

        /// <summary>
        /// �÷��̾ ������ ��ĥ ��� �ڵ����� ����˴ϴ�.
        /// </summary>
        /// <param name="items"></param>
        public void OnOrderEnded(int[] items)
        {
            isOrderable = false;
        }

        /// <summary>
        /// ������ �������� ��ǻ�ͷ� �ű�ϴ�.
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
            //������ ���ǵ��� �����ϴ� ó��
            if (isAlarm)
            {
                if (deliveryItem != null)
                {
                    //������ ������ �����۵��� ���� ��ǰ���ٰ� �߰��Ͽ� ���մϴ�.
                    for (int i = 0; i < deliveryItem.items.Length; i++)
                    {
                        GameManager.Instance.storageBox[(MoruDefine.Product)i].ProductKeep(deliveryItem.items[i]);
                    }
                    Debug.Log("���������� ������ ���������� �ű�ϴ�.");
                    //�������� �ܷ��� ������Ʈ�˴ϴ�.
                    MoruDefine.delegate_UpdateStorage?.Invoke();
                }
                else Debug.Log("���������� ���Դϴ�.");
                //�˶��� ���ϴ�.
                isAlarm = false;
                alarm?.gameObject.SetActive(false);
            }


            //���� ���� ó��
            //���� �÷��̾ ������ ��� �ִٸ� ��ȣ�ۿ��� �Ұ����մϴ�.
            if (pl)
            {
                //return;
            }
            //���� �÷��̾ ������ ������ ���¶�� 
            else if (isOrderable)
            {
                //�÷��̰� ������ �Ұ����� ���µǵ���
                pl.state = Player.State.Computer;
                //����â UI�� ��ϴ�.
                GameManager.Instance.Pop_OrderUI?.SetActive(true);

            }
            else
            {
                //������ ��� �ְų� �ֹ��� �Ұ����� �����Դϴ�.

            }



        }
        #endregion
    }
}