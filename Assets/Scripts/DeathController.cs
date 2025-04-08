using UnityEngine;

public class DeathController : MonoBehaviour
{
    private void Remove() => Destroy(gameObject);

    private void Start() => Invoke("Remove", 3);
}
