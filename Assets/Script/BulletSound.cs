using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletSound : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private AudioClip[] _audioColpi;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = false;
        _audioSource.playOnAwake = false;
    }

    void Start()
    {
        _audioSource.clip = _audioColpi[Random.Range(0, _audioColpi.Length)];
        _audioSource.Play();
    }
}
