using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMainMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.Find("Intro").gameObject.SetActive(true);
            transform.Find("Help").gameObject.SetActive(false);
            transform.Find("Credit").gameObject.SetActive(false);
        }
    }
}
