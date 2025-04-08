using UnityEngine;

public class FrogController : MonoBehaviour
{
    [SerializeField] private float _moveModifier = 0.5f;
    [SerializeField] private float _delayMove = 1f;
    [SerializeField] private Transform _worldParent;
    [SerializeField] private GameObject _death;

    [HideInInspector] public bool CanMove = false;

    private Vector3 _startPosition;
    private AudioSource _audio;
    private Animator _animator;

    private void Update() {
        ResetPosition();
        Move();
    }

    private void ResetPosition() {
        if (GameController.Instance.State != GameState.Start)  return;

        transform.localPosition = _startPosition;
    }

    public void ResetFrog(bool died = false) {
        transform.parent = _worldParent;
        if(died) Instantiate(_death, transform.localPosition, Quaternion.identity);
        transform.localPosition = _startPosition; 
    }

    private void Move() {
        if (GameController.Instance.State != GameState.Run || !CanMove) return;
        if (Input.GetKeyDown(KeyCode.W)) {
            transform.position += transform.up * _moveModifier;
            _animator.SetTrigger("Up");
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            transform.position -= transform.up * _moveModifier;
            _animator.SetTrigger("Down");
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            transform.position += transform.right * _moveModifier;
            _animator.SetTrigger("Right");
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            transform.position -= transform.right * _moveModifier;
            _animator.SetTrigger("Left");
        }

        Invoke("ResetTriggers", 0.1f);
    }

    private void ResetTriggers() {
        _animator.ResetTrigger("Up");
        _animator.ResetTrigger("Down");
        _animator.ResetTrigger("Right");
        _animator.ResetTrigger("Left");
    }

    private void AllowMoving() => CanMove = true;

    public void PerformAllowMoving() => Invoke("AllowMoving", _delayMove);

    private void Awake() { 
        _audio = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _startPosition = transform.localPosition;
    }
}