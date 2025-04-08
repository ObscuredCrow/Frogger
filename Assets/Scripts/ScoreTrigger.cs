using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    public GameObject Goal;
    public GameObject Bug;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag != "Player") return;

        if (Goal.activeSelf) GameController.Instance.LowerHealth();
        else {
            if (Bug.activeSelf) { 
                GameController.Instance.AddToScore(1); 
                Bug.SetActive(false);
            }

            GameController.Instance.AddToScore(1);
            GameController.Instance.Goals++;
            Goal.SetActive(true);
        }

        collider.GetComponent<FrogController>().ResetFrog();
    }
}
