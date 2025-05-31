using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyLife : MonoBehaviour
{
    private float _life;
    [SerializeField] private float _danno = 0.75f;   //Servono 3 proiettili

    private static UIScript _uiScript;
    private static uint _id = 0;

    void Awake()
    {
        if (_uiScript == null)
        {
            _uiScript = FindAnyObjectByType<UIScript>();
        }

        gameObject.name = "Enemy_" + _id.ToString();
        _id++;
    }

    void Start()
    {
        _life = transform.localScale.x;
    }

    public void HitBullet()
    {
        _life = Mathf.Max(0, _life - _danno);
        _uiScript.SetEnemy(gameObject.name, _life);
        if (_life == 0)
        {
            Destroy(gameObject);
            _uiScript.EnemyDestroy(transform.localScale.x);
        }
    }
}
