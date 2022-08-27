using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moru.CSV
{

    public class CSVReader : MonoBehaviour
    {
        [SerializeField] private static Dictionary<object, CSVData> textassets = new Dictionary<object, CSVData>();
        public CSVData this[int value] => textassets[value];

        /// <summary>
        /// CSV 데이터를 읽어올 수 있도록 변환한 구조체입니다.
        /// </summary>
        [System.Serializable]
        public struct CSVData
        {
            [Sirenix.OdinInspector.ReadOnly]
            public int columnCount;
            [Sirenix.OdinInspector.ReadOnly]
            public int rowCount;
            public TextAsset original_CSV;
            [SerializeField]
            private List<string[]> datas;

            public CSVData(int column, int row, TextAsset asset, List<string[]> data)
            {
                columnCount = column;
                rowCount = row;
                original_CSV = asset;
                datas = data;

                for(int i = 0; i < datas.Count; i++)
                {
                    string debug = "";
                    for(int j = 0; j < datas[i].Length; j++)
                    {
                        debug += ","+data[i][j];
                    }
                    //Debug.Log($"{ debug}");

                }
            }


            public string[] GetData(int _columnCount)
            {
                if (this.columnCount <= _columnCount)
                {
                    Debug.LogError("컬럼값이 너무 높습니다. 가장 높은 값을 반환합니다.");
                    _columnCount = this.columnCount;
                }
                else if (_columnCount < 0)
                {
                    Debug.LogError("컬럼값이 너무 낮습니다. 가장 낮은 값을 반환합니다.");
                    _columnCount = 0;
                }
                return datas[_columnCount];
            }

            public string GetData(int _columnCount, int _rowCount)
            {
                if (this.columnCount < _columnCount)
                {
                    Debug.LogError("컬럼값이 너무 높습니다. 가장 높은 값을 반환합니다.");
                    _columnCount = this.columnCount;
                }
                else if (_columnCount < 0)
                {
                    Debug.LogError("컬럼값이 너무 낮습니다. 가장 낮은 값을 반환합니다.");
                    _columnCount = 0;
                }
                if (this.rowCount < _rowCount)
                {
                    Debug.LogError("로우값이 너무 낮습니다. 가장 낮은 값을 반환합니다.");
                    _rowCount = this.rowCount;
                }
                else if (_rowCount < 0)
                {
                    Debug.LogError("로우값이 너무 낮습니다. 가장 낮은 값을 반환합니다.");
                    _rowCount = 0;
                }
                return datas[_columnCount][_rowCount];
            }
        }

        /// <summary>
        /// 텍스트리소스 배열을 할당하여 초기화합니다.
        /// </summary>
        /// <param name="assets"></param>
        /// <param name="keys"></param>
        public static void Initialize_TextAsset(TextAsset[] assets, object[] keys = null)
        {
            //텍스트에셋의 개수만큼 수행합니다.
            for (int i = 0; i < assets.Length; i++)
            {
                //스트링값을 담을 변수
                List<string[]> _value = new List<string[]>();
                int colunm = 0;
                int rowCount = 0;

                //줄바꿈 단위로 쪼갭니다.
                var data_Split = assets[i].text.Split(new char[] { '\n' }, System.StringSplitOptions.None);
                //맨 아래줄은 데이터그릇으로부터 제외시키기 위해 카운트를 한줄 줄입니다.
                for (int j = 0; j < data_Split.Length - 1; j++)
                {
                    //줄바꿈 단위로 쪼개진 CSV를 다시 ,단위로 쪼갭니다.
                    var value = data_Split[j].Split(new char[] { ',' }, System.StringSplitOptions.None);
                    //가로길이
                    rowCount = data_Split.Length;
                    //세로길이
                    colunm = value.Length;

                    for (int k = 0; k < value.Length; k++)
                    {
                        _value.Add(value);
                    }
                }
                if (keys == null)
                {
                    textassets.Add(i, new CSVData(colunm, rowCount, assets[i], _value));
                }
                else
                {
                    textassets.Add(keys[i], new CSVData(colunm, rowCount, assets[i], _value));
                }
            }
        }
        public static CSVData Initialize_TextAsset(TextAsset assets, object keys)
        {
            //스트링값을 담을 변수
            List<string[]> _value = new List<string[]>();
            int colunm = 0;
            int rowCount = 0;

            //줄바꿈 단위로 쪼갭니다.
            var data_Split = assets.text.Split(new char[] { '\n' }, System.StringSplitOptions.None);
            //맨 아래줄은 데이터그릇으로부터 제외시키기 위해 카운트를 한줄 줄입니다.
            for (int j = 0; j < data_Split.Length - 1; j++)
            {
                //줄바꿈 단위로 쪼개진 CSV를 다시 ,단위로 쪼갭니다.
                var value = data_Split[j].Split(new char[] { ',' }, System.StringSplitOptions.None);
                //가로길이
                rowCount = value.Length;
                //세로길이
                colunm = data_Split.Length-1; 

                _value.Add(value);
                
            }
            CSVData data = new CSVData(colunm, rowCount, assets, _value);
            textassets.Add(keys, data);
            return data;
        }

        /// <summary>
        /// CSV데이터 원본 구조를 읽어오기를 시도합니다.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool TryGetCSV_Data(object key, out CSVData data)
        {
            if (CSVReader.textassets.ContainsKey(key))
            {
                data = CSVReader.textassets[key];
                return true;
            }
            else
            {
                data = new CSVData();
                return false;
            }
        }
    }
}