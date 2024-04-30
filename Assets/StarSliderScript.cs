using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StarSliderScript : MonoBehaviour
{
    [SerializeField] float CurrentValue;

    [SerializeField] RectTransform ImageScale;

    public float MovedCount;

    [SerializeField] float TotalMoveCount;

    [SerializeField] List<float> MoveCounts;

    [SerializeField] List<Transform> StarBars;
    [SerializeField] List<Transform> StarBarsTransforms;
    [SerializeField] List<Transform> StarBarsImages;


    [SerializeField] TextMeshProUGUI TotalText;

    [SerializeField] RectTransform BarTransform; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentValue = MovedCount;
        ImageScale.localScale = new Vector2(CurrentValue / TotalMoveCount, ImageScale.localScale.y);
        BarTransform.sizeDelta = new Vector2(146 * (CurrentValue / TotalMoveCount <= 1 ? (CurrentValue / TotalMoveCount) : 1), 40f);

        for (int i = 0; i < MoveCounts.Count; i++)
        {
            if (MoveCounts[i] - CurrentValue > 0)
                StarBarsImages[i].GetComponentInChildren<TextMeshProUGUI>().text = (MoveCounts[i] - CurrentValue).ToString();
            else
                StarBarsImages[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        TotalText.text = TotalMoveCount - CurrentValue >= 0 ?(TotalMoveCount - CurrentValue).ToString() : "0";
    }

    public void GetStarValues(float total,float s2, float s1, float s0)
    {
        TotalMoveCount = s0;
        MoveCounts.Add(total);
        MoveCounts.Add(s1);
        MoveCounts.Add(s2);

        for (int i = 0; i < StarBars.Capacity; i++)
        {
            StarBars[i].localScale = new Vector2(MoveCounts[i] / TotalMoveCount, ImageScale.localScale.y);
            StarBarsImages[i].transform.position = StarBarsTransforms[i].transform.position;   
        }     
    }

    public int GetEndValues()
    {
        if (CurrentValue <= MoveCounts[0])
        {
            return 3;
        }
        else if (CurrentValue <= MoveCounts[1])
        {
            return 2;
        }
        else if (CurrentValue <= MoveCounts[2])
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
