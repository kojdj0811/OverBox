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
        /// 아이템을 하나 꺼냅니다.
        /// </summary>
        private bool PullItem()
        {
            //플레이어가 아이템을 아무것도 들고 있지 않다면
            //스토리지에서 검사 한번 하니까  우선 그냥 넘어가기
            if (true)
            {
                //상품이 충분하다면
                if (StorageBox.TryProductPull(out int result))
                {
                    //플레이어가 해당물건을 들고 있는 상태로 만듭니다.
                    Debug.Log($"플레이어가 {TargetProduct}를 {result}만큼 꺼냈습니다.");
                    //보관함의 잔량이 업데이트됩니다.
                    MoruDefine.delegate_UpdateStorage?.Invoke();
                    return true;
                }
                else
                {
                    Debug.Log($"물건 잔량이 부족합니다.");
                    return false;
                }
            }

        }

        /// <summary>
        /// 아이템을 하나 세이브합니다.
        /// </summary>
        private void TrySaveItem()
        {
            //플레이어가 들고 있는 아이템이 보관함이 보관할 수 있는 종류와 같은 것이라면
            if (true)
            {
                //상품을 보관함에 저장합니다.
                StorageBox.ProductKeep();
                //플레이어의 아이템이 비어있는 상태로 만듭니다.
                Debug.Log($"플레이어가 {TargetProduct}를 집어넣었습니다.");
                //보관함의 잔량이 업데이트됩니다.
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
            //일단 테스트용입니다.
            //플레이어가 아이템 들고 있는지 판별
            if (pl.carryingObject != null)
            {
                //플레이어가 아이템을 들고 있다면 어떤 아이템을 들고 있는지 판별
                if (pl.carryingIndex == (int)TargetProduct)
                {
                    TrySaveItem();
                    pl.Lay();
                }
                else
                {
                    Debug.Log($"같은 인덱스값이 아니라서 보관 안됨 들고계셈 // 시도 : {pl.carryingIndex} // 타겟 : {(int)TargetProduct}");
                }
            }
            //플레이어가 아이템을 들고 있지 않다면
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