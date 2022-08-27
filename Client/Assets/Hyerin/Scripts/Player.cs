using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Moru;

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

        [Range(0f, 100f)]
        public float power = 10f;
        [Range(1f, 10f)]
        public float boast = 1.5f;
        private bool isBoastMode = false;
        public float BoastTime = 1f;

        private float keyPressCooldownMax = 0f;
        [Range(0f, 15f)]
        public float MaxBoastCooltime = 5;
        [Range(0.1f, 2f)]
        public float boxSize = 1.5f;

        public int carryingIndex; // 0~6:물건, 7:박스, 8:없음
        public State state;
        public GameObject carryingObject;

        /// <summary>
        /// 알람을 담당하는 오브젝트가 담겨져있습니다.
        /// </summary>
        public Carryed_Image dummy_PopupAlarm;
        /// <summary>
        /// 플레이어가 들고 있는 박스 정보를 담고있습니다.
        /// </summary>
        public Box myBox;

        private bool isSpacebarPressed;
        // 발주 상품 목록
        private int[] items;
        private Animator anim;
        private Rigidbody rb;


        protected override void Awake()
        {
            base.Awake();
            carryingIndex = 8;
        }
        void Start()
        {
            carryingIndex = 8;
            state = State.Movable;
            isSpacebarPressed = false;
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }
#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(boxSize, boxSize, boxSize));
        }
#endif
        /// <summary>
        /// 키 입력으로 플레이어의 이동을 처리합니다.
        /// </summary>
        private void FixedUpdate()
        {
            if (GameManager.Instance.isGameOver) return;
            if (state == State.Movable)
            {
                if (Input.GetKey(KeyCode.UpArrow)) { rb.AddForce(transform.forward * power * Boast()); }
                if (Input.GetKey(KeyCode.DownArrow)) { rb.AddForce(-transform.forward * power * Boast()); }
                if (Input.GetKey(KeyCode.LeftArrow)) { rb.AddForce(-transform.right * power * Boast()); }
                if (Input.GetKey(KeyCode.RightArrow)) { rb.AddForce(transform.right * power * Boast()); }
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
            if (GameManager.Instance.isGameOver) return;
            isSpacebarPressed = false;

            keyPressCooldownMax += Time.deltaTime;



            int layerMask = (1 << (LayerMask.GetMask(Ground)) | (1 << (LayerMask.GetMask(Tag_Player))));
            RaycastHit hit;
            // 플레이어 주변에 상호작용 가능한 오브젝트가 있는지 확인합니다.
            //Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(boxSize, boxSize, boxSize), Quaternion.identity, layerMask);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, boxSize, layerMask, QueryTriggerInteraction.Ignore);

            if (Input.GetKeyUp(KeyCode.Space)) isSpacebarPressed = false;

            // 스페이스바를 눌렀을 때 플레이어 상태에 따라 상호작용합니다.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // 일정 거리 내에 있을 때
                if (hitColliders.Length > 0 && state == State.Movable)
                {
                    Obstacle obj = null;
                    for (int i = 0; i < hitColliders.Length; i++)
                    {
                        // 박스캐스트에 닿은 오브젝트의 상호작용 메서드를 호출합니다.
                        if (hitColliders[i].TryGetComponent<Obstacle>(out obj))
                        {
                            Debug.Log($"{obj.gameObject.name}");
                            obj.OnInteractive(this);
                            break;
                        }

                    }

                    //Old
                    //// 플레이어의 상태를 변경합니다.
                    //if (hitColliders[0].CompareTag(Box) || hitColliders[0].CompareTag(Storage))
                    //{
                    //    Carry(hitColliders[0].gameObject);
                    //    isSpacebarPressed = true;
                    //}
                    //if (hitColliders[0].CompareTag(Computer))
                    //{
                    //    Debug.Log("컴퓨터 사용");
                    //    state = State.Computer;
                    //    isSpacebarPressed = true;
                    //}
                    //if (hitColliders[0].CompareTag(ConveyorBelt))
                    //{
                    //    state = State.Audition;
                    //}
                }
                //Old
                //// 컴퓨터 사용 상태라면 발주 주문을 확정합니다.
                //if (state == State.Computer && !isSpacebarPressed)
                //{
                //    Debug.Log("발주됨");
                //    Moru.MoruDefine.delegate_Delivery?.Invoke(items);
                //}

                //// 물건을 들고 있었다면 내려놓습니다.
                //if (IsCarrying() && !isSpacebarPressed) Lay();
            }


            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (keyPressCooldownMax >= MaxBoastCooltime)
                {
                    isBoastMode = true;
                    keyPressCooldownMax = 0;
                }
            }
            #region QWEASD 처리 (주석처리)
            //if ( Input.GetKeyDown(KeyCode.Q))
            //{
            //    switch (state)
            //    {
            //        case State.Computer:

            //            break;
            //        case State.Audition:
            //            break;
            //        default:
            //            break;
            //    }
            //}

            //if (Input.GetKeyDown(KeyCode.W))
            //{
            //    switch (state)
            //    {
            //        case State.Computer:
            //            break;
            //        case State.Audition:
            //            break;
            //        default:
            //            break;
            //    }
            //}

            //if (Input.GetKeyDown(KeyCode.E))
            //{
            //    switch (state)
            //    {
            //        case State.Computer:
            //            break;
            //        case State.Audition:
            //            break;
            //        default:
            //            break;
            //    }
            //}

            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    switch (state)
            //    {
            //        case State.Computer:
            //            break;
            //        case State.Audition:
            //            break;
            //        default:
            //            break;
            //    }
            //}

            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    switch (state)
            //    {
            //        case State.Computer:
            //            break;
            //        case State.Audition:
            //            break;
            //        default:
            //            break;
            //    }
            //}

            //if (Input.GetKeyDown(KeyCode.D))
            //{
            //    switch (state)
            //    {
            //        case State.Computer:
            //            break;
            //        case State.Audition:
            //            break;
            //        default:
            //            break;
            //    }
            //}
            #endregion

        }

        /// <summary>
        /// 0~7 true : 들고 있다., 8 =false : 아니다
        /// </summary>
        /// <returns></returns>
        public bool IsCarrying()
        {
            return carryingIndex < 8;
        }

        /// <summary>
        /// 물건을 듭니다.
        /// </summary>
        public void Carry(int targetIndex, Box box = null)
        {
            /*old
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
            */
            //8이면 fasle, 즉 핸드 비어있음
            if (IsCarrying())
            {
                Debug.Log($"내 손 꽉차있다");
                return;
            }

            //상자가 아님
            if (targetIndex != 7)
            {
                //현재 끌고 다니고 있는 걸 지웁니다.
                carryingObject = null;
                //자신의 현재 인덱스를 타겟인덱스로 변경
                carryingIndex = targetIndex;
                //define의 적절한 아이콘을 불러와 캐릭터에게 붙여 실행시킨다.
                var sprite = MoruDefine.Item_Icon[carryingIndex];
                dummy_PopupAlarm.Alarming(sprite, this.transform);
                carryingObject = dummy_PopupAlarm.gameObject;
            }

            //상자임
            else if (targetIndex == 7)
            {
                Debug.Log("상자를 집어들었다.");
                //현재 끌고다니고 있는 걸 지웁니다.
                carryingObject = null;
                //자신의 현재 인덱스를 타겟인덱스로 변경
                carryingIndex = targetIndex;
                //define은 아니고 박스를 자신의 자식오브젝트로 변경
                if (box != null)
                {
                    this.myBox = box;
                    box.gameObject.transform.SetParent(this.transform);
                    box.gameObject.transform.localPosition = new Vector3(0.5f, 0.5f, 0);
                    carryingObject = box.gameObject;
                }
            }
        }

        /// <summary>
        /// 물건을 내려놓습니다.
        /// </summary>
        public void Lay()
        {
            Debug.Log("내려놓음");
            //여긴 물건들임
            if (carryingIndex < 7)
            {
                carryingObject = null;
                dummy_PopupAlarm.gameObject.SetActive(false);
            }
            else if (carryingIndex == 7)
            {
                carryingObject = null;
                dummy_PopupAlarm.gameObject.SetActive(false);
            }
            myBox = null;
            carryingIndex = 8;

        }

        private float Boast()
        {
            if (isBoastMode)
            {
                Invoke("InvokingMethod", BoastTime);
                return boast * 10;
            }
            else
            {
                return 1f;
            }

        }
        void InvokingMethod()
        {
            isBoastMode = false;
        }
    }
}
