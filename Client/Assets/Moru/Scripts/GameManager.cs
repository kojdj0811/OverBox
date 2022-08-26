using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Moru
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// ������ ���� ���ھ��Դϴ�.
        /// </summary>
        /// 
        [LabelText("���� ��������")]
        public static int curScore = 0;

        /// <summary>
        /// ������ ���� �����Դϴ�.
        /// </summary>
        public int curCoin = 0;
        /// <summary>
        /// ������ ������ �� �ִ� �ִ� �����Դϴ�.
        /// </summary>
        public const int MaxCoin = 10;

        /// <summary>
        /// �մԵ��� ������ ����ִ� CSV�����Դϴ�.
        /// </summary>
        /// 
        [LabelText("�մԵ��� ���� ���� CSV")]
        private TextAsset ProductPatten_CSV;
        public CSV.CSVReader.CSVData pp;
    }
}