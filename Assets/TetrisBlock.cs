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
    [SerializeField] GameObject _rotatePoint;
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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(_rotatePoint.transform.TransformPoint(rotationPoint), new Vector3(_rotatePoint.transform.rotation.x, _rotatePoint.transform.rotation.y, 1), 90);
            if(!ValidMove())
            {
                transform.RotateAround(_rotatePoint.transform.TransformPoint(rotationPoint), new Vector3(_rotatePoint.transform.rotation.x, _rotatePoint.transform.rotation.y, 1), -90);

            }
        }

        if (Time.time - _previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime/ 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);

            _previousTime = Time.time;
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform) { 
        int roundedX = Mathf.RoundToInt(children.transform.position.x);
        int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if(roundedX < 0 || roundedX >= width || roundedY >= height)
            {
                return false;
            }

        }
        return true;


    }
}
