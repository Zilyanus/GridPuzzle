using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartAnimationScript : MonoBehaviour
{
    Sequence sequence;

    float MaxScale = 45;

    [SerializeField] Transform RightPanel;
    [SerializeField] Transform LeftPanel;

    [SerializeField] SpriteRenderer MiddlePart;

    public static UnityEvent StartingAnimationEnded = new UnityEvent();

    float Duration = 0.4f;

    [SerializeField] GameObject StartObject;
    [SerializeField] SpriteRenderer Player;
    // Start is called before the first frame update
    void Start()
    {
        sequence = DOTween.Sequence();

        StartObject.SetActive(false);
        Player.enabled = false;
        transform.localScale = new Vector3(MaxScale, MaxScale, 1);

        sequence.AppendInterval(0f);
        sequence.Append(DOVirtual.DelayedCall(0.0f, () =>
        {
            StartObject.SetActive(true);
            Player.enabled = true;
        }));

        sequence.Append(transform.DOScale(1, 1).SetEase(Ease.InCubic));
        sequence.AppendInterval(0.3f);
        sequence.Append(MiddlePart.DOFade(0, 0));

        //Right 1
        sequence.Join(RightPanel.DOScaleX(-0.5671635f, Duration));
        sequence.Join(RightPanel.DOLocalMoveX(0.8436f, Duration));

        //Left 1
        sequence.Join(LeftPanel.DOScaleX(-0.5671635f, Duration));
        sequence.Join(LeftPanel.DOLocalMoveX(-0.8436f, Duration));

        //Right 2
        sequence.Append(RightPanel.DOLocalMoveX(0.5558f, Duration));
        sequence.Join(RightPanel.DOScaleX(0, Duration));

        //Left 2
        sequence.Join(LeftPanel.DOLocalMoveX(-0.5558f, Duration));
        sequence.Join(LeftPanel.DOScaleX(0, Duration));

        sequence.OnComplete(() => { StartingAnimationEnded.Invoke(); gameObject.SetActive(false); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
