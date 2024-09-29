using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float _previousTime;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 12;
    [SerializeField] bool canTotalyTurn;
    [SerializeField] private bool primary = true;
    [SerializeField] private bool canTurn = true;
    [SerializeField] private bool cLaS;
    [SerializeField] private bool cLaL;

    private static Transform[,] grid = new Transform[width, height];
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove()) transform.position -= new Vector3(-1, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove()) transform.position -= new Vector3(1, 0, 0);

        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && canTurn)
        {
            if (!canTotalyTurn)
            {
                if (primary)
                {
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                    primary = false;
                }
                else if (!primary)
                {

                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                    primary = true;
                }

            }
            if (canTotalyTurn)
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

            }
            if (!ValidMove())
            {
                if (!cLaS && !canTotalyTurn)
                {
                    if (primary)
                    {
                        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                    }
                    else
                    {
                        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                    }

                }

                if (cLaS)
                {
                    if (primary)
                    {
                        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                    }
                    else
                    {
                        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                    }

                }

                if (canTotalyTurn)
                {
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                }

            }
        }

        if (Time.time - _previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime/ 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);

            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                this.enabled = false;
                FindObjectOfType<SpawnTetromino>().NewTetromino();
            }
            _previousTime = Time.time;
        }
    }

    private void AddToGrid()
    {
        foreach(Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform) { 
        int roundedX = Mathf.RoundToInt(children.transform.position.x);
        int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (!canTotalyTurn || cLaL)
            {
                if (roundedX <= 0 || roundedX >= width)
                {
                    return false;
                }
            }

            if (canTotalyTurn && !cLaL)
            {
                if ((roundedX <= 1.25f || roundedX >= 10.75f))
                {
                    return false;
                }
            }

            if (roundedY <= 0f)
            {
                return false;
            }

            if (grid[roundedX,roundedY] != null)
            {
                return false;
            }

        }
        return true;


    }
}
