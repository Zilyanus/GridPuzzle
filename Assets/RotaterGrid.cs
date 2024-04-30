using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaterGrid : PuzzleGrid
{
    [SerializeField] LayerMask GridMask;
    Vector2 Dir;

    public override void CreateCommand(Vector2 ComingDir)
    {
        Debug.Log("CreateCommand");

        Dir = ComingDir;

        Command = new RotateCommand(MainObject,-90);
    }

    public override bool ControlCommand()
    {
        Debug.Log("ControlCommand");
        Transform PivotPoint = MainObject;
        List<ISurroundable> Grids = PivotPoint.GetComponentInParent<GridParent>().Grids;
        for (int i = 0; i < Grids.Count; i++)
        {
            Transform Grid = Grids[i].GetTransform();

            if (Grid == PivotPoint || !Grid.GetComponent<GridScript>())
                continue;

            Vector2 Pos = RotatePoint(Grid.position, PivotPoint.position, -90);

            Debug.Log(Grid.name + " " +Dir + " " + Pos);
            //Pos += Dir;

            RaycastHit2D hit = Physics2D.BoxCast(Pos, Vector2.one * 0.1f,0,transform.up,0.1f, GridMask);

            if (!hit)
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
}
