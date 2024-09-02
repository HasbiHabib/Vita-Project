using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using static AudioData;
using static Unity.VisualScripting.Member;

namespace Global.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _bgmSource;
        [SerializeField] private AudioData _audioData;
        private bool _isBgmMute = false;

        public static AudioManager Instance;

        public float BGMDefault;
        public float BGMReducerSpeed;
        private float ReducerTime;
        private bool OnReduce;
        private bool OnReduceQuickChange;
        private bool OnTransitionOpening;
        private bool OnTransitionOpeningEnd;
        private string BGMSet;

        public List<sound> SoundListOnScene;
        

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
            for (int i = 0; i < _audioData.SoundList.Count; i++)
            {
                
                AudioSource d =  gameObject.AddComponent<AudioSource>();
                d.playOnAwake = false;
                d.clip = _audioData.SoundList[i].Clip;
                d.volume = _audioData.SoundList[i].Volume;
                d.pitch = _audioData.SoundList[i].pitch;
                d.loop = _audioData.SoundList[i].isLoop;
                d.outputAudioMixerGroup = _audioData.SoundList[i].Output;

                sound b = new sound { SoundName =_audioData.SoundList[i].SoundName, source = d};
                SoundListOnScene.Add(b);
            }
            
        }

        public AudioData GetAudioData()
        {
            return _audioData;
        }

        public void MuteBgm(bool isBgmMute)
        {
            _isBgmMute = isBgmMute;
            if (_isBgmMute == true)
            {
                _bgmSource.Stop();
            }
            else
            {
                _bgmSource.Play();
            }
        }

        public void OpeningTransition(float Timed) 
        {
            OnTransitionOpening = true;
            ReducerTime = Timed;
            StartCoroutine(OpeningTransitionIE(Timed));
        }

        IEnumerator OpeningTransitionIE(float Timed) 
        {
            yield return new WaitForSeconds(5);
            OnTransitionOpeningEnd = true;
            ReducerTime = Timed;
        }

        public void SetCurrentBgmClip(string clip)
        {
            if (_bgmSource.enabled)
            {
                _bgmSource.volume = BGMDefault;
                for (int i = 0; i < _audioData.BgmList.Count; i++)
                {
                    var soundName = _audioData.BgmList[i].BgmName;
                    if (soundName == clip)
                    {
                        var currentClip = _audioData.BgmList[i].Clip;
                        _bgmSource.clip = currentClip;
                        if (!_bgmSource.isPlaying && _bgmSource.clip.name != clip)
                        {
                            if (!_isBgmMute)
                            {

                                _bgmSource.Play();
                            }
                            else if (_isBgmMute)
                            {
                                _bgmSource.Stop();
                            }
                        }
                        break;
                    }
                }
            }
        }
        public void SetCurrentSoundFXClip(string clip)
        {
            sound s = SoundListOnScene.Find(x => x.SoundName == clip);
            if (s != null)
            {
                s.source.Play();
            }
        }

        public void BGMStopper() 
        {
            OnReduce = true;
            ReducerTime = 2;
        }

        public void QuickBGMChanger(string clip, float Timed)
        {
            OnReduceQuickChange = true;
            ReducerTime = Timed;
            BGMSet = clip;
        }

        public void Update()
        {
            if (OnReduce) 
            {
                if (ReducerTime >= 0)
                {
                    _bgmSource.volume -= BGMReducerSpeed *2;
                    ReducerTime -= Time.deltaTime;
                }
                else
                {
                    OnReduce = false;
                }
            }

            if (OnReduceQuickChange)
            {
                if (ReducerTime >= 0)
                {
                    _bgmSource.volume -= BGMReducerSpeed * 2;
                    ReducerTime -= Time.deltaTime;
                }
                else
                {
                    SetCurrentBgmClip(BGMSet);
                    OnReduceQuickChange = false;
                }
            }

            if (OnTransitionOpening)
            {
                if (ReducerTime >= 0)
                {
                    _bgmSource.volume -= BGMReducerSpeed * 3;
                    ReducerTime -= Time.deltaTime;
                }
                else
                {
                    OnTransitionOpening = false;
                }
            }
            if (OnTransitionOpeningEnd)
            {
                if (ReducerTime >= 0)
                {
                    if (_bgmSource.volume <= BGMDefault)
                    {
                        _bgmSource.volume += BGMReducerSpeed * 3;
                    }
                    ReducerTime -= Time.deltaTime;
                }
                else
                {
                    OnTransitionOpeningEnd = false;
                }
            }
        }

    }
    [System.Serializable]
    public class sound
    {
        public string SoundName;
        public AudioSource source;
    }
}

