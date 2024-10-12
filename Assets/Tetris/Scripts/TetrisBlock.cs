using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float _previousTime;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;
    [SerializeField] bool canTotalyTurn;
    [SerializeField] private bool primary = true;
    [SerializeField] private bool canTurn = true;
    [SerializeField] private bool cLaS;
    [SerializeField] private bool cLaL;

    private static Transform[,] grid = new Transform[width, height];

    private void Update()
    {
        HandleInput();
        HandleFall();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveBlock(new Vector3(-1, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveBlock(new Vector3(1, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && canTurn)
        {
            RotateBlock();
        }
    }

    private void MoveBlock(Vector3 direction)
    {
        transform.position += direction;
        if (!ValidMove())
        {
            transform.position -= direction;
        }
        else
        {
            SnapToGrid();
        }
    }

    private void RotateBlock()
    {
        if (!canTotalyTurn)
        {
            if (primary)
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                primary = false;
            }
            else
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                primary = true;
            }
        }
        else
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        }

        if (!ValidMove())
        {
            UndoRotation();
        }
        else
        {
            SnapToGrid();
        }
    }

    private void UndoRotation()
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
        else if (cLaS)
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
        else if (canTotalyTurn)
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }
    }

    private void HandleFall()
    {
        if (Time.time - _previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                FindObjectOfType<SpawnTetromino>().NewTetromino();
            }
            _previousTime = Time.time;
        }
    }

    private void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
                i++;
            }
        }
    }

    private bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
            {
                return false;
            }
        }
        return true;
    }

    private void DeleteLine(int i)
    {
         for (int j = 0; j < width; j++)
            {
                if (grid[j, i] != null)
                {
                grid[j,i].gameObject.SetActive(false);
                    Destroy(grid[j, i].gameObject);
                    grid[j, i] = null;
                }
            }
        
    }

    void RowDown(int i)
    {
       
            for (int y = i + 1; y < height; y++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (grid[j, y] != null)
                    {
                        if (y - 1 >= 0)
                        {
                            grid[j, y - 1] = grid[j, y];
                            grid[j, y] = null;
                            grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                        }
                    }
                }
            }
        
    }

    private void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX >= 0 && roundedX < width && roundedY >= 0 && roundedY < height)
            {
                grid[roundedX, roundedY] = children;
            }
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width)
            {
                return false;
            }

            if (roundedY < 0)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null && grid[roundedX, roundedY].parent != transform)
            {
                return false;
            }
        }
        return true;
    }

    private void SnapToGrid()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);
        transform.position = pos;
    }
}
