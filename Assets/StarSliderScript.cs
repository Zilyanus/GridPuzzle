using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSliderScript : MonoBehaviour
{
    [SerializeField] float MaxValue;
    [SerializeField] float CurrentValue;

    [SerializeField] RectTransform ImageScale;

    public float MovedCount;

    [SerializeField] float TotalMoveCount;
    [SerializeField] float MoveCountFor2Star;
    [SerializeField] float MoveCountFor1Star;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentValue = MaxValue - MovedCount >= 0 ? MaxValue - MovedCount : 0;
        ImageScale.localScale = new Vector2(CurrentValue / MaxValue, ImageScale.localScale.y);
    }

    public void GetStarValues(float total,float s2, float s1, float s0)
    {
        TotalMoveCount = total;
        MoveCountFor2Star = s2;
        MoveCountFor1Star = s1;
        MaxValue = s0;
    }
}
