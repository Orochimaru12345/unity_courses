using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timeout = 3f;

    void Start()
    {
        Destroy(gameObject, timeout);
    }
}
