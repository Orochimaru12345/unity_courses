using UnityEngine;

public class TowerStats : MonoBehaviour
{
    [SerializeField] float rangeGrowth = 2f;
    [SerializeField] float rofGrowth = 0.1f;
    [SerializeField] float projectileSpeedGrowth = 4f;
    [SerializeField] public float baseRange = 20f;
    [SerializeField] public float baseRoF = 1f;
    [SerializeField] public float baseProjectileSpeed = 40f;

    Bank bank;

    private void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void AddRange()
    {
        if (bank.CurrentBalance >= 100)
        {
            baseRange += rangeGrowth;
            baseProjectileSpeed += projectileSpeedGrowth;
            bank.Withdraw(100);
            UpdateTowers();
        }
    }

    public void AddRoF()
    {
        if (bank.CurrentBalance >= 100)
        {
            baseRoF += rofGrowth;
            bank.Withdraw(100);
            UpdateTowers();
        }
    }

    public void UpdateTowers()
    {
        foreach (Tower tower in FindObjectsOfType<Tower>())
        {
            TargetLocator towerTl = tower.GetComponent<TargetLocator>();
            towerTl.SetRange(baseRange);

            ParticleSystem towerPs = tower.GetComponentInChildren<ParticleSystem>();
            var main = towerPs.main;
            main.startSpeed = new ParticleSystem.MinMaxCurve(baseProjectileSpeed);

            var emission = towerPs.emission;
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(baseRoF);
        }
    }
}

