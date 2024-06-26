using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaterGrid : PuzzleGrid
{
    [SerializeField] LayerMask GridMask;
    [SerializeField] LayerMask BgGridMask;
    Vector2 Dir;
    [SerializeField] float RotateValue = -90;
    [SerializeField] Transform ChildTransform;
    public override void CreateCommand(Vector2 ComingDir)
    {
        Debug.Log("CreateCommand");

        Dir = -ComingDir;

        Command = new RotateCommand(MainObjects[Dir], RotateValue);
    }

    public override bool ControlCommand()
    {
        Debug.Log("ControlCommand " + Dir);
        Transform PivotPoint = MainObjects[Dir];
        List<ISurroundable> Grids = PivotPoint.GetComponentInParent<GridParent>().Grids;

        Debug.Log(Grids.Count + "Count");

        for (int i = 0; i < Grids.Count; i++)
        {
            Transform Grid = Grids[i].GetTransform();

            if (Grid == PivotPoint || !Grid.GetComponent<GridScript>() && !Grid.GetComponent<SubGridScript>())
                continue;

            Vector2 Pos = RotatePoint(Grid.position, PivotPoint.position, RotateValue);

            Debug.Log(Grid.name + " " +Dir + " " + Pos);

            RaycastHit2D hit = Physics2D.BoxCast(Pos, Vector2.one * 0.05f,0,transform.up,0.1f, GridMask);

            RaycastHit2D hit2 = Physics2D.BoxCast(Pos, Vector2.one * 0.05f, 0, transform.up, 0.1f, BgGridMask);

            bool ControlBg = Grid.GetComponent<SubGridScript>() != null ? true : hit2;

            if ((hit && hit.collider.transform.parent != Grid.parent) || !ControlBg)
            {
                GameObject Pos1Object = new GameObject("Pos1Object");
                GameObject Pos2Object = new GameObject("Pos2Object");
                Pos1Object.transform.position = Grid.position;
                Pos2Object.transform.position = Pos;
                Debug.Log(Pos.ToString() + " is Full " + Grid.name + " Cant Rotate");
                return false;
            }
        }
        return true;
    }

    Vector2 RotatePoint(Vector2 point, Vector2 pivot, float angle)
    {
        // Translate the coordinate system
        Vector2 translatedPoint = point - pivot;

        // Rotate the translated point
        float angleRad = angle * Mathf.Deg2Rad;
        float x = Mathf.Cos(angleRad) * translatedPoint.x - Mathf.Sin(angleRad) * translatedPoint.y;
        float y = Mathf.Sin(angleRad) * translatedPoint.x + Mathf.Cos(angleRad) * translatedPoint.y;
        Vector2 rotatedPoint = new Vector2(x, y);

        // Translate back to the original coordinate system
        return rotatedPoint + pivot;
    }

    [Button]
    void TurnClockWise()
    {
        RotateValue = -90;
        ChildTransform.transform.localScale = new Vector2(0.7f, 0.7f);
    }
    [Button]
    void TurnCounterClockWise()
    {
        RotateValue = 90;
        ChildTransform.transform.localScale = new Vector2(-0.7f, 0.7f);
    }
}
