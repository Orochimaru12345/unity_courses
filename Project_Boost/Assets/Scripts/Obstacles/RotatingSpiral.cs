using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSpiral : MonoBehaviour
{
    [SerializeField] float angle = 2f;

    void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * angle);
    }
}
