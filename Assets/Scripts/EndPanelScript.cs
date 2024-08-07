using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using ZilyanusLib.Audio;

public class EndPanelScript : MonoBehaviour
{
    [SerializeField] GameObject Panel;

    [SerializeField] List<GameObject> Starts;

    [SerializeField] List<GameObject> WinLosePanels;

    [SerializeField] SoundData WinSound;
    [SerializeField] SoundData LoseSound;

    [SerializeField] SoundData StarSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndLevel(int score, int LevelCount)
    {
        if (score == 0)
        {
            AudioClass.PlayAudio(LoseSound);
            WinLosePanels[0].SetActive(true);
        }
        else
        {
            AudioClass.PlayAudio(WinSound);
            WinLosePanels[1].SetActive(true);
        }

        if (ES3.Load("Level " + LevelCount,-1) < score)
            ES3.Save("Level " + LevelCount, score);

        if (ES3.Load("LastLevel", 0) < LevelCount + 1)
            ES3.Save("LastLevel", LevelCount +1 );

        Panel.SetActive(true);
        Panel.GetComponent<RectTransform>().localScale = Vector3.zero;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(Panel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
        sequence.Join(Panel.GetComponent<RectTransform>().DOPunchRotation(Vector3.forward * 15, 0.5f).SetEase(Ease.OutBack));

        sequence.OnComplete(() =>
        {
            sequence.Kill();
            StartCoroutine(StarCome(score));
        });
    }

    IEnumerator StarCome(int score)
    {
        for (int i = 0; i < score; i++)
        {
            Starts[i].SetActive(true);
            Vector3 Scale = Starts[i].GetComponent<RectTransform>().localScale;
            Starts[i].GetComponent<RectTransform>().localScale = Vector3.zero;

            Sequence sequence1 = DOTween.Sequence();

            sequence1.Append(Starts[i].GetComponent<RectTransform>().DOScale(Scale, 0.5f).SetEase(Ease.OutBack));
            sequence1.Join(Starts[i].GetComponent<RectTransform>().DOPunchRotation(Vector3.forward * 15, 0.5f).SetEase(Ease.OutBack));
            sequence1.Join(DOVirtual.DelayedCall(0,() => { AudioClass.PlayAudio(StarSound); }));

            sequence1.OnComplete(() =>
            {                
                sequence1.Kill();
            });
            yield return new WaitForSeconds(0.2f);
        }
    }
}
