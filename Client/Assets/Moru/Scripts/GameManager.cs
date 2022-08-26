using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Moru
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// 유저의 현재 스코어입니다.
        /// </summary>
        /// 
        [LabelText("유저 현재점수")]
        public static int curScore = 0;

        /// <summary>
        /// 유저의 현재 코인입니다.
        /// </summary>
        public int curCoin = 0;
        /// <summary>
        /// 유저가 보유할 수 있는 최대 코인입니다.
        /// </summary>
        public const int MaxCoin = 10;

        /// <summary>
        /// 손님들의 오더가 담겨있는 CSV파일입니다.
        /// </summary>
        /// 
        [LabelText("손님들의 오더 종류 CSV")]
        private TextAsset ProductPatten_CSV;
        public CSV.CSVReader.CSVData pp;
    }
}