using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : SingleToneMono<SoundManager>
{
    [ShowInInspector]
    public static AudioMixer mixer;
    [ShowInInspector]
    private static AudioMixerGroup BGM;
    private static AudioMixerGroup SFX;

    public static AudioSource bgm_AudioSource;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);

        //ref วาด็
        mixer = Resources.Load<AudioMixer>("GeneralAudioMixer");
        BGM = mixer.FindMatchingGroups("Master")[1];
        SFX = mixer.FindMatchingGroups("Master")[2];
        bgm_AudioSource = GetComponent<AudioSource>();
        bgm_AudioSource.outputAudioMixerGroup = BGM;
        bgm_AudioSource.loop = true;
    }

    public void OnLevelWasLoaded(int level)
    {
        var audios = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < audios.Length; i++)
        {
            if (audios[i] != bgm_AudioSource)
            {
                audios[i].outputAudioMixerGroup = SFX;
            }
        }
    }


    public static void PlayBGM(AudioClip clip)
    {
        bgm_AudioSource.loop = true;
        bgm_AudioSource.clip = clip;
        bgm_AudioSource.Play();
    }

    public static void PlaySFX()
    {

    }
}
