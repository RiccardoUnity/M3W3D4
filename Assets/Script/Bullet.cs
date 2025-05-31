using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _timeDeSpawn = 5f;

    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _collider;
    private Rigidbody2D _rb2D;
    private EnemySound _enemySound;

    private bool _isHit;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<CircleCollider2D>();
        _rb2D = GetComponent<Rigidbody2D>();
        _enemySound = GetComponentInChildren<EnemySound>();
        Invoke("Check", _timeDeSpawn);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyLife _enemyLife = other.gameObject.GetComponentInParent<EnemyLife>();
        if (_enemyLife != null)
        {
            _enemyLife.HitBullet();
            _spriteRenderer.enabled = false;
            _collider.enabled = false;
            _isHit = true;
            _enemySound.HitEnemy();
            Destroy(gameObject, _timeDeSpawn);
        }
    }

    public void Born(Vector2 force)
    {
        _rb2D.AddForce(force * _speed, ForceMode2D.Impulse);
        float angle = Vector2.Angle(Vector2.right, force);
        angle *= (force.y < 0f) ? -1 : 1;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Check()
    {
        if (!_isHit)
        {
            Destroy(gameObject);
        }
    }
}
