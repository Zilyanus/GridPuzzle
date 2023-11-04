using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    [SerializeField] bool isSoundMuted;
    [SerializeField] bool isMusicMuted;

    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Image SoundImage;
    [SerializeField] List<Sprite> SoundSprites;

    [SerializeField] Image MusicImage;
    [SerializeField] List<Sprite> MusicSprites;

    [SerializeField] Slider SoundSlider;
    [SerializeField] Slider MusicSlider;

    [SerializeField] float OldSoundVolume;
    [SerializeField] float OldMusicVolume;

    // Start is called before the first frame update
    void Start()
    {       
        if(SoundSlider != null)
            SoundSlider.value = PlayerPrefs.GetFloat("Sound");
        if (MusicSlider != null)
            MusicSlider.value = PlayerPrefs.GetFloat("Music");

        isSoundMuted = PlayerPrefs.GetInt("isSoundMuted") == 1 ? true : false;
        isMusicMuted = PlayerPrefs.GetInt("isMusicMuted") == 1 ? true : false;

        OldSoundVolume = PlayerPrefs.GetFloat("OldSoundVolume");
        OldMusicVolume = PlayerPrefs.GetFloat("OldMusicVolume");
    }

    // Update is called once per frame
    void Update()
    {
        if (SoundSlider != null)
            PlayerPrefs.SetFloat("Sound", SoundSlider.value);
        if (MusicSlider != null)
            PlayerPrefs.SetFloat("Music", MusicSlider.value);

        if(SoundImage != null)
            SoundImage.sprite = isSoundMuted ? SoundSprites[0] : SoundSprites[1];
        if(MusicImage != null)
            MusicImage.sprite = isMusicMuted ? MusicSprites[0] : MusicSprites[1];
    }

    public void SoundSliderChange(float value)
    {
        SoundSlider.value = value;
        if (value > -80)
            isSoundMuted = false;
        else
        {
            isSoundMuted = true;
        }

        PlayerPrefs.SetInt("isSoundMuted", isSoundMuted ? 1 : 0);
        audioMixer.SetFloat("Sound", value);
    }

    public void MusicSliderChange(float value)
    {
        MusicSlider.value = value;
        if (value > -80)
            isMusicMuted = false;
        else
        {
            isMusicMuted = true;
        }

        PlayerPrefs.SetInt("isMusicMuted", isMusicMuted ? 1 : 0);
        audioMixer.SetFloat("Music", value);      
    }

    public void PressSoundButton()
    {
        Debug.Log("Sound");
        isSoundMuted = !isSoundMuted;

        PlayerPrefs.SetInt("isSoundMuted", isSoundMuted ? 1 : 0);

        if (isSoundMuted)
        {
            OldSoundVolume = SoundSlider.value;
            PlayerPrefs.SetFloat("OldSoundVolume", OldSoundVolume);
            SoundSlider.value = -80;
        }
        else
        {
            SoundSlider.value = OldSoundVolume;
        }
    }
    public void PressMusicButton()
    {
        isMusicMuted = !isMusicMuted;

        PlayerPrefs.SetInt("isMusicMuted", isMusicMuted ? 1 : 0);

        if (isMusicMuted)
        {
            OldMusicVolume = MusicSlider.value;
            PlayerPrefs.SetFloat("OldMusicVolume", OldSoundVolume);
            MusicSlider.value = -80;
        }
        else
        {
            MusicSlider.value = OldMusicVolume;
        }
    }
}
