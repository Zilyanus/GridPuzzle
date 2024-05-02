using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCommand : ICommand
{
    Transform ObjectToRotate;
    float RotateValue;

    public static event Action<float> OnGridRotate;

    public RotateCommand(Transform RotatingObject, float rotateValue)
    {
        ObjectToRotate = RotatingObject;
        RotateValue = rotateValue;
    }

    public void Execute()
    {
        Transform OldParent = ObjectToRotate.parent.parent;

        GameObject Pivot = new GameObject();
        Pivot.transform.position = ObjectToRotate.transform.position;

        ObjectToRotate.parent.parent = Pivot.transform;

        Sequence sequence = DOTween.Sequence(this);

        sequence.Append(Pivot.transform.DOScale(1.2f, 0.3f));
        sequence.AppendInterval(0.1f);

        sequence.Append(Pivot.transform.DORotate(Vector3.forward * RotateValue, 0.3f));

        sequence.Join(DOVirtual.DelayedCall(0,()=> OnGridRotate.Invoke(-RotateValue)));

        sequence.AppendInterval(0.1f);
        sequence.Append(Pivot.transform.DOScale(1f, 0.3f));

        sequence.OnComplete(() =>
        {
            ObjectToRotate.parent.parent = OldParent;
            ObjectToRotate.GetComponentInParent<GridParent>().ControlSurround();
            UnityEngine.Object.Destroy(Pivot);
            sequence.Kill();
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

