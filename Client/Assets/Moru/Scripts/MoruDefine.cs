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
        /// �� ��ǰ���� �ε����Դϴ�.
        /// </summary>
        public enum Product { Coke = 0, Coffee, Snack, Ramen, Beef_Jerky, DryFood, MAX }

        /// <summary>
        /// ĳ���͵��� �ε����Դϴ�.
        /// </summary>
        public enum Character { POPO, PONYEON, PEACH, POONG, MAX }

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



        /// <summary>
        /// ��ǻ�Ϳ��� �︱ �˶��Դϴ�.
        /// </summary>
        public delegate void Delegate_Alarm();
        /// <summary>
        /// ��ǻ�Ϳ��� �︱ �˶��Դϴ�.
        /// </summary>
        public static Delegate_Alarm delegate_Alarm;

        #endregion

        #region STRUCT

        /// <summary>
        /// ������û����Ʈ ����ü�Դϴ�.
        /// </summary>
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

        /// <summary>
        /// ��ǰ������ ����ü�Դϴ�.
        /// </summary>
        [System.Serializable]
        public class StorageBox
        {
            /// <summary>
            /// �ִ�� ������ �� �ִ� ���Դϴ�.
            /// </summary>
            [ShowInInspector, ReadOnly]
            public int MaxSavedCount { get; private set; }
            /// <summary>
            /// ���� ������ �ִ� ���Դϴ�.
            /// </summary>
            [ShowInInspector, ReadOnly]
            public int CurSavedCount { get; private set; }

            public StorageBox(int _MaxSavedCount, int _CurSavedCount)
            {
                MaxSavedCount = _MaxSavedCount;
                CurSavedCount = _CurSavedCount;
            }

            /// <summary>
            /// ��ǰ�� �Ű������� ����ŭ �����մϴ�. ���ؼ� ������ ��ǰ�� �ִ뺸���뷮���� ���� ��� �ִ뺸���뷮��ŭ���� �ʱ�ȭ�մϴ�.
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
            /// ��ǰ�� �����⸦ �õ��մϴ�. �⺻�� = 1
            /// �Ű������� ����ŭ �����⸦ �õ��ϸ� �õ������ ��ȯ�Ǹ�, ���� ī��Ʈ�� �پ��ϴ�.
            /// </summary>
            /// <param name="howMany">�󸶳� ���� ���ΰ�?</param>
            /// <returns>�õ���� ��ȯ</returns>
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