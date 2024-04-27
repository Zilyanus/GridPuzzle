using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCommand : ICommand
{
    Transform ObjectToRotate;

    public static event Action<float> OnGridRotate;

    public RotateCommand(Transform RotatingObject)
    {
        ObjectToRotate = RotatingObject;
    }

    public void Execute()
    {
        Transform OldParent = ObjectToRotate.parent.parent;

        GameObject Pivot = new GameObject();
        Pivot.transform.position = ObjectToRotate.transform.position;

        ObjectToRotate.parent.parent = Pivot.transform;

        OnGridRotate.Invoke(90);

        Pivot.transform.DORotate(Vector3.forward * 90, 0.3f).OnComplete(() =>
        {
            ObjectToRotate.parent.parent = OldParent;

            UnityEngine.Object.Destroy(Pivot);
        });
    }

    public bool Undo()
    {
        return true;
    }

    public void Redo()
    {

    }
}

