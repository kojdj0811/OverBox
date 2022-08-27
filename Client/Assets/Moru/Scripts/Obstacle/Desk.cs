using Hyerin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Moru
{
    /// <summary>
    /// 택배상자를 올려놓을 수 있는 데스크입니다.
    /// </summary>
    public class Desk : Obstacle
    {
        public bool isBoxExist => box? true : false;
        private Vector3 Box_PivotPos;
        public Box box;

        /// <summary>
        /// 게임을 시작할 때 박스가 있으면 박스위치를 재조정합니다.
        /// </summary>
        private void Start()
        {
            if(box != null)
            {
                SetBoxInit(box);
            }
        }

        /// <summary>
        /// 박스위치 재조정용 메서드
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
            //박스가 있을 경우만 상호작용이 가능합니다.
            if(isBoxExist)
            {
                //박스가 있을 경우, 플레이어 핸드가 비어있지 않다면 물건을 집어넣습니다.
                if(pl)
                {
                    //플레이어의 핸드가 들고 있는게 물건일 경우 물건을 집어넣습니다.
                    if(pl)
                    {
                        //어떤 물건을 집어넣을지는 플레이어의 핸드에서 참조합니다. 일단 임시값 랜덤
                        int random = Random.Range(0, (int)MoruDefine.Product.MAX);
                        box.Put_Product((MoruDefine.Product)random);
                    }
                    else if(pl) //플레이어가 들고 있는게 박스라면 박스를 데스크에 올려둡니다.
                    {
                        //플레이어가 들고 있는 박스를 데스크의 위치로 변경합니다.
                        SetBoxInit(null);
                    }
                }
                //플레이어 핸드가 비어있다면 상자를 집어듭니다.
                else
                {

                }
            }
        }

        #endregion
    }
}