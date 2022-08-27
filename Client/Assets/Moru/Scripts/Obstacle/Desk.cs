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
        public Box box;

        

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