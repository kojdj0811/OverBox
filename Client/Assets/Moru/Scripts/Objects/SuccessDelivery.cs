using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessDelivery : MonoBehaviour
{
    public float ExistTime = 1.5f;

    private void Update()
    {
        ExistTime -= Time.deltaTime;
        if(ExistTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
