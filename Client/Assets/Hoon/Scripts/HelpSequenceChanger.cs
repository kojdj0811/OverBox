using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpSequenceChanger : MonoBehaviour
{
    public int helpOrder = 0;

    private void OnEnable()
    {
        helpOrder = 0;
    }

    private void OnDisable()
    {
        transform.GetChild(helpOrder).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && helpOrder > 0)
        {
            transform.GetChild(helpOrder).gameObject.SetActive(false);
            helpOrder--;
            transform.GetChild(helpOrder).gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && helpOrder < 2)
        {
            transform.GetChild(helpOrder).gameObject.SetActive(false);
            helpOrder++;
            transform.GetChild(helpOrder).gameObject.SetActive(true);
        }


    }


}
