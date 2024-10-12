using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SimonGameManager : MonoBehaviour
{
    [SerializeField] List<int> _idNodeList = new List<int>();
    public List<int> _idNodeListPlayer = new List<int>();
    public bool isNewRound = false;
    [SerializeField] List<GameObject> _buttonsList = new List<GameObject>();
    public bool isRounding;
    void Start()
    {
        isNewRound = true;
    }

    void Update()
    {

        if (isNewRound)
        {
            NewRound();
        }
    }

    private void NewRound()
    {
        isRounding = true;
        isNewRound = false ;
        _idNodeList.Add(Random.Range(0, 6));
        StartCoroutine(ShowChain());
    }

    private IEnumerator ShowChain()
    {
        for (int i = 0; i < _idNodeList.Count; i++)
        {
            StartCoroutine(_buttonsList[_idNodeList[i]].GetComponent<Node>().ButtonColor());
            yield return new WaitForSeconds(0.6f);
        }
        isRounding = false;
    }

    public void VerifSimon()
    {
        for (int i = 0; i < _idNodeList.Count; i++)
        {
            if (_idNodeList[i] == _idNodeListPlayer[i])
            {

            }
        }
    }
}
