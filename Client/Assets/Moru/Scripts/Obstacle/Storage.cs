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


        /// <summary>
        /// �������� �ϳ� �����ϴ�.
        /// </summary>
        private bool PullItem()
        {
            //�÷��̾ �������� �ƹ��͵� ��� ���� �ʴٸ�
            //���丮������ �˻� �ѹ� �ϴϱ�  �켱 �׳� �Ѿ��
            if (true)
            {
                //��ǰ�� ����ϴٸ�
                if (StorageBox.TryProductPull(out int result))
                {
                    //�÷��̾ �ش繰���� ��� �ִ� ���·� ����ϴ�.
                    Debug.Log($"�÷��̾ {TargetProduct}�� {result}��ŭ ���½��ϴ�.");
                    //�������� �ܷ��� ������Ʈ�˴ϴ�.
                    MoruDefine.delegate_UpdateStorage?.Invoke();
                    return true;
                }
                else
                {
                    Debug.Log($"���� �ܷ��� �����մϴ�.");
                    return false;
                }
            }

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
                //�������� �ܷ��� ������Ʈ�˴ϴ�.
                MoruDefine.delegate_UpdateStorage?.Invoke();
            }



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
            if (pl.carryingObject != null)
            {
                //�÷��̾ �������� ��� �ִٸ� � �������� ��� �ִ��� �Ǻ�
                if (pl.carryingIndex == (int)TargetProduct)
                {
                    TrySaveItem();
                    pl.Lay();
                }
                else
                {
                    Debug.Log($"���� �ε������� �ƴ϶� ���� �ȵ� ����� // �õ� : {pl.carryingIndex} // Ÿ�� : {(int)TargetProduct}");
                }
            }
            //�÷��̾ �������� ��� ���� �ʴٸ�
            else if (pl.carryingObject == null)
            {
                if (PullItem())
                {
                    pl.Carry((int)TargetProduct);
                }
            }

        }

        #endregion
    }
}