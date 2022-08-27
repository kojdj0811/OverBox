using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private float idleOffset = 0.00001f;
    private Vector3 prevPos;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        prevPos = transform.parent.localPosition;
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // 이전 프레임과 현재 프레임의 플레이어 위치 변화에 따라 Idle/Walk 상태를 결정합니다.
        float offsetX = Mathf.Abs(transform.parent.localPosition.x - prevPos.x);
        float offsetZ = Mathf.Abs(transform.parent.localPosition.z - prevPos.z);
        prevPos = transform.parent.localPosition;
        anim.SetBool("is_walking", offsetX > idleOffset || offsetZ > idleOffset);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetBool("is_walking", true);
            transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetBool("is_walking", true);
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
}