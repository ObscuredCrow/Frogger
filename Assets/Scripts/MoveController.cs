using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float _destinationX = -18;
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _delayed = 1;

    private Vector3 _startPosition;
    private float _timer = 0;

    [HideInInspector] public bool GoLeft = true;

    public void FixedUpdate() {
        ResetPosition();
        Move();
    }

    private void ResetPosition() {
        if (GameController.Instance.State != GameState.Start) return;

        transform.localPosition = _startPosition;
        _timer = 0;
    }

    public void ResetRow() => transform.localPosition = _startPosition;

    private void Move() {
        if (GameController.Instance.State != GameState.Run) return;

        _timer += Time.deltaTime;
        if (_timer < _delayed) return;

        GoLeft = _destinationX < 0;
        var destination = new Vector3(_destinationX, transform.localPosition.y, 0);
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, _speed * Time.deltaTime);
    }

    private void Awake() => _startPosition = transform.localPosition;
}
