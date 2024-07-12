using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using ZilyanusLib.Audio;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] GameObject LoaderCanvas;
    [SerializeField] Image BgImage;
    [SerializeField] Animator animator;

    AudioSource audioSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        TransitionScript.OnTransitionTriggered += LoadScene;
    }

    private void OnDisable()
    {
        TransitionScript.OnTransitionTriggered -= LoadScene;
    }

    public async void LoadScene(int index)
    {
        audioSource.Play();
        var scene = SceneManager.LoadSceneAsync(index);
        scene.allowSceneActivation = false;

        LoaderCanvas.SetActive(true);
        BgImage.DOFade(1, 0.3f);
        animator.Play("TransitionCatStart");
        
        await UniTask.DelayFrame(100);

        while (scene.progress < 0.9f)
        {
            await UniTask.DelayFrame(100);
        }

        scene.allowSceneActivation = true;

        animator.SetTrigger("End");

        BgImage.DOFade(0, 1.5f);        
    }

    public void ClosePanel()
    {
        LoaderCanvas.SetActive(false);
    }
}
