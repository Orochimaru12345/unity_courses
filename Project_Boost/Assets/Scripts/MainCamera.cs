using UnityEngine;

public class MainCamera : MonoBehaviour
{    
    [SerializeField] float zOffset = 10f;
    [SerializeField] float xOffset = 10f;
    [SerializeField] float yOffset = 10f;

    Rocket rocket;

    void Start()
    {
        rocket = FindObjectOfType<Rocket>();
    }

    void Update()
    {
        transform.position = new Vector3(rocket.transform.position.x + xOffset, rocket.transform.position.y + yOffset, zOffset);
    }
}
