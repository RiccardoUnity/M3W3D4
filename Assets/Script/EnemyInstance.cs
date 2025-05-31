using UnityEngine;

//Se il player è vivo instanzia un nemico ogni tot secondi e lo imparenta a quest'oggetto
public class EnemyInstance : MonoBehaviour
{
    public int startNumberEnemy = 20;
    public float timeRespawn = 1f;
    public Enemy enemyPrefab;

    private GameObject _player;

    private void InstantiateEnemy()
    {
        if (_player.activeSelf)
        {
            Instantiate(enemyPrefab, transform);
        }
    }

    void Start()
    {
        _player = FindAnyObjectByType<PlayerController>().gameObject;

        for (int i = 0; i < startNumberEnemy; i++)
        {
            InstantiateEnemy();
        }

        InvokeRepeating("InstantiateEnemy", 0f, timeRespawn);
    }
}
