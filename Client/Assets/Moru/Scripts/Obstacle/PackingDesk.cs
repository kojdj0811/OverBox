using Hyerin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Moru
{
    /// <summary>
    /// 박스를 패킹하는 장소입니다.
    /// </summary>
    public class PackingDesk : Obstacle
    {
        public bool isBoxExist => box ? true : false;
        private Vector3 Box_PivotPos;
        public Box box;

        /// <summary>
        /// 게임을 시작할 때 박스가 있으면 박스위치를 재조정합니다.
        /// </summary>
        private void Start()
        {
            if (box != null)
            {
                SetBoxInit(box);
            }
        }

        /// <summary>
        /// 박스위치 재조정용 메서드
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
