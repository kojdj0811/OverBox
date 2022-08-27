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
        public const string Tag_Player = "Player";
        public const string Storage = "Storage";
        #endregion

        /// <summary>
        /// 플레이어의 상태 인덱스입니다.
        /// </summary>
        public enum State { Movable, Computer, Audition }

        [Range(0f, 10f)]
        public float power = 10f;
        [Range(0f, 1f)]
        public float keyPressCooldownMax = 0.1f;
        [Range(1f, 5f)]
        public float boxSize = 1.5f;

        public int carryingIndex; // 0~6:물건, 7:박스, 8:없음
        public State state;
        public GameObject carryingBox;

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
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(boxSize, boxSize, boxSize));
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

            int layerMask = (1 << (LayerMask.GetMask(Ground)) | (1 << (LayerMask.GetMask(Tag_Player))));
            RaycastHit hit;
            // 플레이어 주변에 상호작용 가능한 오브젝트가 있는지 확인합니다.
            Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(boxSize, boxSize, boxSize), Quaternion.identity, layerMask);

            if (Input.GetKeyUp(KeyCode.Space)) isSpacebarPressed = false;

            // 스페이스바를 눌렀을 때 플레이어 상태에 따라 상호작용합니다.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 일정 거리 내에 있을 때
                if(hitColliders.Length>0 && state == State.Movable)
                {
                    // 박스캐스트에 닿은 오브젝트의 상호작용 메서드를 호출합니다.
                    hitColliders[0].GetComponent<Obstacle>().OnInteractive(this);

                    // 플레이어의 상태를 변경합니다.
                    if (hitColliders[0].CompareTag(Box) || hitColliders[0].CompareTag(Storage))
                    {
                        Carry(hitColliders[0].gameObject);
                        isSpacebarPressed = true;
                    }
                    if (hitColliders[0].CompareTag(Computer))
                    {
                        Debug.Log("컴퓨터 사용");
                        state = State.Computer;
                        isSpacebarPressed = true;
                    }
                    if (hitColliders[0].CompareTag(ConveyorBelt))
                    {
                        state = State.Audition;
                    }
                }

                // 컴퓨터 사용 상태라면 발주 주문을 확정합니다.
                if (state == State.Computer && !isSpacebarPressed)
                {
                    Debug.Log("발주됨");
                    Moru.MoruDefine.delegate_Delivery?.Invoke(items);
                }

                // 물건을 들고 있었다면 내려놓습니다.
                if (IsCarrying() && !isSpacebarPressed) Lay();
            }

            #region QWEASD 처리
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

            if (Input.GetKeyDown(KeyCode.A))
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

            if (Input.GetKeyDown(KeyCode.S))
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

            if (Input.GetKeyDown(KeyCode.D))
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
            #endregion
        }

        public bool IsCarrying()
        {
            return carryingIndex < 8;
        }

        /// <summary>
        /// 물건을 듭니다.
        /// </summary>
        private void Carry(GameObject obj)
        {
            if (IsCarrying()) return;

            if (obj.name == Box)
            {
                Debug.Log("박스 들었음");
                carryingBox = obj;
                obj.transform.SetParent(transform);
                carryingIndex = 7;
            }
            else
            {
                Debug.Log(obj.GetComponent<Moru.Storage>().TargetProduct+" 들었음");
                // UI 메서드 호출 (구현되면 추가 예정)
                //...
                carryingIndex = (int)obj.GetComponent<Moru.Storage>().TargetProduct;
            }
        }
        
        /// <summary>
        /// 물건을 내려놓습니다.
        /// </summary>
        private void Lay()
        {
            Debug.Log("내려놓음");
            if (carryingBox)
            {
                carryingBox.transform.SetParent(null);
                carryingBox = null;
            }
            carryingIndex = 8;
        }
    }
}
