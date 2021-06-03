using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] float range = 15f;
    Transform target;
    TowerStats towerStats;

    private void Start()
    {
        towerStats = FindObjectOfType<TowerStats>();

        range = towerStats.baseRange;

        ParticleSystem towerPs = GetComponentInChildren<ParticleSystem>();

        var main = towerPs.main;
        main.startSpeed = new ParticleSystem.MinMaxCurve(towerStats.baseProjectileSpeed);

        var emission = towerPs.emission;
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(towerStats.baseRoF);
    }

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);

        weapon.LookAt(target);

        if (targetDistance < range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }

    public void SetRange(float range)
    {
        this.range = range;
    }
}
