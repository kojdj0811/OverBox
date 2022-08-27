using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyerin
{
    public class Player : SingleToneMono<Player>
    {
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


        public bool isCarrying;
        public int carryingObj; // 0~6:����, 7:�ڽ�, 8:����
        public State state;
        //public Moru.MoruDefine.Product carryingProduct;

        private float keyPressCooldown;
        // ���� ��ǰ ���
        private int[] items;
        private Animator anim;
        private Rigidbody rb;


        void Start()
        {
            isCarrying = false;
            carryingObj = 8;
            state = State.Movable;
            keyPressCooldown = 0f;
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }

        void OnDrawGizmos()
        {
            int layerMask = (1 << LayerMask.GetMask("Ground"));
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
            if (keyPressCooldown > 0f) keyPressCooldown -= Time.deltaTime;

            int layerMask = (1 << LayerMask.GetMask("Ground"));
            RaycastHit hit;
            // �÷��̾� �ֺ��� ��ȣ�ۿ� ������ ������Ʈ�� �ִ��� Ȯ���մϴ�.(�������� �߻��� ��ġ, �簢���� �� ��ǥ�� ���� ũ��, �߻� ����, �浹 ���, ȸ�� ����, �ִ� �Ÿ�)
            bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out hit, transform.rotation, boxCastMaxDistance);

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
                    Debug.Log(hitColName);
                    switch (hit.collider.name)
                    {
                        case "Box":
                            break;
                        case "Computer":
                            Debug.Log("��ǻ�� ���");
                            state = State.Computer;
                            keyPressCooldown = keyPressCooldownMax;
                            break;
                        case "ConveyorBelt":
                            state = State.Audition;
                            break;
                        case "Storage":
                            break;
                        default:
                            break;
                    }
                }

                // ��ǻ�� ��� ���¶�� ���� �ֹ��� Ȯ���մϴ�.
                if (state == State.Computer && keyPressCooldown<=0)
                {
                    Debug.Log("���ֵ�");
                    //Moru.MoruDefine.delegate_Delivery?.invoke(items);
                }
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
    }
}
