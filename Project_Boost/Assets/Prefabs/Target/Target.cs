using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    LandingPad landingPad = null;

    void Start()
    {
        landingPad = FindObjectOfType<LandingPad>();
    }

    void Update()
    {
        this.PointAtLandingPad();
    }

    private void PointAtLandingPad()
    {
        Vector3 landingPadPos = Camera.main.WorldToScreenPoint(landingPad.transform.position);
        this.transform.position = new Vector3(
            Mathf.Clamp(landingPadPos.x, 0, Camera.main.pixelWidth * 0.95f),
            Mathf.Clamp(landingPadPos.y, 0, Camera.main.pixelHeight * 0.95f),
            0
        );
    }
}
