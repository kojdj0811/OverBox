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
            // 틀리면 처음부터 재도전
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

        // 눌러야 할 키 순서를 섞습니다.
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

        // 게임을 재시작합니다.(인덱스만 리셋)
        private void Restart()
        {
            // UI 갱신
            //...
            index = 0;
        }

        // 게임을 종료합니다.
        private void End()
        {
            Player.Instance.carryingBox.GetComponent<Moru.Box>().CompletePacking(score);
        }
    }

}