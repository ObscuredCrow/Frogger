using UnityEngine;

public class TurtleFlipper : MonoBehaviour
{
    [SerializeField] private float _diveMinTime = 3f;
    [SerializeField] private float _diveMaxTime = 7f;
    [SerializeField] private float _riseTime = 2f;
    [SerializeField] private Sprite _surfaceTurtle;
    [SerializeField] private Sprite _divingTurtle;

    private SpriteRenderer _sprite;
    private float _diveTime = 5f;

    private void TurtlePrepareRise() {
        _sprite.enabled = true;
        Invoke("TurtleRise", _riseTime);
    }

    private void TurtleRise() {
        _sprite.enabled = true;
        _sprite.sprite = _surfaceTurtle;
        Invoke("TurtlePrepareDive", _diveTime);
    }

    private void TurtlePrepareDive() {
        _sprite.enabled = true;
        _sprite.sprite = _divingTurtle;
        Invoke("TurtleDive", _riseTime);
    }

    private void TurtleDive() {
        _sprite.enabled = false;
        Invoke("TurtlePrepareRise", _riseTime);
    }

    public void ResetTurtle() {
        _diveTime = Random.Range(_diveMinTime, _diveMaxTime);
        TurtleRise(); 
    }

    private void Awake() {
        _sprite = GetComponent<SpriteRenderer>();
        ResetTurtle();
    }

    private void OnTriggerStay2D(Collider2D collider) {
        if (_sprite.enabled || collider.tag != "Player") return;

        collider.GetComponent<FrogController>().ResetFrog(true);
        GameController.Instance.LowerHealth();
    }
}
