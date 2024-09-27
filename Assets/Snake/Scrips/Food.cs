using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;
    [SerializeField] private Snake _player;

    private void Start()
    {
        this.RandomizePosition();
    }

    private void RandomizePosition()
    {

        Bounds bounds = this.gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
            if(collider.CompareTag("Player"))
        {
            RandomizePosition();
        }
    }

}
