using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MoruDefine : SingleTone<MoruDefine>
{
    #region INDEX

    /// <summary>
    /// �� ��ǰ���� �ε����Դϴ�.
    /// </summary>
    public enum Product { Coke = 0, Coffee, Snack, Ramen, Beef_Jerky, DryFood }


    #endregion

    #region PATH&DIC_KEY

    /// <summary>
    /// ���� ������ �о�� ���� �������� Ű�Դϴ�.
    /// </summary>
    public const string ProductPatten_DicKey = "projectKey";

    #endregion

    #region DELEGATE

    /// <summary>
    ///�������� �������� ������ �� ȣ��˴ϴ�.
    /// </summary>
    /// <param name="items"></param>
    public delegate void Delegate_Delivery(int[] items);
    /// <summary>
    ///�������� �������� ������ �� ȣ��˴ϴ�.
    /// </summary>
    /// <param name="items"></param>
    public static Delegate_Delivery delegate_Delivery;

    #endregion

    [System.Serializable]
    public struct OrderRequest
    {
        [LabelText("�ε���")]
        public int index;
        [LabelText("��Һ� �ʿ����尳��")]
        public int[] requestList;
        public OrderRequest(string[] values)
        {
            //ù��° ���� �׻� �ε���
            index = int.Parse(values[0]);
            //�迭���� �ε����� �� ��ŭ�� �迭�� �Ҵ�
            requestList = new int[values.Length - 1];
            for (int i = 0; i < values.Length; i++)
            {
                if (i == 0)
                {
                    
                }
                else
                {
                    requestList[i - 1] = int.Parse(values[i]);
                }
            }
        }
    }

}
