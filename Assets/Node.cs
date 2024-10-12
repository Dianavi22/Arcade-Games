using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    [SerializeField] private Color _currentColor;
    [SerializeField] private Button _currentButton;
    public int idButton;
    private Color _startColor;
    [SerializeField] SimonGameManager _gameManager;
    void Start()
    {
        _startColor = GetComponent<Image>().color;
    }

    void Update()
    {
        
    }

    public void TouchButton()
    {
        if (!_gameManager.isRounding)
        {
            StartCoroutine(ButtonColor());
            _gameManager._idNodeListPlayer.Add(idButton);
            _gameManager.VerifSimon();

        }
        else { return; }
       
    }

    public IEnumerator ButtonColor()
    {
        _currentButton.image.color = _currentColor;
        yield return new WaitForSeconds(0.5f);
        _currentButton.image.color = _startColor;

    }
}
