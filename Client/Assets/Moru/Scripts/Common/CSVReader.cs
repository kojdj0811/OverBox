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
        /// CSV �����͸� �о�� �� �ֵ��� ��ȯ�� ����ü�Դϴ�.
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
                    Debug.LogError("�÷����� �ʹ� �����ϴ�. ���� ���� ���� ��ȯ�մϴ�.");
                    _columnCount = this.columnCount;
                }
                else if (_columnCount < 0)
                {
                    Debug.LogError("�÷����� �ʹ� �����ϴ�. ���� ���� ���� ��ȯ�մϴ�.");
                    _columnCount = 0;
                }
                return datas[_columnCount];
            }

            public string GetData(int _columnCount, int _rowCount)
            {
                if (this.columnCount < _columnCount)
                {
                    Debug.LogError("�÷����� �ʹ� �����ϴ�. ���� ���� ���� ��ȯ�մϴ�.");
                    _columnCount = this.columnCount;
                }
                else if (_columnCount < 0)
                {
                    Debug.LogError("�÷����� �ʹ� �����ϴ�. ���� ���� ���� ��ȯ�մϴ�.");
                    _columnCount = 0;
                }
                if (this.rowCount < _rowCount)
                {
                    Debug.LogError("�ο찪�� �ʹ� �����ϴ�. ���� ���� ���� ��ȯ�մϴ�.");
                    _rowCount = this.rowCount;
                }
                else if (_rowCount < 0)
                {
                    Debug.LogError("�ο찪�� �ʹ� �����ϴ�. ���� ���� ���� ��ȯ�մϴ�.");
                    _rowCount = 0;
                }
                return datas[_columnCount][_rowCount];
            }
        }

        /// <summary>
        /// �ؽ�Ʈ���ҽ� �迭�� �Ҵ��Ͽ� �ʱ�ȭ�մϴ�.
        /// </summary>
        /// <param name="assets"></param>
        /// <param name="keys"></param>
        public static void Initialize_TextAsset(TextAsset[] assets, object[] keys = null)
        {
            //�ؽ�Ʈ������ ������ŭ �����մϴ�.
            for (int i = 0; i < assets.Length; i++)
            {
                //��Ʈ������ ���� ����
                List<string[]> _value = new List<string[]>();
                int colunm = 0;
                int rowCount = 0;

                //�ٹٲ� ������ �ɰ��ϴ�.
                var data_Split = assets[i].text.Split(new char[] { '\n' }, System.StringSplitOptions.None);
                //�� �Ʒ����� �����ͱ׸����κ��� ���ܽ�Ű�� ���� ī��Ʈ�� ���� ���Դϴ�.
                for (int j = 0; j < data_Split.Length - 1; j++)
                {
                    //�ٹٲ� ������ �ɰ��� CSV�� �ٽ� ,������ �ɰ��ϴ�.
                    var value = data_Split[j].Split(new char[] { ',' }, System.StringSplitOptions.None);
                    //���α���
                    rowCount = data_Split.Length;
                    //���α���
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
            //��Ʈ������ ���� ����
            List<string[]> _value = new List<string[]>();
            int colunm = 0;
            int rowCount = 0;

            //�ٹٲ� ������ �ɰ��ϴ�.
            var data_Split = assets.text.Split(new char[] { '\n' }, System.StringSplitOptions.None);
            //�� �Ʒ����� �����ͱ׸����κ��� ���ܽ�Ű�� ���� ī��Ʈ�� ���� ���Դϴ�.
            for (int j = 0; j < data_Split.Length - 1; j++)
            {
                //�ٹٲ� ������ �ɰ��� CSV�� �ٽ� ,������ �ɰ��ϴ�.
                var value = data_Split[j].Split(new char[] { ',' }, System.StringSplitOptions.None);
                //���α���
                rowCount = value.Length;
                //���α���
                colunm = data_Split.Length-1; 

                _value.Add(value);
                
            }
            CSVData data = new CSVData(colunm, rowCount, assets, _value);
            textassets.Add(keys, data);
            return data;
        }

        /// <summary>
        /// CSV������ ���� ������ �о���⸦ �õ��մϴ�.
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