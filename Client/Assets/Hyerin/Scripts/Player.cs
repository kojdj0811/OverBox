using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyerin
{
    public class Player : MonoBehaviour
    {
        [Range(0f, 0.5f)]
        public float speed = 0.05f;

        private State state;
        private RaycastHit rayHit;
        private Animator anim;
        private Rigidbody2D rb;

        private enum State
        {
            MOVEABLE, COMPUTER, AUDITION
        }

        // Start is called before the first frame update
        void Start()
        {
            state = State.MOVEABLE;
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            #region 이동 처리
            if (state == State.MOVEABLE)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.position += (transform.forward*speed);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.position -= (transform.forward*speed);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.position -= (transform.right*speed);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.position += (transform.right*speed);
                }

                if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
                {

                }
            }
            #endregion

            

            if (Input.GetKeyDown(KeyCode.Q))
            {
                switch (state)
                {
                    case State.COMPUTER:
                        break;
                    case State.AUDITION:
                        break;
                    default:
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {

            }

            if (Input.GetKeyDown(KeyCode.E))
            {

            }
        }
    }
}
