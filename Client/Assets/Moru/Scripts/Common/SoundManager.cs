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
    [ShowInInspector]
    private static AudioMixerGroup SFX;

    [SerializeField]
    private AudioClip BGMclip;
    [SerializeField]
    private AudioClip[] sfxclips;

    public enum SFXClips
    {
        Computer, Alarm, PutBox, Convaynor
    }

    
    public  AudioSource bgm_AudioSource;
    public  AudioSource SFX_AudioSource;

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

    private void Start()
    {
        PlayBGM(BGMclip);
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
        var SM = SoundManager.Instance;
        SM.bgm_AudioSource.loop = true;
        SM.bgm_AudioSource.clip = clip;
        SM.bgm_AudioSource.Play();
    }

    public static void PlaySFX(SFXClips clip)
    {
        var SM = SoundManager.Instance;
        SM.SFX_AudioSource.PlayOneShot(SM.sfxclips[(int)clip]);
    }
}
