using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyerin
{
    public class MiniGame : MonoBehaviour
    {
        private int index;
        private int score;
        List<KeyCode> pattern = new List<KeyCode>();

        private void Start()
        {
            index = 0;
            score = 100;
            pattern.Add(KeyCode.Q);
            pattern.Add(KeyCode.W);
            pattern.Add(KeyCode.E);
            pattern.Add(KeyCode.A);
            pattern.Add(KeyCode.S);
            pattern.Add(KeyCode.D);
            Shuffle();
        }

        public delegate void Check(KeyCode key);

        public void GetKey(KeyCode key, Check test)
        {
            // Ʋ���� ó������ �絵��
            if (!IsRight(key))
            {
                score -= 10;

            }
        }

        private bool IsRight(KeyCode key)
        {
            bool res = (pattern[index++] == key);
            if (index == pattern.Count) End();
            return res;
        }

        // ������ �� Ű ������ �����ϴ�.
        private void Shuffle()
        {
            int rand1, rand2;
            KeyCode tmp;
            
            for(int i=0; i<pattern.Count; i++)
            {
                rand1 = Random.Range(0,pattern.Count);
                rand2 = Random.Range(0, pattern.Count);

                tmp = pattern[rand1];
                pattern[rand1] = pattern[rand2];
                pattern[rand2] = tmp;
            }
        }

        // ������ ������մϴ�.(�ε����� ����)
        private void Restart()
        {
            // UI ����
            //...
            index = 0;
        }

        // ������ �����մϴ�.
        private void End()
        {
            Player.Instance.carryingBox.GetComponent<Moru.Box>().CompletePacking(score);
        }
    }

}