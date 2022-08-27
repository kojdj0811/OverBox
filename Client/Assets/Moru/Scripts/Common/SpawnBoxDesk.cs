using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Moru
{
    [RequireComponent(typeof(Moru.Desk))]
    public class SpawnBoxDesk : MonoBehaviour
    {
        /// <summary>
        /// ������ġ�� �ڽ��� ����ũ�Դϴ�.
        /// </summary>
        private Desk mydesk;
        public bool isBoxExsist => mydesk ? mydesk.isBoxExist : false;

        private void Awake()
        {
            mydesk = GetComponent<Desk>();
        }

        public void OnSpawn(GameObject _box)
        {
            //����ũ�� �����Ǿ��ְ�
            if(mydesk)
            {
                //����ũ�� �ڽ��� ���� ���¶��
                if(!mydesk.isBoxExist)
                {
                    //�ڽ� �ν��Ͻ��� ��ȯ�ϰ� ����ũ���� �ڽ��� �������� �ʱ�ȭ�մϴ�.
                    var new_box = Instantiate(_box);
                    var boxComp = new_box.GetComponent<Box>();
                    mydesk.SetBoxInit(boxComp);
                    GameManager.Instance.cur_Box_Exist_Count++;
                }
            }
        }
        
    }
}