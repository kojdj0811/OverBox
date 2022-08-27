using Hyerin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Moru
{
    /// <summary>
    /// �ù���ڸ� �÷����� �� �ִ� ����ũ�Դϴ�.
    /// </summary>
    public class Desk : Obstacle
    {
        public bool isBoxExist => box? true : false;
        private Vector3 Box_PivotPos;
        public Box box;

        /// <summary>
        /// ������ ������ �� �ڽ��� ������ �ڽ���ġ�� �������մϴ�.
        /// </summary>
        private void Start()
        {
            if(box != null)
            {
                SetBoxInit(box);
            }
        }

        /// <summary>
        /// �ڽ���ġ �������� �޼���
        /// </summary>
        /// <param name="box"></param>
        public void SetBoxInit(Box box)
        {
            box.gameObject.transform.localPosition = Box_PivotPos;
            box.transform.SetParent(this.transform);
            this.box = box;
        }
        

        #region ABSTRACT

        public override void OnHit(Collision collision)
        {
            
        }

        public override void OnInteractive(Player pl)
        {
            //�ڽ��� ���� ��츸 ��ȣ�ۿ��� �����մϴ�.
            if(isBoxExist)
            {
                //�ڽ��� ���� ���, �÷��̾� �ڵ尡 ������� �ʴٸ� ������ ����ֽ��ϴ�.
                if(pl)
                {
                    //�÷��̾��� �ڵ尡 ��� �ִ°� ������ ��� ������ ����ֽ��ϴ�.
                    if(pl)
                    {
                        //� ������ ����������� �÷��̾��� �ڵ忡�� �����մϴ�. �ϴ� �ӽð� ����
                        int random = Random.Range(0, (int)MoruDefine.Product.MAX);
                        box.Put_Product((MoruDefine.Product)random);
                    }
                    else if(pl) //�÷��̾ ��� �ִ°� �ڽ���� �ڽ��� ����ũ�� �÷��Ӵϴ�.
                    {
                        //�÷��̾ ��� �ִ� �ڽ��� ����ũ�� ��ġ�� �����մϴ�.
                        SetBoxInit(null);
                    }
                }
                //�÷��̾� �ڵ尡 ����ִٸ� ���ڸ� �����ϴ�.
                else
                {

                }
            }
        }

        #endregion
    }
}