using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Rocket rocket;
    [SerializeField] float zOffset = 10f;
    [SerializeField] float xOffset = 10f;
    [SerializeField] float yOffset = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rocket = FindObjectOfType<Rocket>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(rocket.transform.position.x + xOffset, rocket.transform.position.y + yOffset, zOffset);
    }
}
