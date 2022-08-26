using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Moru
{
    /// <summary>
    /// 실제 저장고 기능을 담당하는 클래스입니다.
    /// </summary>
    public class Storage : Obstacle
    {
        [LabelText("타겟 보관함")]
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
        /// 아이템을 하나 꺼냅니다.
        /// </summary>
        public void PullItem()
        {
            //플레이어가 아이템을 아무것도 들고 있지 않다면
            if(true)
            {
                //상품이 충분하다면
                if(StorageBox.TryProductPull(out int result))
                { 
                    //플레이어가 해당물건을 들고 있는 상태로 만듭니다.
                }
            }
        }

        /// <summary>
        /// 아이템을 하나 세이브합니다.
        /// </summary>
        public void TrySaveItem()
        {
            //플레이어가 들고 있는 아이템이 보관함이 보관할 수 있는 종류와 같은 것이라면
            if(true)
            {
                //상품을 보관함에 저장합니다.
                StorageBox.ProductKeep();
                //플레이어의 아이템이 비어있는 상태로 만듭니다.
            }
        }

        public override void OnHit(Collision collision)
        {

        }
    }
}