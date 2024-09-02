using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SocialPlatforms;


[CreateAssetMenu]
[System.Serializable]
public class AudioData : ScriptableObject
{
    [System.Serializable]
    public struct Bgm
    {
        public string BgmName;
        public AudioClip Clip;
    }
    [System.Serializable]
    public struct Sound
    {
        public string SoundName;
        public AudioClip Clip;
        public AudioMixerGroup Output;
        [Range(0f, 1f)] public float Volume;
        [Range(1f, 5f)] public float pitch;
        public bool isLoop;
    }

    public List<Bgm> BgmList;
    public List<Sound> SoundList;
}
