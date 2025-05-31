using UnityEngine;

public class EnemySound : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private AudioClip[] _audioDeSpawn;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.playOnAwake = false;
    }

    public void HitEnemy()
    {
        _audioSource.clip = _audioDeSpawn[Random.Range(0, _audioDeSpawn.Length)];
        _audioSource.Play();
    }
}
