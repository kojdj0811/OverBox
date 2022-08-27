using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    public void OnSliderBGM(float value)
    {
        SoundManager.mixer.SetFloat("bgm_Volume", value);
    }

    public void OnSliderSFX(float value)
    {
        SoundManager.mixer.SetFloat("sfx_Volume", value);
    }

    public void OnSliderMaster(float value)
    {
        SoundManager.mixer.SetFloat("Master_Volume", value);
    }

    public void OnRestart()
    {
        var curScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(curScene);
    }

    public void OnExit()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void OnFullScreen()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }

    public void OnWindowScreen()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }


    public void Update()
    {
        if(Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
    }
}
