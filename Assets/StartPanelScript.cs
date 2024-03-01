using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanelScript : MonoBehaviour
{
    [SerializeField] Transform GoPos;

    // Start is called before the first frame update
    void Start()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(0.8f);

        sequence.Append(GetComponent<RectTransform>().DOMoveY(GoPos.GetComponent<RectTransform>().position.y, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
