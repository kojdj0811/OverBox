using Hyerin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Moru
{
    public class ConveyorBelt : Obstacle
    {
        public bool isBoxExist => box ? true : false;
        [SerializeField]
        private Transform box_Spot_transform;
        public Box box;


        /// <summary>
        /// �ڽ���ġ �������� �޼���
        /// </summary>
        /// <param name="box"></param>
        private void SetBoxInit(Box box)
        {
            box.transform.SetParent(this.transform);
            box.gameObject.transform.localPosition = box_Spot_transform.localPosition;
            box.gameObject.transform.rotation = this.transform.rotation;
            this.box = box;
        }

        /// <summary>
        /// �ڽ��� �ܺ��̳ʺ�Ʈ�� �����ϸ� ȣ��˴ϴ�.
        /// </summary>
        /// <param name="box"></param>
        public void OnArrive(Box box)
        {
            if (box == null) return;

            //�ڽ��� ���� ����� ������ �����Դϴ�.
            //�ڽ��� ��ųʸ����� �迭�� ��ȯ�մϴ�.
            var boxProducts = box.cur_PuttingItem;
            int[] prd_Arry = new int[6];
            for(int i = 0; i < prd_Arry.Length; i++)
            {
                prd_Arry[i] = boxProducts[(MoruDefine.Product)i];
            }

            
            var requestManager = RequestManager.instance;
            if (requestManager != null)
            {
                //nullable int ����
                int ?correct_Adress = null;
                //������Ʈ��Ͽ��� ���� �迭������ �ִٸ� �ش��巹���� �Ҵ�
                for(int i = 0; i < requestManager.getOrderList().Count; i++)
                {
                    var orderList = requestManager.getOrderList()[i];
                    if(orderList.requestList == prd_Arry)
                    {
                        correct_Adress = i;
                        break;
                    }
                }
                if(correct_Adress != null)
                {
                    //������Ʈ �Ŵ������� ��������Ʈ�� correct_Adress��° ����Ʈ�� �����ϴ� �Լ�
                    requestManager.completeRequest(correct_Adress.Value);
                    //���ھ� �ø���
                    SetBoxInit(box);
                    GameManager.Instance.GetScore(prd_Arry, box.DiscorrectCount);
                }
                else
                {
                    //�����ϴ� ������Ʈ�� �����ϱ�
                    //������Ʈ �������, ���ھ� �ȿø��� �׳� ����
                    SetBoxInit(box);
                }
            }
            else
            {
                Debug.Log("������Ʈ �Ŵ����� �����ϴ�.");
            }
        }

        #region ABSTRACT
        public override void OnHit(Collision collision)
        {

        }

        public override void OnInteractive(Player pl)
        {
            if (pl.IsCarrying())
            {
                var box_comp = pl.carryingBox.GetComponent<Box>();
                //���� �ִ� ������Ʈ�� �ڽ��� ���
                if (box_comp)
                {
                    //�ڽ��� ������ �Ǿ��ִ°�?
                    if(box_comp.isPacking)
                    {
                        //�Ǿ��ִٸ�
                        //�ڽ��� �Ҵ��Ѵ�. (������ �׳� ����ִ� ������Ʈ)
                        OnArrive(box_comp);
                        var obj = new GameObject();
                        Destroy(obj,1f);
                    }
                    else
                    {
                        Debug.Log($"�ڽ� ������ �ȵǾ��ֽ��ϴ�.");
                    }
                }
            }
        }
        #endregion
    }
}