using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float flySpeed = 1f;
    public bool fly = false;
    public float resetPoint = -1000f;

    void Update()
    {
        if (fly)
        {
            transform.Translate(Vector3.left * Time.deltaTime * flySpeed);
            if (transform.position.x < resetPoint)
            {
                transform.position = new Vector3(-resetPoint, transform.position.y, transform.position.z);
            }
        }
    }
}
