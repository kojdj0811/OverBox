using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Moru
{
    /// <summary>
    /// ���� ����� ����� ����ϴ� Ŭ�����Դϴ�.
    /// </summary>
    public class Storage : Obstacle
    {
        [LabelText("Ÿ�� ������")]
        public Moru.MoruDefine.Product TargetProduct;
        private MoruDefine.StorageBox storageBox;
        public MoruDefine.StorageBox StorageBox 
        { 
            get 
            { 
                if (storageBox == null) storageBox = GameManager.Instance.storageBox[TargetProduct];
                return storageBox;
            } 
        }

        //�׽�Ʈ ���� �����Դϴ�.
        public bool TestToggle = false;



        /// <summary>
        /// �������� �ϳ� �����ϴ�.
        /// </summary>
        private void PullItem()
        {
            //�÷��̾ �������� �ƹ��͵� ��� ���� �ʴٸ�
            if(true)
            {
                //��ǰ�� ����ϴٸ�
                if(StorageBox.TryProductPull(out int result))
                {
                    //�÷��̾ �ش繰���� ��� �ִ� ���·� ����ϴ�.
                    Debug.Log($"�÷��̾ {TargetProduct}�� {result}��ŭ ���½��ϴ�.");
                }
            }

            //�׽�Ʈ ���� �ڵ��Դϴ�.
            TestToggle = !TestToggle;
            //�׽�Ʈ ���� �ڵ��Դϴ�.
        }

        /// <summary>
        /// �������� �ϳ� ���̺��մϴ�.
        /// </summary>
        private void TrySaveItem()
        {
            //�÷��̾ ��� �ִ� �������� �������� ������ �� �ִ� ������ ���� ���̶��
            if (true)
            {
                //��ǰ�� �����Կ� �����մϴ�.
                StorageBox.ProductKeep();
                //�÷��̾��� �������� ����ִ� ���·� ����ϴ�.
                Debug.Log($"�÷��̾ {TargetProduct}�� ����־����ϴ�.");
            }


            //�׽�Ʈ ���� �ڵ��Դϴ�.
            TestToggle = !TestToggle;
            //�׽�Ʈ ���� �ڵ��Դϴ�.
        }

        private void OnCollisionStay(Collision collision)
        {
            
        }


        #region ABSTRACT


        public override void OnHit(Collision collision)
        {

        }

        public override void OnInteractive(Hyerin.Player pl)
        {
            //�ϴ� �׽�Ʈ���Դϴ�.
            //�÷��̾ ������ ��� �ִ��� �Ǻ�
            if(TestToggle)
            {
                //�÷��̾ �������� ��� �ִٸ� � �������� ��� �ִ��� �Ǻ�
                TrySaveItem();
            }
            //�÷��̾ �������� ��� ���� �ʴٸ�
            else if(!TestToggle)
            {
                PullItem();
            }

        }

        #endregion
    }
}