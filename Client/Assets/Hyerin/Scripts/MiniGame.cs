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
            score = 0;
            pattern.Add(KeyCode.Q);
            pattern.Add(KeyCode.W);
            pattern.Add(KeyCode.E);
            pattern.Add(KeyCode.A);
            pattern.Add(KeyCode.S);
            pattern.Add(KeyCode.D);
            Shuffle();
        }

        public delegate void Test(KeyCode key);

        public void GetAction(KeyCode key, Test test)
        {
            // 맞았을 때 점수 획득
            if (IsRight(key))
            {
                score++;
            }
        }

        private bool IsRight(KeyCode key)
        {
            return pattern[index++] == key;
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
    }

}