using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EndPanelScript : MonoBehaviour
{
    [SerializeField] GameObject Panel;

    [SerializeField] List<GameObject> Starts;

    [SerializeField] List<GameObject> WinLosePanels;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndLevel(int score)
    {
        if (score == 0)
        {
            WinLosePanels[0].SetActive(true);
        }
        else
        {
            WinLosePanels[1].SetActive(true);
        }

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

            sequence1.OnComplete(() =>
            {
                sequence1.Kill();
            });
            yield return new WaitForSeconds(0.2f);
        }
    }
}
