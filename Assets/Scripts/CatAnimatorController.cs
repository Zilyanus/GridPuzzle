using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimatorController : MonoBehaviour
{
    float TimeStart;
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMoving()
    {
        TimeStart = Time.time;
        playerMovement.ConfirmMovement();
    }

    public void EndMoving()
    {
        Debug.Log(Time.time - TimeStart);
    }
}
