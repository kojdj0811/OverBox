using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyerin
{
    public class Player : SingleToneMono<Player>
    {
        #region Const
        public const string Box = "Box";
        public const string Computer = "Computer";
        public const string ConveyorBelt = "ConveyorBelt";
        public const string Ground = "Ground";
        public const string Storage = "Storage";
        #endregion

        /// <summary>
        /// 플레이어의 상태 인덱스입니다.
        /// </summary>
        public enum State { Movable, Computer, Audition }

        [Range(0f, 10f)]
        public float power = 10f;
        [Range(0f, 10f)]
        public float boxCastMaxDistance = 1f;
        [Range(0f, 1f)]
        public float keyPressCooldownMax = 0.1f;

        public int carryingIndex; // 0~6:물건, 7:박스, 8:없음
        public State state;
        public GameObject carryingObj;

        private bool isSpacebarPressed;
        // 발주 상품 목록
        private int[] items;
        private Animator anim;
        private Rigidbody rb;


        void Start()
        {
            carryingIndex = 8;
            state = State.Movable;
            isSpacebarPressed = false;
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }

        void OnDrawGizmos()
        {
            int layerMask = (1 << LayerMask.GetMask(Ground));
            RaycastHit hit;
            bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out hit, transform.rotation, boxCastMaxDistance, layerMask);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            if (isHit)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.lossyScale);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(transform.position + transform.forward * boxCastMaxDistance, transform.lossyScale);
            }
        }

        /// <summary>
        /// 키 입력으로 플레이어의 이동을 처리합니다.
        /// </summary>
        private void FixedUpdate()
        {
            if (state == State.Movable)
            {
                if (Input.GetKey(KeyCode.UpArrow)) { rb.AddForce(transform.forward * power); }
                if (Input.GetKey(KeyCode.DownArrow)) { rb.AddForce(-transform.forward * power); }
                if (Input.GetKey(KeyCode.LeftArrow)) { rb.AddForce(-transform.right * power); }
                if (Input.GetKey(KeyCode.RightArrow)) { rb.AddForce(transform.right * power); }
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {

                }
            }
        }

        /// <summary>
        /// 플레이어의 상호작용을 처리합니다.
        /// </summary>
        void Update()
        {
            isSpacebarPressed = false;

            int layerMask = (1 << LayerMask.GetMask(Ground));
            RaycastHit hit;
            // 플레이어 주변에 상호작용 가능한 오브젝트가 있는지 확인합니다.(레이저를 발사할 위치, 사각형의 각 좌표의 절판 크기, 발사 방향, 충돌 결과, 회전 각도, 최대 거리)
            bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out hit, transform.rotation, boxCastMaxDistance);

            if (Input.GetKeyUp(KeyCode.Space)) isSpacebarPressed = false;

            // 스페이스바를 눌렀을 때 플레이어 상태에 따라 상호작용합니다.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 일정 거리 내에 있을 때
                if(isHit && state == State.Movable)
                {
                    // 박스캐스트에 닿은 오브젝트의 상호작용 메서드를 호출합니다.
                    //hit.collider.GetComponent<Obstacle>().OnInteractive(this);

                    // 플레이어의 상태를 변경합니다.
                    string hitColName = hit.collider.name;
                    switch (hit.collider.name)
                    {
                        case Box:
                        case Storage:
                            Carry(hit.collider.gameObject);
                            isSpacebarPressed = true;
                            break;
                        case Computer:
                            Debug.Log("컴퓨터 사용");
                            state = State.Computer;
                            isSpacebarPressed = true;
                            break;
                        case ConveyorBelt:
                            state = State.Audition;
                            break;
                        default:
                            break;
                    }
                }

                // 컴퓨터 사용 상태라면 발주 주문을 확정합니다.
                if (state == State.Computer && !isSpacebarPressed)
                {
                    Debug.Log("발주됨");
                    //Moru.MoruDefine.delegate_Delivery?.invoke(items);
                }

                // 물건을 들고 있었다면 내려놓습니다.
                if (carryingObj && !isSpacebarPressed) Lay();
            }

            if ( Input.GetKeyDown(KeyCode.Q))
            {
                switch (state)
                {
                    case State.Computer:

                        break;
                    case State.Audition:
                        break;
                    default:
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                switch (state)
                {
                    case State.Computer:
                        break;
                    case State.Audition:
                        break;
                    default:
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (state)
                {
                    case State.Computer:
                        break;
                    case State.Audition:
                        break;
                    default:
                        break;
                }
            }
        }

        public bool IsCarrying()
        {
            return carryingObj != null;
        }

        /// <summary>
        /// 물건을 듭니다.
        /// </summary>
        private void Carry(GameObject obj)
        {
            if (carryingObj) return;

            Debug.Log(obj.name + "을 들었음");
            obj.transform.SetParent(transform);
            carryingObj = obj;
            if (obj.name == Box) carryingIndex = 7;
            else carryingIndex = (int)obj.GetComponent<Moru.Storage>().TargetProduct;
        }
        
        /// <summary>
        /// 물건을 내려놓습니다.
        /// </summary>
        private void Lay()
        {
            Debug.Log(carryingObj.name + "을 놓음");
            carryingObj.transform.SetParent(null);
            carryingObj = null;
            carryingIndex = 8;
        }
    }
}
