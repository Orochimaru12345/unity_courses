using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionVfx = null;
    [SerializeField] GameObject hitVfx = null;
    [SerializeField] int hp = 10;

    ScoreBoard scoreBoard;
    GameObject parent = null;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parent = GameObject.FindGameObjectWithTag("SpawnAtRuntime");
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit(other);

        if (hp <= 0)
        {
            KillYourself();
        }
    }

    void ProcessHit(GameObject other)
    {
        scoreBoard.IncreaseScore(10);

        hp -= 1;

        GameObject vfx = Instantiate(hitVfx, other.transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;
    }

    void KillYourself()
    {
        scoreBoard.IncreaseScore(100);

        GameObject vfx = Instantiate(explosionVfx, transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;

        Destroy(gameObject);
    }
}
