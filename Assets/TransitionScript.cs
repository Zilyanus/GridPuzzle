using DG.Tweening;
using Sirenix.OdinInspector;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        GameManager.OnGameCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        GameManager.OnGameCompleted -= OnLevelCompleted;
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

    public void RestartLevel()
    {
        int CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        LoadLevel(CurrentLevel);
    }

    public void OnLevelCompleted()
    {
        DOVirtual.DelayedCall(0.5f, () => LoadNextLevel());
    }
}
