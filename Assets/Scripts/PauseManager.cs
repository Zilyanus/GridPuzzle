using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseManager : MonoBehaviour
{

    [SerializeField] GameObject PausePanel;

    public static UnityEvent<bool> OnPausedChanged = new UnityEvent<bool>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPause(bool value)
    {
        OnPausedChanged.Invoke(value);

        if (value)
        {
            PausePanel.SetActive(true);
            PausePanel.GetComponent<RectTransform>().localScale = Vector3.zero; PausePanel.GetComponent<RectTransform>().localScale = Vector3.zero;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(PausePanel.GetComponent<RectTransform>().DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
            sequence.Join(PausePanel.GetComponent<RectTransform>().DOPunchRotation(Vector3.forward * 15, 0.5f).SetEase(Ease.OutBack));

            sequence.OnComplete(() =>
            {
                sequence.Kill();
            });
        }
        else
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(PausePanel.GetComponent<RectTransform>().DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() => PausePanel.SetActive(false)));

            sequence.OnComplete(() =>
            {
                sequence.Kill();
            });
        }
    }
}
