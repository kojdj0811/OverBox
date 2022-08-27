using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    public GameObject ButtonList;

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

    public void LoadScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }



    public void OnFullScreen()
    {
        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }

    public void OnWindowScreen()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void Update()
    {
        if(Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnExitAtMainMenu();
        }
    }

    public void OnExitAtMainMenu()
    {
        if(ButtonList)
        {
            ButtonList.SetActive(true);
        }
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
