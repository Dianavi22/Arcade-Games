using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePart : MonoBehaviour
{
    void Start()
    {
        Invoke("AssignPlayerTag", 0.5f);
    }

    private void AssignPlayerTag()
    {
        this.tag = "Player";
    }
    
}
