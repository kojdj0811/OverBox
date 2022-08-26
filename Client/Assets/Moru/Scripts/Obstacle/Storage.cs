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
        private MoruDefine.StorageBox? storageBox;
        public MoruDefine.StorageBox StorageBox 
        { 
            get 
            { 
                if (storageBox == null) storageBox = GameManager.Instance.storageBox[TargetProduct];
                return (MoruDefine.StorageBox)storageBox;
            } 
        }

        /// <summary>
        /// �������� �ϳ� �����ϴ�.
        /// </summary>
        public void PullItem()
        {
            //�÷��̾ �������� �ƹ��͵� ��� ���� �ʴٸ�
            if(true)
            {
                //��ǰ�� ����ϴٸ�
                if(StorageBox.TryProductPull(out int result))
                { 
                    //�÷��̾ �ش繰���� ��� �ִ� ���·� ����ϴ�.
                }
            }
        }

        /// <summary>
        /// �������� �ϳ� ���̺��մϴ�.
        /// </summary>
        public void TrySaveItem()
        {
            //�÷��̾ ��� �ִ� �������� �������� ������ �� �ִ� ������ ���� ���̶��
            if(true)
            {
                //��ǰ�� �����Կ� �����մϴ�.
                StorageBox.ProductKeep();
                //�÷��̾��� �������� ����ִ� ���·� ����ϴ�.
            }
        }

        public override void OnHit(Collision collision)
        {

        }
    }
}