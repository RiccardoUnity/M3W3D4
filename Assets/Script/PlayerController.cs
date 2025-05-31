using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent (typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Vector2 _moveInput;
    private float _moveInputmMagnSqrt;
    [SerializeField] private float speed = 15f;

    //Componenti
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    private Rigidbody2D _rb2D;
    private PlayerShooterController _playerShooterController;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _rb2D = GetComponent<Rigidbody2D>();
        _playerShooterController = GetComponent<PlayerShooterController>();
    }

    private Vector2 Move()
    {
        _moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _moveInputmMagnSqrt = _moveInput.sqrMagnitude;
        if (_moveInputmMagnSqrt > 1)
        {
            _moveInput /= Mathf.Sqrt(_moveInputmMagnSqrt);
        }
        return _moveInput * (speed * Time.fixedDeltaTime);
    }

    void Update()
    {
        _rb2D.MovePosition(_rb2D.position + Move());
    }

    void OnDisable()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        _playerShooterController.enabled = false;
    }
}
