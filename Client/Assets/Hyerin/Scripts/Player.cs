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
        /// �÷��̾��� ���� �ε����Դϴ�.
        /// </summary>
        public enum State { Movable, Computer, Audition }

        [Range(0f, 10f)]
        public float power = 10f;
        [Range(0f, 1f)]
        public float keyPressCooldownMax = 0.1f;
        [Range(1f, 5f)]
        public float boxSize = 1.5f;

        public int carryingIndex; // 0~6:����, 7:�ڽ�, 8:����
        public State state;
        public GameObject carryingBox;

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
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(boxSize, boxSize, boxSize));
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

            int layerMask = (1 << (LayerMask.GetMask(Ground)) | (1 << (LayerMask.GetMask(Tag_Player))));
            RaycastHit hit;
            // �÷��̾� �ֺ��� ��ȣ�ۿ� ������ ������Ʈ�� �ִ��� Ȯ���մϴ�.
            Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(boxSize, boxSize, boxSize), Quaternion.identity, layerMask);

            if (Input.GetKeyUp(KeyCode.Space)) isSpacebarPressed = false;

            // �����̽��ٸ� ������ �� �÷��̾� ���¿� ���� ��ȣ�ۿ��մϴ�.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // ���� �Ÿ� ���� ���� ��
                if(hitColliders.Length>0 && state == State.Movable)
                {
                    // �ڽ�ĳ��Ʈ�� ���� ������Ʈ�� ��ȣ�ۿ� �޼��带 ȣ���մϴ�.
                    hitColliders[0].GetComponent<Obstacle>().OnInteractive(this);

                    // �÷��̾��� ���¸� �����մϴ�.
                    if (hitColliders[0].CompareTag(Box) || hitColliders[0].CompareTag(Storage))
                    {
                        Carry(hitColliders[0].gameObject);
                        isSpacebarPressed = true;
                    }
                    if (hitColliders[0].CompareTag(Computer))
                    {
                        Debug.Log("��ǻ�� ���");
                        state = State.Computer;
                        isSpacebarPressed = true;
                    }
                    if (hitColliders[0].CompareTag(ConveyorBelt))
                    {
                        state = State.Audition;
                    }
                }

                // ��ǻ�� ��� ���¶�� ���� �ֹ��� Ȯ���մϴ�.
                if (state == State.Computer && !isSpacebarPressed)
                {
                    Debug.Log("���ֵ�");
                    Moru.MoruDefine.delegate_Delivery?.Invoke(items);
                }

                // ������ ��� �־��ٸ� ���������ϴ�.
                if (IsCarrying() && !isSpacebarPressed) Lay();
            }

            #region QWEASD ó��
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
        /// ������ ��ϴ�.
        /// </summary>
        private void Carry(GameObject obj)
        {
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
        }
        
        /// <summary>
        /// ������ ���������ϴ�.
        /// </summary>
        private void Lay()
        {
            Debug.Log("��������");
            if (carryingBox)
            {
                carryingBox.transform.SetParent(null);
                carryingBox = null;
            }
            carryingIndex = 8;
        }
    }
}
