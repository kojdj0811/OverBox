using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyerinAnimation : MonoBehaviour
{
#pragma warning disable CS0414
    private float idleOffset = 0.00001f;
#pragma warning restore CS0414


    private string animState;
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
        if(prevPos == transform.parent.localPosition)
        {
            anim.SetFloat("X", 0);
            anim.SetFloat("Z", 0);
        }
        prevPos = transform.parent.localPosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetFloat("X", -1);
            anim.SetFloat("Z", 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetFloat("X", 1);
            anim.SetFloat("Z", 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetFloat("X", 0);
            anim.SetFloat("Z", 1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetFloat("X", 0);
            anim.SetFloat("Z", -1);
        }
    }
}