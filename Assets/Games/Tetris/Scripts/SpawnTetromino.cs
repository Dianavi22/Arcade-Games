using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetromino : MonoBehaviour
{

    public GameObject[] Tetrominoes;
    void Start()
    {
        NewTetromino();
    }

    void Update()
    {
        
    }

    public void NewTetromino()
    {
        Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
    }
}
