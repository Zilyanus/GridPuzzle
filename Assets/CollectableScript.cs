using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectableScript : MonoBehaviour
{
    public static UnityEvent OnSpawned = new();
    public static UnityEvent OnCollected = new();
    bool isCollected;

    [SerializeField] LayerMask GridMask;

    private void Start()
    {
        OnSpawned.Invoke();

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
        OnCollected.Invoke();
        transform.DOScale(1.5f,0.25f).OnComplete(()=>transform.DOScale(0f, 0.3f).OnComplete(() => Destroy(gameObject)));       
    }
}
