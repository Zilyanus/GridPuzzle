using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Audio;

public class CoolMathAds : MonoBehaviour
{
    public static CoolMathAds instance;
    [SerializeField] AudioMixer audioMixer;
    float LastValue;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(this);
        }
    }


    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    [Button]
    public void PauseGame()
    {
        Debug.Log("PauseGame function called");
        Time.timeScale = 0f;
        audioMixer.GetFloat("Master", out LastValue);
        audioMixer.SetFloat("Master", -80);
        //ADD LOGIC TO MUTE SOUND HERE
    }

    [Button]
    public void ResumeGame()
    {
        Debug.Log("ResumeGame function called");
        Time.timeScale = 1.0f;
        audioMixer.SetFloat("Master", LastValue);
        //ADD LOGIC TO UNMUTE SOUND HERE
    }
    public void InitiateAds()
    {
        triggerAdBreak();
    }
    [DllImport("__Internal")]
    private static extern void triggerAdBreak();
}
