using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag != "Player") return;

        collider.GetComponent<FrogController>().ResetFrog(true);
        GameController.Instance.LowerHealth();
    }
}
