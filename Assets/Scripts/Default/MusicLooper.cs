using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicLooper: MonoBehaviour
{
    [SerializeField] GameObject NewMusic;
    [SerializeField] List<MusicLooper> NewMusics;
    private void Awake()
    {
        NewMusic = FindObjectOfType<MusicLooper>().gameObject;
        if (NewMusic != null && NewMusic != gameObject)
        {
            if (NewMusic.GetComponent<AudioSource>().clip != GetComponent<AudioSource>().clip)
            {
                Destroy(NewMusic);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

#if UNITY_EDITOR
    [MenuItem("GameObject/Audio/Music Looper")]
    static void CreateMusicObject()
    {
        GameObject MusicObject = new GameObject();
        MusicObject.name = "Music Looper";
        MusicObject.AddComponent<MusicLooper>();
        MusicObject.GetComponent<AudioSource>().loop = true;
    }
    #endif
}
