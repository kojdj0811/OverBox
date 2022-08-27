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
        /// �÷��̾��� ���� �ε����Դϴ�.
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

        public int carryingIndex; // 0~6:����, 7:�ڽ�, 8:����
        public State state;
        public GameObject carryingObject;

        /// <summary>
        /// �˶��� ����ϴ� ������Ʈ�� ������ֽ��ϴ�.
        /// </summary>
        public Carryed_Image dummy_PopupAlarm;
        /// <summary>
        /// �÷��̾ ��� �ִ� �ڽ� ������ ����ֽ��ϴ�.
        /// </summary>
        public Box myBox;

        private bool isSpacebarPressed;
        // ���� ��ǰ ���
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
        /// Ű �Է����� �÷��̾��� �̵��� ó���մϴ�.
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
        /// �÷��̾��� ��ȣ�ۿ��� ó���մϴ�.
        /// </summary>
        void Update()
        {
            if (GameManager.Instance.isGameOver) return;
            isSpacebarPressed = false;

            keyPressCooldownMax += Time.deltaTime;



            int layerMask = (1 << (LayerMask.GetMask(Ground)) | (1 << (LayerMask.GetMask(Tag_Player))));
            RaycastHit hit;
            // �÷��̾� �ֺ��� ��ȣ�ۿ� ������ ������Ʈ�� �ִ��� Ȯ���մϴ�.
            //Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(boxSize, boxSize, boxSize), Quaternion.identity, layerMask);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, boxSize, layerMask, QueryTriggerInteraction.Ignore);

            if (Input.GetKeyUp(KeyCode.Space)) isSpacebarPressed = false;

            // �����̽��ٸ� ������ �� �÷��̾� ���¿� ���� ��ȣ�ۿ��մϴ�.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // ���� �Ÿ� ���� ���� ��
                if (hitColliders.Length > 0 && state == State.Movable)
                {
                    Obstacle obj = null;
                    for (int i = 0; i < hitColliders.Length; i++)
                    {
                        // �ڽ�ĳ��Ʈ�� ���� ������Ʈ�� ��ȣ�ۿ� �޼��带 ȣ���մϴ�.
                        if (hitColliders[i].TryGetComponent<Obstacle>(out obj))
                        {
                            Debug.Log($"{obj.gameObject.name}");
                            obj.OnInteractive(this);
                            break;
                        }

                    }

                    //Old
                    //// �÷��̾��� ���¸� �����մϴ�.
                    //if (hitColliders[0].CompareTag(Box) || hitColliders[0].CompareTag(Storage))
                    //{
                    //    Carry(hitColliders[0].gameObject);
                    //    isSpacebarPressed = true;
                    //}
                    //if (hitColliders[0].CompareTag(Computer))
                    //{
                    //    Debug.Log("��ǻ�� ���");
                    //    state = State.Computer;
                    //    isSpacebarPressed = true;
                    //}
                    //if (hitColliders[0].CompareTag(ConveyorBelt))
                    //{
                    //    state = State.Audition;
                    //}
                }
                //Old
                //// ��ǻ�� ��� ���¶�� ���� �ֹ��� Ȯ���մϴ�.
                //if (state == State.Computer && !isSpacebarPressed)
                //{
                //    Debug.Log("���ֵ�");
                //    Moru.MoruDefine.delegate_Delivery?.Invoke(items);
                //}

                //// ������ ��� �־��ٸ� ���������ϴ�.
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
            #region QWEASD ó�� (�ּ�ó��)
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
        /// 0~7 true : ��� �ִ�., 8 =false : �ƴϴ�
        /// </summary>
        /// <returns></returns>
        public bool IsCarrying()
        {
            return carryingIndex < 8;
        }

        /// <summary>
        /// ������ ��ϴ�.
        /// </summary>
        public void Carry(int targetIndex, Box box = null)
        {
            /*old
            if (IsCarrying()) return;

            if (obj.name == Box)
            {
                Debug.Log("�ڽ� �����");
                carryingBox = obj;
                obj.transform.SetParent(transform);
                carryingIndex = 7;
            }
            else
            {
                Debug.Log(obj.GetComponent<Moru.Storage>().TargetProduct+" �����");
                // UI �޼��� ȣ�� (�����Ǹ� �߰� ����)
                //...
                carryingIndex = (int)obj.GetComponent<Moru.Storage>().TargetProduct;
            }
            */
            //8�̸� fasle, �� �ڵ� �������
            if (IsCarrying())
            {
                Debug.Log($"�� �� �����ִ�");
                return;
            }

            //���ڰ� �ƴ�
            if (targetIndex != 7)
            {
                //���� ���� �ٴϰ� �ִ� �� ����ϴ�.
                carryingObject = null;
                //�ڽ��� ���� �ε����� Ÿ���ε����� ����
                carryingIndex = targetIndex;
                //define�� ������ �������� �ҷ��� ĳ���Ϳ��� �ٿ� �����Ų��.
                var sprite = MoruDefine.Item_Icon[carryingIndex];
                dummy_PopupAlarm.Alarming(sprite, this.transform);
                carryingObject = dummy_PopupAlarm.gameObject;
            }

            //������
            else if (targetIndex == 7)
            {
                Debug.Log("���ڸ� ��������.");
                //���� ����ٴϰ� �ִ� �� ����ϴ�.
                carryingObject = null;
                //�ڽ��� ���� �ε����� Ÿ���ε����� ����
                carryingIndex = targetIndex;
                //define�� �ƴϰ� �ڽ��� �ڽ��� �ڽĿ�����Ʈ�� ����
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
        /// ������ ���������ϴ�.
        /// </summary>
        public void Lay()
        {
            Debug.Log("��������");
            //���� ���ǵ���
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
