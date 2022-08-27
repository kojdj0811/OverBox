using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLoader : MonoBehaviour
{
    // Start is called before the first frame update
  
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            transform.GetChild(0).Find("Intro").gameObject.SetActive(true);
            transform.GetChild(0).Find("Help").gameObject.SetActive(false);
            transform.GetChild(0).Find("Credit").gameObject.SetActive(false);
        }
    }
}
