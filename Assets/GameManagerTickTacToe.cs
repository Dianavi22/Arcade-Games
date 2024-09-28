using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class GameManagerTicTacToe : MonoBehaviour
{
    private bool _playerOneTurn = true;
    private Button _currentButton;
    [SerializeField] TMP_Text _currentText;
    List<int> grid = new List<int>() { 0,0,0,0,0,0,0,0,0};
    public TMP_Text _winText;
    [SerializeField] GameObject _gameOver;
    public void PlayerTurn(Button clickedButton)
    {
         _currentButton = clickedButton;

        if (_playerOneTurn && _currentButton.GetComponentInChildren<TMP_Text>().text == "")
        {
             _currentButton.GetComponentInChildren<TMP_Text>().text = "X";
            grid[Int32.Parse(_currentButton.name)] = 1;
            Verif();
            _playerOneTurn = false;
        }
        else if(!_playerOneTurn && _currentButton.GetComponentInChildren<TMP_Text>().text == "")
        {
            _currentButton.GetComponentInChildren<TMP_Text>().text = "O";
            grid[Int32.Parse(_currentButton.name)] = 2;
            Verif();
            _playerOneTurn = true;
        }

    }

    private void Verif()
    {
        if (grid[0] == 1 && grid[1] == 1 && grid[2] == 1 ||
            grid[3] == 1 && grid[4] == 1 && grid[5] == 1 ||
            grid[6] == 1 && grid[7] == 1 && grid[8] == 1 ||

            grid[0] == 1 && grid[3] == 1 && grid[6] == 1 ||
            grid[1] == 1 && grid[4] == 1 && grid[7] == 1 ||
            grid[2] == 1 && grid[5] == 1 && grid[8] == 1 ||

            grid[0] == 1 && grid[4] == 1 && grid[8] == 1 ||
            grid[6] == 1 && grid[4] == 1 && grid[2] == 1 ||


            grid[0] == 2 && grid[1] == 2 && grid[2] == 2 ||
            grid[3] == 2 && grid[4] == 2 && grid[5] == 2 ||
            grid[6] == 2 && grid[7] == 2 && grid[8] == 2 ||

            grid[0] == 2 && grid[3] == 2 && grid[6] == 2 ||
            grid[1] == 2 && grid[4] == 2 && grid[7] == 2 ||
            grid[2] == 2 && grid[5] == 2 && grid[8] == 2 ||

            grid[0] == 2 && grid[4] == 2 && grid[8] == 2 ||
            grid[6] == 2 && grid[4] == 2 && grid[2] == 2)


        {
            _gameOver.SetActive(true);
            if (_playerOneTurn)
            {
                _winText.text = "Victory Player 1";
            }
            else
            {
                _winText.text = "Victory Player 2";
            }

        }

    }
}
