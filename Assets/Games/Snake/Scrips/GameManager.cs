using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   [SerializeField] private GameObject _gameOverCanvas;
    
    public void GameOver() 
    {
        Time.timeScale = 0f;
        _gameOverCanvas.SetActive(true);
    }

    
}
