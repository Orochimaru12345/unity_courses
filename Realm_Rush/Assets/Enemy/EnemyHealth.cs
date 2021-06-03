using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHitPoints = 5.0f;
    [SerializeField] float hpGrowth = 1.0f;

    public int deathCount = 0;
    float currentHitPoints = 0;
    Enemy enemy;

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    void ProcessHit()
    {
        currentHitPoints -= 1;

        if (currentHitPoints <= 0)
        {
            gameObject.SetActive(false);

            maxHitPoints += hpGrowth;
            deathCount++;

            enemy.RewardGold();
        }
    }
}
