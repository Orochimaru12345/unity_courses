using UnityEngine;

public class Rocket : MonoBehaviour
{
    //[SerializeField] ParticleSystem mainEngineParticles = null;

    //ParticleSystem.MainModule engineMainModule;
    //ParticleSystem.EmissionModule engineEmissionModule;

    //private void Start()
    //{
    //    engineMainModule = mainEngineParticles.main;
    //    engineEmissionModule = mainEngineParticles.emission;
    //}

    //private void RespondToThrustInput()
    //{
    //    // Thrust 'Space'
    //    if (Input.GetKey(KeyCode.Space))
    //    {
    //        //myRigidbody.AddRelativeForce(Vector3.up * thrustingForce * Time.deltaTime, ForceMode.Acceleration);
    //        SetThrustingParticles();
    //    }
    //    else
    //    {
    //        SetIdleParticles();
    //    }
    //}

    //void SetThrustingParticles()
    //{
    //    engineMainModule.startSpeed = 15f;
    //    engineEmissionModule.rateOverTime = 100;
    //}

    //void SetIdleParticles()
    //{
    //    engineMainModule.startSpeed = 5f;
    //    engineEmissionModule.rateOverTime = 50;
    //}
}
