using Hyerin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Moru
{
    /// <summary>
    /// �ù���ڸ� �÷����� �� �ִ� ����ũ�Դϴ�.
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
            //�ڽ��� ���� ��츸 ��ȣ�ۿ��� �����մϴ�.
            if(isBoxExist)
            {
                //�ڽ��� ���� ���, �÷��̾� �ڵ尡 ������� �ʴٸ� ������ ����ֽ��ϴ�.
                if(pl)
                {

                }
                //�÷��̾� �ڵ尡 ����ִٸ� ���ڸ� �����ϴ�.
                else
                {

                }
            }
        }

        #endregion
    }
}