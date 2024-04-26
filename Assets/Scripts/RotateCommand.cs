using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCommand : ICommand
{
    Transform ObjectToRotate;
    public RotateCommand(Transform RotatingObject)
    {
        ObjectToRotate = RotatingObject;
    }

    public void Execute()
    {
        Transform OldParent = ObjectToRotate.parent;

        GameObject Pivot = new GameObject();
        Pivot.transform.position = ObjectToRotate.transform.position;

        ObjectToRotate.parent = Pivot.transform;

        Pivot.transform.DORotate(Vector3.forward * 90 ,0.3f);
        ObjectToRotate.parent = OldParent;

        Object.Destroy(Pivot);
    }

    public bool Undo()
    {
        return true;
    }

    public void Redo()
    {

    }
}

