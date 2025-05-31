using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    private static PlayerShooterController _playerShooterController;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _box2D;
    private Rigidbody2D _rb2D;

    private float _deltaMove;

    public Color quick = Color.yellow;
    public Color slow = Color.red;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _box2D = GetComponent<BoxCollider2D>();
        _rb2D = GetComponent<Rigidbody2D>();

        if (_playerShooterController == null)
        {
            _playerShooterController = FindAnyObjectByType<PlayerShooterController>();
        }
        _playerShooterController.AddEnemyInList(this);

        //Posizione iniziale
        transform.position = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)) * new Vector3(Random.Range(15f, 20f), 0f) + _playerShooterController.transform.position;

        //Velocità per frame, scala e colore
        _deltaMove = Random.Range(1f, 3f) * Time.fixedDeltaTime;
        transform.localScale *= 0.05f / _deltaMove;
        _spriteRenderer.color = Color.Lerp(slow, quick, (_deltaMove - Time.fixedDeltaTime) / (Time.fixedDeltaTime * 2f));
    }

    IEnumerator ActiveCollider()
    {
        yield return new WaitForSeconds(0.5f);
        _box2D.enabled = true;
    }

    void Start()
    {
        //Attivo il collider solo dopo tot per evitare problemi di collisioni ad inizio partita
        StartCoroutine(ActiveCollider());
    }

    private void EnemyMovement()
    {
        _rb2D.MovePosition(Vector2.MoveTowards(_rb2D.position, _playerShooterController.transform.position, _deltaMove));
    }

    void FixedUpdate()
    {
        EnemyMovement();
    }

    void OnDestroy()
    {
        _playerShooterController.RemoveEnemyInList(this);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponentInParent<PlayerController>();
        if (player != null)
        {
            player.enabled = false;
        }
    }
}
