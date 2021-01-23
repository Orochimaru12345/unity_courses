using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBelt : MonoBehaviour
{
    [SerializeField] Asteroid asteroid;
    [SerializeField] float targetLength = 100f;
    [SerializeField] int layers = 2;
    [SerializeField] float yOffset = 0f;
    [SerializeField] float distanceBetweenLayers = 0.5f;
    [SerializeField] float minSize = 1f;
    [SerializeField] float maxSize = 4f;
    [SerializeField] float minDistance = 1f;
    [SerializeField] float maxDistance = 3f;
    [SerializeField] float minSpeed = 1f;
    [SerializeField] float maxSpeed = 2f;

    List<Asteroid> greyBalls = new List<Asteroid>();

    void Start()
    {
        SpawnAsteroids();
        StartFlying();
    }

    void SpawnAsteroids()
    {
        float y = transform.position.y;

        for (int i = 0; i < layers; i++)
        {
            float x = -(targetLength / 2) + (minDistance + maxDistance) / 2 + (minSize + maxSize) / 2;
            float lenght = 0f;
            float maxDiameterInLayer = 0f;
            float flySpeed = Random.Range(minSpeed, maxSpeed);

            while (lenght < this.targetLength)
            {
                var diameter = Random.Range(minSize, maxSize);
                var distance = Random.Range(minDistance, maxDistance);
                Asteroid ball = Instantiate(asteroid, new Vector3(x, y, 0f), Quaternion.identity);
                ball.transform.localScale = new Vector3(diameter, diameter, diameter);
                ball.flySpeed = flySpeed;
                ball.resetPoint = -(targetLength / 2);
                greyBalls.Add(ball);
                lenght += diameter + distance;
                x += diameter + distance;
                if (diameter > maxDiameterInLayer)
                {
                    maxDiameterInLayer = diameter;
                }
            }

            y += maxDiameterInLayer + distanceBetweenLayers;
        }
    }

    void StartFlying()
    {
        foreach (Asteroid ball in greyBalls)
        {
            ball.fly = true;
        }
    }
}
