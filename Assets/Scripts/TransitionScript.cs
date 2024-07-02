using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    Animator animator;

    int SceneToLoad;

    public static event Action<int> OnTransitionTriggered;
    private void OnEnable()
    {
        CharacterLevelIcon.OnLevelTranslate.AddListener(OnLevelClicked);
    }

    private void OnDisable()
    {
        CharacterLevelIcon.OnLevelTranslate.RemoveListener(OnLevelClicked);
    }

    public void StartGameViaMainMenu()
    {
        CoolMathApiScript.StartGameEvent();
        LoadLastLevel();
    }

    public void LoadLastLevel()
    {
        LoadLevelViaEndGame(ES3.Load("LastLevel", 0) + 2);
    }

    public void LoadLevel(int index, bool isRestart = false)
    {
        if (index > 1 && !isRestart)
            CoolMathApiScript.StartLevelEvent(index - 1);
        SceneToLoad = index;
        OnTransitionTriggered.Invoke(SceneToLoad);
    }

    [Button]
    public void LoadNextLevel()
    {
        int NextLevel = SceneManager.GetActiveScene().buildIndex + 1;

        if (NextLevel < SceneManager.sceneCountInBuildSettings) 
        {
            LoadLevel(NextLevel);
        }
    }

    public void LoadNextLevelViaEndGame()
    {
        if (ES3.Load("Level"+ (SceneManager.GetActiveScene().buildIndex-1)+"Req", 0) > 0)
        {
            LoadLevel(1);
        }
        else
        {
            LoadNextLevel();
        }
    }

    public void LoadLevelViaEndGame(int Level)
    {
        if (ES3.Load("Level" + (SceneManager.GetActiveScene().buildIndex - 1) + "Req", 0) > 0)
        {
            LoadLevel(1);
        }
        else
        {
            CoolMathAds.instance.InitiateAds();
            LoadLevel(Level);
        }
    }

    public void RestartLevel()
    {
        CoolMathApiScript.ReplayEvent();
        int CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        LoadLevel(CurrentLevel,true);
    }

    private void OnLevelClicked(int level) 
    {
        LoadLevel(level+2);
    }

    bool AnimatorIsFinished()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1;
    }
}
