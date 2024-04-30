using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugObject : MonoBehaviour
{
    public GridScript gridScript;

    [SerializeField] List<TextMeshProUGUI> DebugTexts = new List<TextMeshProUGUI>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gridScript != null)
        {
            transform.position = gridScript.transform.position;
            for (int i = 0; i < DebugTexts.Count; i++)
            {
                DebugTexts[i].text = gridScript.DebugFloats[i].ToString();
            }
        }
    }
}
