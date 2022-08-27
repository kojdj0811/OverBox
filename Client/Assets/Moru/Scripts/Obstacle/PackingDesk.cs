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
        private Vector3 Box_PivotPos;
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
            
        }

        #endregion
    }
}
