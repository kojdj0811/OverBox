using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Moru
{

    public class MoruDefine : SingleTone<MoruDefine>
    {
        #region INDEX

        /// <summary>
        /// 각 상품들의 인덱스입니다.
        /// </summary>
        public enum Product { Coke = 0, Coffee, Snack, Ramen, Beef_Jerky, DryFood, MAX }

        /// <summary>
        /// 캐릭터들의 인덱스입니다.
        /// </summary>
        public enum Character { POPO, PONYEON, PEACH, POONG, MAX }

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



        /// <summary>
        /// 컴퓨터에서 울릴 알람입니다.
        /// </summary>
        public delegate void Delegate_Alarm();
        /// <summary>
        /// 컴퓨터에서 울릴 알람입니다.
        /// </summary>
        public static Delegate_Alarm delegate_Alarm;

        #endregion

        #region STRUCT

        /// <summary>
        /// 오더요청리스트 구조체입니다.
        /// </summary>
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

        /// <summary>
        /// 상품보관함 구조체입니다.
        /// </summary>
        [System.Serializable]
        public class StorageBox
        {
            /// <summary>
            /// 최대로 보관할 수 있는 수입니다.
            /// </summary>
            [ShowInInspector, ReadOnly]
            public int MaxSavedCount { get; private set; }
            /// <summary>
            /// 현재 가지고 있는 수입니다.
            /// </summary>
            [ShowInInspector, ReadOnly]
            public int CurSavedCount { get; private set; }

            public StorageBox(int _MaxSavedCount, int _CurSavedCount)
            {
                MaxSavedCount = _MaxSavedCount;
                CurSavedCount = _CurSavedCount;
            }

            /// <summary>
            /// 상품을 매개변수의 값만큼 보관합니다. 더해서 보관한 상품이 최대보관용량보다 많을 경우 최대보관용량만큼으로 초기화합니다.
            /// </summary>
            /// <param name="howMany"></param>
            public void ProductKeep(int howMany = 1)
            {

                CurSavedCount += howMany;
                if (CurSavedCount >= MaxSavedCount)
                {
                    CurSavedCount = MaxSavedCount;
                }
            }

            /// <summary>
            /// 상품을 꺼내기를 시도합니다. 기본값 = 1
            /// 매개변수의 값만큼 꺼내기를 시도하며 시도결과가 반환되며, 현재 카운트가 줄어듭니다.
            /// </summary>
            /// <param name="howMany">얼마나 꺼낼 것인가?</param>
            /// <returns>시도결과 반환</returns>
            public bool TryProductPull(out int result,int howMany = 1)
            {
                if (CurSavedCount >= howMany)
                {
                    CurSavedCount -= howMany;
                    result = howMany;
                    return true;
                }
                else
                {
                    result = 0;
                    return false;
                }
            }
        }

        #endregion


    }
}