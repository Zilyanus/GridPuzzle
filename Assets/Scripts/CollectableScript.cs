using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class CollectableScript : MonoBehaviour
{
    public static event Action OnSpawned;
    public static event Action OnCollected;
    bool isCollected;

    [SerializeField] LayerMask GridMask;

    [SerializeField] Transform Effect;

    private void Start()
    {
        OnSpawned?.Invoke();

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one, 0, Vector2.down, 0.1f, GridMask);

        if (hit)
        {
            GridParent gridParent = null;
            if (hit.transform.parent != null)
            {
                gridParent = hit.transform.GetComponentInParent<GridParent>();
            }
            else
            {
                gridParent = hit.transform.GetComponent<GridScript>().TransformToGridParent();
            }

            transform.parent = gridParent.transform;
        }

        Effect.DOScale(1.05f, 0.5f).OnComplete(() => Effect.DOScale(1f, 0.5f)).SetLoops(-1,LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && !isCollected)
        {          
            Collected();
        }
    }

    public void Collected()
    {
        isCollected = true;
        OnCollected?.Invoke();
        transform.DOScale(1.5f,0.25f).OnComplete(()=>transform.DOScale(0f, 0.3f).OnComplete(() => Destroy(gameObject)));       
    }
}
