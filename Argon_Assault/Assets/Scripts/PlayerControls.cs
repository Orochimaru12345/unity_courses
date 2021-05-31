using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("SubObjects")]
    [SerializeField] GameObject[] lasers = null;
    [Header("Properties")]
    [SerializeField] float controlSpeed = 50f;
    [SerializeField] float xMin = -1f;
    [SerializeField] float xMax = 1f;
    [SerializeField] float yMin = -1f;
    [SerializeField] float yMax = 1f;
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlYawFactor = 5f;
    [SerializeField] float controlRollFactor = -10f;

    float xThrow, yThrow;

    void Start()
    {

    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");

        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float newXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(newXPos, xMin, xMax);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float newYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(newYPos, yMin, yMax);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yawDueToControlThrow = xThrow * controlYawFactor;
        float yaw = yawDueToPosition + yawDueToControlThrow;

        float roll = controlRollFactor * xThrow;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool enabled)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = enabled;
        }
    }

    public void x()
    {

    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
