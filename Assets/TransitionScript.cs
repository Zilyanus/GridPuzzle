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

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadNextLevel()
    {
        int NextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        LoadLevel(NextLevel);
    }

    public void RestartLevel()
    {
        int CurrentLevel = SceneManager.GetActiveScene().buildIndex;
        LoadLevel(CurrentLevel);
    }
}
