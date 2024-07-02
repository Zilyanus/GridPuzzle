using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class CoolMathApiScript : MonoBehaviour
{
    [SerializeField] bool isSiteLock;

#if UNITY_WEBGL
    public string[] domains = new string[] 
    {
        "https://www.coolmath-games.com",
        "www.coolmath-games.com",
        "edit.coolmath-games.com",
        "www.stage.coolmath-games.com",
        "edit-stage.coolmath-games.com",
        "dev.coolmath-games.com",
        "m.coolmath-games.com",
        "https://www.coolmathgames.com",
        "www.coolmathgames.com",
        "edit.coolmathgames.com",
        "www.stage.coolmathgames.com",
        "edit-stage.coolmathgames.com",
        "dev.coolmathgames.com",
        "m.coolmathgames.com",
        "https://edit.coolmathgames.com",
        "https://www.stage.coolmathgames.com",
        "https://edit-stage.coolmathgames.com",
        "https://dev.coolmathgames.com",
        "https://m.coolmathgames.com"
    };

    // Import the javascript function that redirects to another URL
    [DllImport("__Internal")]
    public static extern void RedirectTo(string url);



    public static CoolMathApiScript instance;

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

    // Check right away if the domain is valid
    private void Start()
    {
#if !UNITY_EDITOR
        if(SceneManager.GetActiveScene().buildIndex == 0 && isSiteLock)
        {
            CheckDomains();            
        }            
#endif
    }

    // Import the javascript function that redirects to another URL
    [DllImport("__Internal")]
    public static extern void StartGameEvent();

    // Import the javascript function that redirects to another URL
    [DllImport("__Internal")]
    public static extern void StartLevelEvent(int level);

    // Import the javascript function that redirects to another URL
    [DllImport("__Internal")]
    public static extern void ReplayEvent();


    private void CheckDomains()
    {
#if !UNITY_EDITOR
        if (!IsValidHost(domains))
        {
            RedirectTo("www.coolmathgames.com");
        }
#else

#endif
    }

    private bool IsValidHost (string[] hosts)
    {
        foreach (string host in hosts)
            if(Application.absoluteURL.IndexOf(host) ==0)
                return true;
        return false;
    }


    public void StartGame()
    {
#if !UNITY_EDITOR
        if(isSiteLock)
            StartGameEvent();
#endif
    }

    public void StartLevel(int level)
    {
#if !UNITY_EDITOR
        if(isSiteLock)
            StartLevelEvent(level);
#endif
    }

    public void Replay()
    {
#if !UNITY_EDITOR
        if(isSiteLock)
            ReplayEvent();
#endif
    }
#endif
    }
