using Hyerin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Moru
{
    /// <summary>
    /// �ڽ��� ��ŷ�ϴ� ����Դϴ�.
    /// </summary>
    public class PackingDesk : Obstacle
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
        private void SetBoxInit(Box box)
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
            //�÷��̾ �ڽ��� ��� ���� ���
            if(pl.myBox)
            {
                Debug.Log($"�ڽ��� �� ��� �ֳ׿�");
                //������ ��ŷ����ũ�� �ø��ϴ�.
                SetBoxInit(pl.myBox);
                //�÷��̾ ������ �������� ó���� �մϴ�.
                pl.Lay();
                //�����۾� �ǽ�
                if (GameManager.Instance.Pop_MiniGameUI != null)
                {
                    var Minigame = GameManager.Instance.Pop_MiniGameUI;
                    Minigame.GetComponent<MiniGame>().pl_box = this.box;
                    GameManager.Instance.Pop_MiniGameUI?.SetActive(true);
                    pl.state = Player.State.Audition;
                }
                else
                {
                    //�Ϻ��� ����Ϸ� 
                    box.CompletePacking(0);
                }



            }
            //�÷��̾� �ڽ� ���� �Ŵ뿡 �ڽ��� ������ ���(��, ������ ��ģ ���)
            else if(!pl.myBox && box)
            {
                //Ȥ�ø𸣴� �˻�
                if(box.isPacking)
                {
                    pl.Carry(7, box);
                    box = null;
                }
                return;
            }
        }

        #endregion
    }
}
