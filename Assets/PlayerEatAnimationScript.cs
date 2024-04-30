using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEatAnimationScript : MonoBehaviour
{
    [SerializeField] Animator animator;

    private void OnEnable()
    {
        GameManager.OnLastFishAted += LastEatingAnimation;
        GameManager.OnFishAted += EatingAnimation;
    }

    private void OnDisable()
    {
        GameManager.OnLastFishAted -= LastEatingAnimation;
        GameManager.OnFishAted -= EatingAnimation;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LastEatingAnimation()
    {
        animator.Play("CatLastEatAnim");
    }

    void EatingAnimation()
    {

    }
}
