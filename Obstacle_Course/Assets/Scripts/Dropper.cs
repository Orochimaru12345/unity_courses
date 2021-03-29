using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] GameObject pointer = null;
    [SerializeField] List<GameObject> drops = null;
    [SerializeField] GameObject player = null;
    [SerializeField] GameObject map = null;
    [SerializeField] int dropCount = 5;

    private float safeZoneBot, safeZoneTop, safeZoneLeft, safeZoneRight;
    private float safeZoneDimensionMultiplier = 1.1f;

    private float mapBot, mapTop, mapLeft, mapRight;
    private float mapDimensionMultiplier = 5;

    void Start()
    {
        DetectSafeZoneSides(showPointers: false);
        DetectMapSides(showPointers: false);
        DropIn();
    }

    private void SetPoints(params Vector3[] pos)
    {
        foreach (Vector3 p in pos)
        {
            Instantiate<GameObject>(pointer, p, Quaternion.identity);
        }
    }

    private void DetectSafeZoneSides(bool showPointers = false)
    {
        Vector3 safeZonePos = player.transform.position;
        float scaleX = player.transform.localScale.x * safeZoneDimensionMultiplier;
        float scaleZ = player.transform.localScale.z * safeZoneDimensionMultiplier;

        safeZoneBot = safeZonePos.z - scaleZ;
        safeZoneTop = safeZonePos.z + scaleZ;
        safeZoneLeft = safeZonePos.x - scaleX;
        safeZoneRight = safeZonePos.x + scaleX;

        if (showPointers)
            SetPoints(new Vector3(0, 0, safeZoneBot), new Vector3(0, 0, safeZoneTop), new Vector3(safeZoneLeft, 0, 0), new Vector3(safeZoneRight, 0, 0));
    }

    private void DetectMapSides(bool showPointers = false)
    {
        Vector3 mapPos = map.transform.position;
        float mapScaleX = map.transform.localScale.x * mapDimensionMultiplier;
        float mapScaleZ = map.transform.localScale.z * mapDimensionMultiplier;

        mapBot = mapPos.z - mapScaleZ;
        mapTop = mapPos.z + mapScaleZ;
        mapLeft = mapPos.x - mapScaleX;
        mapRight = mapPos.x + mapScaleX;

        if (showPointers)
            SetPoints(new Vector3(0, 0, mapBot), new Vector3(0, 0, mapTop), new Vector3(mapLeft, 0, 0), new Vector3(mapRight, 0, 0));
    }

    private bool IsPointInRectangle(Vector3 point, float bot, float top, float left, float right)
    {
        return (point.x > left && point.x < right) && (point.z > bot && point.z < top);
    }

    private bool IsPointNotInRectangle(Vector3 point, float bot, float top, float left, float right)
    {
        bool x = !((point.x > left && point.x < right) && (point.z > bot && point.z < top));
        return x;
    }

    private bool IsPointInMap(Vector3 point)
    {
        return IsPointInRectangle(point, bot: mapBot, top: mapTop, left: mapLeft, right: mapRight);
    }

    private bool IsPointNotInSafeZone(Vector3 point)
    {
        return IsPointNotInRectangle(point, bot: safeZoneBot, top: safeZoneTop, left: safeZoneLeft, right: safeZoneRight);
    }

    private void DropIn()
    {
        int count = 0;

        while (count < dropCount)
        {
            float x = Random.Range(mapLeft, mapRight);
            float z = Random.Range(mapBot, mapTop);
            float height = Random.Range(5f, 10f);
            Vector3 point = new Vector3(x, height, z);

            // Debug.Log(point);

            if (IsPointInMap(point))
            {
                if (IsPointNotInSafeZone(point))
                {
                    Instantiate<GameObject>(drops[count % drops.Count], new Vector3(x, height, z), Quaternion.identity);
                    count++;
                }
            }
        }
    }
}
