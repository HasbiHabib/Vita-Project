using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace Global.Option
{
    public class option : MonoBehaviour
    {
        [Header("Sound")]
        public Slider sfx;
        public Slider BGM;
        public AudioMixer BGMs;
        public AudioMixer SFXs;

        private void Start()
        {
            BGM_(BGM.value);
            SFX_(sfx.value);
        }

        public void LoadSetting(float sfxs, float bgm)
        {
            BGM.value = bgm;
            sfx.value = sfxs;
            BGM_(BGM.value);
            SFX_(sfx.value);
        }

        public void BGM_(float slidervalue)
        {
            BGMs.SetFloat("music", Mathf.Log10(slidervalue) * 20);
        }
        public void SFX_(float slidervalue)
        {
            SFXs.SetFloat("sfx", Mathf.Log10(slidervalue) * 20);
        }

        public void SaveOption()
        {
            FindObjectOfType<savedata>().Saveoptionssetting(sfx.value, BGM.value);
        }
    }
}
