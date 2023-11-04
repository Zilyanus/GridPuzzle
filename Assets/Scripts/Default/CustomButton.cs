using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ZilyanusLib.Audio;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[ExecuteAlways]
public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
#if UNITY_EDITOR
    [BackgroundColor(0, 0, 0, 1)]
#endif
    [TextArea(3, 10), SerializeField] public string Text = "<b>Custom Button</b>";


    [SerializeField] float TextSize = 20;

#if UNITY_EDITOR
    [BackgroundColor(1, 1, 1, 1)]
#endif
    [SerializeField] bool AutoSize;

    [SerializeField] public Color TextColor = new Color(0.5f, 0, 1);

#if UNITY_EDITOR
    [BackgroundColor(1, 0, 0, 1)]
#endif

    [SerializeField] UnityEvent ButtonDown;
    [SerializeField] UnityEvent ButtonUp;

    [Header("Sound")]
#if UNITY_EDITOR
    [BackgroundColor(1, 1, 0, 1)]
#endif
    public string ButtonSound;
    [Header("Or")]
    [SerializeField] AudioClip ButtonClip;

    Animator animator;
    TextMeshProUGUI myText;

    Vector3 DefaultScale;

    [SerializeField] bool DontOverwriteText;

    Button button;

    [SerializeField] float ButtonScaleFactor = 1.25f;
    private void Start()
    {
        animator = GetComponent<Animator>();
        DefaultScale = transform.localScale;

        button = GetComponent<Button>();
    }
   
    private void Update()
    {
        if (!DontOverwriteText)
        {
            if (myText != null)
            {
                myText.text = Text;
                if (AutoSize)
                {
                    myText.enableAutoSizing = true;
                }
                else
                {
                    myText.fontSize = TextSize;
                    myText.enableAutoSizing = false;
                }
                myText.color = TextColor;
            }
            else
            {
                myText = GetComponentInChildren<TextMeshProUGUI>();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonDown.Invoke();

        if (animator != null)
            animator.SetBool("Click", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ButtonUp.Invoke();
        AudioClass.PlayAudio("ButtonSound",0.2f);
        if (animator != null)
            animator.SetBool("Click", false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (animator != null)
            animator.SetBool("On", true);
        else if (!Application.isMobilePlatform && (button == null || button.interactable))
            transform.localScale = new Vector3(DefaultScale.x * ButtonScaleFactor, DefaultScale.y * ButtonScaleFactor, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator != null)
            animator.SetBool("On", false);
        else if (!Application.isMobilePlatform && (button == null || button.interactable))
            transform.localScale = DefaultScale;
    }

    private void OnDisable()
    {
        transform.localScale = DefaultScale;
    }
}
