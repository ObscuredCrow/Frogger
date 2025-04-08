using UnityEngine;

public class FrogMover : MonoBehaviour
{
    private Transform _frog;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag != "Player") return;

        _frog.parent = transform;
    }

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag != "Player") return;

        _frog.parent = transform.parent.parent.parent;
    }

    private void Awake() => _frog = FindFirstObjectByType<FrogController>().transform;
}
