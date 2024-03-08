using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanelScript : MonoBehaviour
{
    [SerializeField] Transform GoPos;

    [SerializeField] GameObject Panel;
    [SerializeField] Image BlackPanel;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<RectTransform>().localScale = Vector3.zero; GetComponent<RectTransform>().localScale = Vector3.zero;

        Sequence sequence = DOTween.Sequence();

        sequence.Append(GetComponent<RectTransform>().DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
        sequence.Join(GetComponent<RectTransform>().DOPunchRotation(Vector3.forward * 15, 0.5f).SetEase(Ease.OutBack));

        sequence.AppendInterval(1f);

        sequence.Append(GetComponent<RectTransform>().DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() => Panel.SetActive(false)));
        sequence.Join(BlackPanel.DOFade(0,0.5f));

        sequence.OnComplete(() =>
        {
            sequence.Kill();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
