using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonEvnet : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        transform.GetChild(1).gameObject.SetActive(true);
  
    }

    public void OnDeselect(BaseEventData eventData)
    {
        transform.GetChild(1).gameObject.SetActive(false);

    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }


}
