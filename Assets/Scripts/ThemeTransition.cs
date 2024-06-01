using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeTransition : MonoBehaviour
{
    [SerializeField] List<Color> BgColors;
    [SerializeField] List<Color> SpriteColors;

    [SerializeField] SpriteRenderer BgSprite;
    [SerializeField] SpriteRenderer MainSprite;

    private void OnEnable()
    {
        ThemeTrigger.OnThemeTrigger += ChangeTheme;
    }
    private void OnDisable()
    {
        ThemeTrigger.OnThemeTrigger -= ChangeTheme;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Button]
    void ChangeTheme(int index)
    {
        BgSprite.DOColor(BgColors[index], 0.3f);
        MainSprite.DOColor(SpriteColors[index], 0.3f);
    }
}
