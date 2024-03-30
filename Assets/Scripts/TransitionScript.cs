using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void OnEnable()
    {
        LevelSelector.OnLevelClicked.AddListener(OnLevelClicked);
    }

    private void OnDisable()
    {
        LevelSelector.OnLevelClicked.RemoveListener(OnLevelClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLastLevel()
    {
        LoadLevelViaEndGame(ES3.Load("LastLevel", 0) + 2);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
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
            LoadLevel(Level);
        }
    }

    public void RestartLevel()
    {
        int CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        LoadLevel(CurrentLevel);
    }

    private void OnLevelClicked(int level) 
    {
        LoadLevel(level+2);
    }
}
