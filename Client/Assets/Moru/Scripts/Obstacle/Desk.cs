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
        public bool isBoxExist => box ? true : false;
        [SerializeField]
        private Transform box_Spot_transform;
        public Box box;

        /// <summary>
        /// ������ ������ �� �ڽ��� ������ �ڽ���ġ�� �������մϴ�.
        /// </summary>
        private void Start()
        {
            if (box != null)
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
            box.transform.SetParent(this.transform);
            box.gameObject.transform.localPosition = box_Spot_transform.localPosition;
            box.gameObject.transform.rotation = this.transform.rotation;
            this.box = box;
        }


        #region ABSTRACT

        public override void OnHit(Collision collision)
        {

        }

        public override void OnInteractive(Player pl)
        {
            //�ڽ��� ���� ��츸 ��ȣ�ۿ��� �����մϴ�.
            if (isBoxExist)
            {
                //�ڽ��� ���� ���, �÷��̾� �ڵ尡 ������� �ʰ� �ڽ��� �ƴ϶�� ������ ����ֽ��ϴ�.
                if (pl.carryingIndex != 8 && pl.carryingIndex != 7)
                {
                    //� ������ ����������� �÷��̾��� �ڵ忡�� �����մϴ�. �ϴ� �ӽð� ����
                    box.Put_Product((MoruDefine.Product)pl.carryingIndex);
                }
                //�÷��̾ ��� �ִ°� �ڽ���� �ڽ��� ����ũ�� �÷��Ӵϴ�.
                else if (pl.carryingIndex == 7) 
                {
                    //�÷��̾ ��� �ִ� �ڽ��� ����ũ�� ��ġ�� �����մϴ�.
                    SetBoxInit(pl.carryingBox.GetComponent<Box>());
                }
                //�÷��̾� �ڵ尡 ����ִٸ� ���ڸ� �����ϴ�.
                else
                {
                    //�ڽ��� ������ ó���� �÷��̾�� ������ ����
                }
            }
        }

        #endregion
    }
}