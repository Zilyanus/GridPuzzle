using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[SelectionBase]
public class LevelSelector : MonoBehaviour
{
    [SerializeField] int StarRequired = 0;

    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] int LevelIndex;
    int StarValue;
    [SerializeField] TextMeshProUGUI LevelText;

    bool isLocked;
    [SerializeField] GameObject LockImage;
    [SerializeField] GameObject Stars;
    [SerializeField] List<Image> StarObjects;

    [SerializeField] GameObject DarkCircle;

    [SerializeField] Transform MainObject;

    [SerializeField] SpriteRenderer BiggerCircle;

    [SerializeField] Color LockColor;

    public static UnityEvent<int> OnLevelClicked = new UnityEvent<int>();

    [SerializeField] GameObject BarierObject;
    [SerializeField] TextMeshProUGUI BarierObjectText;

    LevelSelectionManager levelSelectionManager;
    // Start is called before the first frame update
    void Start()
    {
        levelSelectionManager = GetComponentInParent<LevelSelectionManager>();

        StarValue = ES3.Load("Level " + LevelIndex, -1);
        LevelText.text = (LevelIndex + 1).ToString();
        isLocked = ES3.Load("LastLevel",0) >= LevelIndex ? false : true;
        Debug.Log(ES3.Load("LastLevel", 0));

        if (ES3.Load("LastLevel", 0) == LevelIndex)
        {
            lineRenderer.startColor = LockColor;
            lineRenderer.endColor = LockColor;
        }

        if (isLocked)
        {
            Stars.SetActive(false);
            LevelText.gameObject.SetActive(false);
            lineRenderer.startColor = LockColor;
            lineRenderer.endColor = LockColor;
            BiggerCircle.color = LockColor;

            if (LevelIndex == ES3.Load("LastLevel", 0) + 1)
            {
                LockImage.SetActive(true);
            }
            else
            {
                
                DarkCircle.SetActive(false);
            }
        }

        if (StarValue != -1)
        {
            Stars.SetActive(true);

            for (int i = 0; i < StarValue; i++)
            {
                StarObjects[i].color = Color.white;
            }    
        }
        else
        {
            Stars.SetActive(false);
        }

        if (StarRequired <= levelSelectionManager.TotalStar)
        {
            
        }
        else if (StarRequired > 0)
        {
            isLocked = true;
            BarierObject.SetActive(true);
            BarierObject.transform.position = (transform.position + levelSelectionManager.GetPos(LevelIndex - 1)) / 2;
            BarierObjectText.text = (StarRequired - levelSelectionManager.TotalStar).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetData(int levelIndex, Vector2 Pos)
    {
        LevelIndex = levelIndex;
        lineRenderer.SetPosition(1, transform.InverseTransformPoint(Pos));
        ES3.Save("Level" + LevelIndex + "Req", StarRequired);
        LevelText.text = (LevelIndex + 1).ToString();
    }

    private void OnMouseEnter()
    {
        if (!isLocked)
            MainObject.DOScale(1.2f, 0.3f);
    }

    private void OnMouseExit()
    {
        if (!isLocked)
            MainObject.DOScale(1f, 0.3f);
    }

    private void OnMouseUp()
    {
        if (!isLocked)
            OnLevelClicked.Invoke(LevelIndex);
    }
}
