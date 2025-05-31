using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIScript : MonoBehaviour
{
    private VisualElement _root;
    private Label _enemyNameLabel;
    private Label _enemyLifeLabel;
    private Label _myPointLabel;

    private float _myPoint;

    void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _enemyNameLabel = _root.Q<Label>("NameEnemy");
        _enemyLifeLabel = _root.Q<Label>("LifeEnemy");
        _myPointLabel = _root.Q<Label>("MyPoint");

        _enemyNameLabel.text = "-";
        _enemyLifeLabel.text = "-";
        _myPointLabel.text = "0";
    }

    public void SetEnemy(string name, float life)
    {
        _enemyNameLabel.text = name;
        _enemyLifeLabel.text = life.ToString("F1");
    }

    public void EnemyDestroy(float point)
    {
        _myPoint += point;
        _myPointLabel.text = _myPoint.ToString("F1");
    }
}
