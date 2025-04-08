using UnityEngine;

public class RepositionTrigger : MonoBehaviour
{
    [SerializeField] private bool IsLeftTrigger = true;

    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag != "Obstacle") return;
        if (IsLeftTrigger != collider.GetComponent<MoveController>().GoLeft) return;

        collider.GetComponent<MoveController>().ResetRow();
    }
}
