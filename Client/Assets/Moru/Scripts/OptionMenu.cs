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
}
