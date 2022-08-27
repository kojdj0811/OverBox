using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Moru
{
    [RequireComponent(typeof(Moru.Desk))]
    public class SpawnBoxDesk : MonoBehaviour
    {
        /// <summary>
        /// 스폰위치는 자신의 데스크입니다.
        /// </summary>
        private Desk mydesk;

        private void Awake()
        {
            mydesk = GetComponent<Desk>();
        }

        public void OnSpawn(GameObject _box)
        {
            //데스크가 참조되어있고
            if(mydesk)
            {
                //데스크가 박스가 없는 상태라면
                if(!mydesk.isBoxExist)
                {
                    //박스 인스턴스를 소환하고 데스크에서 박스를 참조시켜 초기화합니다.
                    var new_box = Instantiate(_box);
                    var boxComp = new_box.GetComponent<Box>();
                    mydesk.SetBoxInit(boxComp);
                }
            }
        }
        
    }
}