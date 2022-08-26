using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hyerin;

public class TestObstacle : Obstacle
{
    /// <summary>
    /// �о����� ��
    /// </summary>
    public float pushAmount;

    /// <summary>
    /// �о����� ������
    /// </summary>
    public Vector3 threshold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
            OnHit(collision);
        if(collision.gameObject.GetComponent<Player>())
        {
        }
    }

    public override void OnHit(Collision collision)
    {
        var velocity = collision.rigidbody.velocity;
        if(velocity.sqrMagnitude > threshold.sqrMagnitude)
        {
            collision.rigidbody.AddForce(velocity * -1* pushAmount*1000);
        }
    }
}
