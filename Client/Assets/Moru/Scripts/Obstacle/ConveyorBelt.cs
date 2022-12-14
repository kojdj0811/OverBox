using Hyerin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Moru
{
    public class ConveyorBelt : Obstacle
    {
        public bool isBoxExist => box ? true : false;
        [SerializeField]
        private Transform box_Spot_transform;
        public Box box;

        public GameObject Success_Prefap;


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

        /// <summary>
        /// 박스가 콘베이너벨트에 도착하면 호출됩니다.
        /// </summary>
        /// <param name="box"></param>
        public void OnArrive(Box box)
        {
            if (box == null) return;

            //사운드재생
            SoundManager.PlaySFX(SoundManager.SFXClips.Convaynor);


            //박스의 현재 담겨진 아이템 종류입니다.
            //박스의 딕셔너리형을 배열로 변환합니다.
            var boxProducts = box.cur_PuttingItem;
            int[] prd_Arry = new int[6];
            string prd_ArrStr = "";
            for(int i = 0; i < prd_Arry.Length; i++)
            {
                prd_Arry[i] = boxProducts[(MoruDefine.Product)i];
                prd_ArrStr += boxProducts[(MoruDefine.Product)i].ToString();
            }
            Debug.Log($"{prd_ArrStr}");
            
            var requestManager = RequestManager.instance;
            if (requestManager != null)
            {
                //nullable int 선언
                int ?correct_Adress = null;
                //리퀘스트목록에서 같은 배열형식이 있다면 해당어드레스를 할당
                for(int i = 0; i < requestManager.getOrderList().Count; i++)
                {
                    var orderList = requestManager.getOrderList()[i];
                    string order_Arr = "";
                    for(int j= 0; j < orderList.requestList.Length; j++)
                    {
                        order_Arr += orderList.requestList[j].ToString();
                    }
                    Debug.Log("배열결과 : " + order_Arr);
                    if(prd_ArrStr == order_Arr)
                    {
                        correct_Adress = i;
                        break;
                    }
                }
                Debug.Log($" 같은 것을 찾았나요?{correct_Adress.HasValue}");
                if(correct_Adress != null)
                {
                    //리퀘스트 매니저에서 오더리스트의 correct_Adress번째 리스트를 삭제하는 함수
                    requestManager.completeRequest(correct_Adress.Value);
                    //스코어 올리기
                    SetBoxInit(box);
                    GameManager.Instance.GetScore(prd_Arry, box.DiscorrectCount);

                    var obj = GameObject.Find("WorldCanvas");
                    
                    var succussObj = Instantiate(Success_Prefap, transform.position, Quaternion.identity);
                    succussObj.transform.SetParent(obj.transform);
                    
                }
                else
                {
                    //만족하는 리퀘스트가 없으니까
                    //리퀘스트 안지우고, 스코어 안올리고 그냥 삭제
                    SetBoxInit(box);
                }
            }
            else
            {
                Debug.Log("리퀘스트 매니저가 없습니다.");
            }
        }

        #region ABSTRACT
        public override void OnHit(Collision collision)
        {

        }

        public override void OnInteractive(Player pl)
        {
            if (pl.IsCarrying())
            {
                var box_comp = pl.carryingObject.GetComponent<Box>();
                //끌고 있는 오브젝트가 박스일 경우
                if (box_comp)
                {
                    //박스가 포장이 되어있는가?
                    if(box_comp.isPacking)
                    {
                        //되어있다면
                        //박스를 할당한다. (지금은 그냥 비어있는 오브젝트)
                        OnArrive(box_comp);
                        pl.Lay();
                        Destroy(box_comp.gameObject,1f);
                    }
                    else
                    {
                        Debug.Log($"박스 포장이 안되어있습니다.");
                    }
                }
            }
        }
        #endregion
    }
}