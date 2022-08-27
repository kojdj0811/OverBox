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
        /// �÷��̾��� ���� �ε����Դϴ�.
        /// </summary>
        public enum State { Movable, Computer, Audition }

        [Range(0f, 10f)]
        public float power = 10f;
        [Range(0f, 10f)]
        public float boxCastMaxDistance = 1f;
        [Range(0f, 1f)]
        public float keyPressCooldownMax = 0.1f;

        public int carryingIndex; // 0~6:����, 7:�ڽ�, 8:����
        public State state;
        public GameObject carryingObj;

        private bool isSpacebarPressed;
        // ���� ��ǰ ���
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
        /// Ű �Է����� �÷��̾��� �̵��� ó���մϴ�.
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
        /// �÷��̾��� ��ȣ�ۿ��� ó���մϴ�.
        /// </summary>
        void Update()
        {
            isSpacebarPressed = false;

            int layerMask = (1 << LayerMask.GetMask(Ground));
            RaycastHit hit;
            // �÷��̾� �ֺ��� ��ȣ�ۿ� ������ ������Ʈ�� �ִ��� Ȯ���մϴ�.(�������� �߻��� ��ġ, �簢���� �� ��ǥ�� ���� ũ��, �߻� ����, �浹 ���, ȸ�� ����, �ִ� �Ÿ�)
            bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out hit, transform.rotation, boxCastMaxDistance);

            if (Input.GetKeyUp(KeyCode.Space)) isSpacebarPressed = false;

            // �����̽��ٸ� ������ �� �÷��̾� ���¿� ���� ��ȣ�ۿ��մϴ�.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // ���� �Ÿ� ���� ���� ��
                if(isHit && state == State.Movable)
                {
                    // �ڽ�ĳ��Ʈ�� ���� ������Ʈ�� ��ȣ�ۿ� �޼��带 ȣ���մϴ�.
                    //hit.collider.GetComponent<Obstacle>().OnInteractive(this);

                    // �÷��̾��� ���¸� �����մϴ�.
                    string hitColName = hit.collider.name;
                    switch (hit.collider.name)
                    {
                        case Box:
                        case Storage:
                            Carry(hit.collider.gameObject);
                            isSpacebarPressed = true;
                            break;
                        case Computer:
                            Debug.Log("��ǻ�� ���");
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

                // ��ǻ�� ��� ���¶�� ���� �ֹ��� Ȯ���մϴ�.
                if (state == State.Computer && !isSpacebarPressed)
                {
                    Debug.Log("���ֵ�");
                    //Moru.MoruDefine.delegate_Delivery?.invoke(items);
                }

                // ������ ��� �־��ٸ� ���������ϴ�.
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
        /// ������ ��ϴ�.
        /// </summary>
        private void Carry(GameObject obj)
        {
            if (carryingObj) return;

            Debug.Log(obj.name + "�� �����");
            obj.transform.SetParent(transform);
            carryingObj = obj;
            if (obj.name == Box) carryingIndex = 7;
            else carryingIndex = (int)obj.GetComponent<Moru.Storage>().TargetProduct;
        }
        
        /// <summary>
        /// ������ ���������ϴ�.
        /// </summary>
        private void Lay()
        {
            Debug.Log(carryingObj.name + "�� ����");
            carryingObj.transform.SetParent(null);
            carryingObj = null;
            carryingIndex = 8;
        }
    }
}
