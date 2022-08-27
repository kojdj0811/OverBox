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
        [SerializeField]
        private Transform box_Spot_transform;
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
            //플레이어가 박스를 들고 있을 경우
            if(pl.myBox)
            {
                Debug.Log($"박스를 잘 들고 있네요");
                //물건을 팩킹데스크에 올립니다.
                SetBoxInit(pl.myBox);
                //플레이어가 물건을 내려놓는 처리를 합니다.
                pl.Lay();
                //포장작업 실시
                if (GameManager.Instance.Pop_MiniGameUI != null)
                {
                    var Minigame = GameManager.Instance.Pop_MiniGameUI;
                    Minigame.GetComponent<MiniGame>().pl_box = this.box;
                    GameManager.Instance.Pop_MiniGameUI?.SetActive(true);
                    pl.state = Player.State.Audition;
                }
                else
                {
                    //완벽히 포장완료 
                    box.CompletePacking(0);
                }



            }
            //플레이어 박스 없고 매대에 박스가 존재할 경우(즉, 포장을 마친 경우)
            else if(!pl.myBox && box)
            {
                //혹시모르는 검사
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
