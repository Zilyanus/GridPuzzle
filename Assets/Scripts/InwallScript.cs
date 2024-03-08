using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InwallScript : MonoBehaviour
{
    [OnValueChanged("UpdateWalls")]
    public InWallBools InWalls;

    [Serializable]
    public struct InWallBools
    {
        public bool Up, Down,Right,Left;
    }

    [SerializeField] List<GameObject> Walls;

    // Start is called before the first frame update
    void Start()
    {

    }

    void UpdateWalls()
    {
        for (int i = 0; i < Walls.Count; i++)
        {
            switch (i)
            {
                case 0:
                    Walls[i].SetActive(InWalls.Up);
                    break;
                case 1:
                    Walls[i].SetActive(InWalls.Down);
                    break;
                case 2:
                    Walls[i].SetActive(InWalls.Right);
                    break;
                case 3:
                    Walls[i].SetActive(InWalls.Left);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
