using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyHealth))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 4f)] float speed = 1f;
    [SerializeField] float speedGrowth = 0.05f;

    List<Node> path = new List<Node>();
    PathFinder pathFinder;
    GridManager gridManager;
    Enemy enemy;
    EnemyHealth enemyHealth;

    void OnEnable()
    {
        enemyHealth = GetComponent<EnemyHealth>();

        ReturnToStart();
        RecalculatePath(true);
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathFinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();

        path.Clear();
        path = pathFinder.GetNewPath(coordinates);

        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * Mathf.Clamp(speed + speedGrowth * enemyHealth.deathCount, 0f, 4f);
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }

    private void FinishPath()
    {
        if (enemy != null)
        {
            enemy.StealGold();
        }

        gameObject.SetActive(false);
    }
}
