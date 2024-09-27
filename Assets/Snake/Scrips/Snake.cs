using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private bool _isMoving = false;
    [SerializeField] GameManager _gameManager;

    [SerializeField] private List<GameObject> _snakeParts;
    public GameObject _snakePartPrefab;

    private bool _cantUp;
    private bool _cantDown;
    private bool _cantLeft;
    private bool _cantRight;

    private void Start()
    {
        _snakeParts = new List<GameObject>();
        _snakeParts.Add(gameObject);
        _snakeParts.Add(_snakePartPrefab);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !_cantUp) {
            _direction = Vector2.up;
            _cantDown = true;
            _cantLeft = false;
            _cantRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.S) && !_cantDown) { _direction = Vector2.down;
            _cantUp = true;
            _cantLeft = false;
            _cantRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.A) && !_cantLeft) { _direction = Vector2.left;
            _cantRight = true;
            _cantDown = false;
            _cantUp = false;
        }
        else if(Input.GetKeyDown(KeyCode.D) && !_cantRight) { _direction = Vector2.right;
            _cantLeft = true;
            _cantDown = false;
            _cantUp = false;
        }

        if (_isMoving == false)
        {
            StartCoroutine(MoveSnake());
        }
    
    }
    private IEnumerator MoveSnake()
    {
        _isMoving = true;
        yield return new WaitForSeconds(0.09f);
        this.transform.position = new Vector3(
           Mathf.Round(this.transform.position.x) + _direction.x,
           Mathf.Round(this.transform.position.y) + _direction.y, 0);

        for (int i = _snakeParts.Count - 1; i > 0 ; i--)
        {
            if (_snakeParts[i].transform.position != _snakeParts[i - 1].transform.position)
            {
            _snakeParts[i].transform.position = _snakeParts[i - 1].transform.position;
            }
        }
        _isMoving = false;

    }

    public void Grow()
    {
        GameObject part = _snakeParts[_snakeParts.Count - 1];
        GameObject _snakePart = Instantiate(
            this._snakePartPrefab,
           part.transform.position,
            Quaternion.identity/* _snakeParts[_snakeParts.Count - 1].rotation*/);
        _snakeParts.Add(_snakePart);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Player"))
        {
            _gameManager.GameOver();
        }
        if (collision.CompareTag("Food"))
        {
            Grow();
        }
    }
}
