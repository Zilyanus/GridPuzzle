using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{
    [SerializeField] GameObject OldText;
    [SerializeField] GameObject NewText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCreditsClicked()
    {
        OldText.SetActive(false);
        NewText.SetActive(true);
    }
}
