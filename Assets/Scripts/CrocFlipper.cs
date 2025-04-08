using UnityEngine;

public class CrocFlipper : MonoBehaviour
{
    [SerializeField] private GameObject Log;
    [SerializeField] private GameObject Croc;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag != "Player") return;

        Log.SetActive(false);
        Croc.SetActive(true);
    }

    public void ResetCroc() {
        Log.SetActive(true);
        Croc.SetActive(false);
    }
}
