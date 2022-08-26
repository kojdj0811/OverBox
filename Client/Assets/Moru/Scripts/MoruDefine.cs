using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MoruDefine : SingleTone<MoruDefine>
{
    #region INDEX

    /// <summary>
    /// 각 상품들의 인덱스입니다.
    /// </summary>
    public enum Product { Coke = 0, Coffee, Snack, Ramen, Beef_Jerky, DryFood }


    #endregion

    #region PATH&DIC_KEY

    /// <summary>
    /// 오더 패턴을 읽어올 때의 오리지널 키입니다.
    /// </summary>
    public const string ProductPatten_DicKey = "projectKey";

    #endregion

    #region DELEGATE

    /// <summary>
    ///딜리버리 아이템이 결정될 때 호출됩니다.
    /// </summary>
    /// <param name="items"></param>
    public delegate void Delegate_Delivery(int[] items);
    /// <summary>
    ///딜리버리 아이템이 결정될 때 호출됩니다.
    /// </summary>
    /// <param name="items"></param>
    public static Delegate_Delivery delegate_Delivery;

    #endregion

    [System.Serializable]
    public struct OrderRequest
    {
        [LabelText("인덱스")]
        public int index;
        [LabelText("요소별 필요포장개수")]
        public int[] requestList;
        public OrderRequest(string[] values)
        {
            //첫번째 값은 항상 인덱스
            index = int.Parse(values[0]);
            //배열에서 인덱스를 뺀 만큼만 배열에 할당
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
