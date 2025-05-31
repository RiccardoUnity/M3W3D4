using System.Collections;
using UnityEngine;

//Permette di simulare uno sfondo infinito spostando la tilemap in modo intelligente, leggeri scatti che non riesco a sistemare
public class InfinityGround : MonoBehaviour
{
    private Transform _player;
    public bool isActive = true;
    private bool _newPositionReady;
    private Vector3 _newPosition;

    void Start()
    {
        _player = FindAnyObjectByType<PlayerController>().transform;
        StartCoroutine(MoveGround());
    }

    IEnumerator MoveGround()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(3f);
            int x = Mathf.RoundToInt(_player.position.x);
            x = (x % 2 == 0) ? x : x - 1;
            int y = Mathf.RoundToInt(_player.position.y);
            y = (y % 2 == 0) ? y : y - 1;
            _newPosition = new Vector3(x, y, 0f);
            _newPositionReady = true;
        }
    }

    void Update()
    {
        if (_newPositionReady)
        {
            _newPositionReady = false;
            transform.position = _newPosition;
        }
    }
}
