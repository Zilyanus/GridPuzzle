using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeTrigger : MonoBehaviour
{
    [SerializeField] int Index;
    public static event Action<int> OnThemeTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnThemeTrigger.Invoke(Index);
    }
}
