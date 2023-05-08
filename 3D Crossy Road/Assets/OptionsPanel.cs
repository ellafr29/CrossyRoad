using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
   [SerializeField] AudioManager audioManager;
   [SerializeField] Toggle mutetoggle;
   [SerializeField] Slider bgmSlider;
   [SerializeField] Slider sfxSlider;
   [SerializeField] TMP_Text bgmVolText;
   [SerializeField] TMP_Text sfxVolText;


   private void OnEnable()
   {
    mutetoggle.isOn = audioManager.IsMute;
    bgmSlider.value = audioManager.BgmVolume;
    sfxSlider.value = audioManager.SfxVolume;
    SetBgmVolText(value: bgmSlider.value);
    SetSfxVolText(value: sfxSlider.value);
   }

   public void SetBgmVolText(float value)
   {
    bgmVolText.text = Mathf.RoundToInt (f: value * 100).ToString();
   }

   public void SetSfxVolText(float value)
   {
    sfxVolText.text = Mathf.RoundToInt (f: value *100).ToString();

   }
}
