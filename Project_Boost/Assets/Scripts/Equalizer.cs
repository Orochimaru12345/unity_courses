using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equalizer : MonoBehaviour
{
    [SerializeField] GameObject top, bot, column;
    [SerializeField] float padding;
    [SerializeField] float heightSpread;
    [SerializeField] float interval;
    List<GameObject> topColumns = new List<GameObject>();
    List<GameObject> botColumns = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        EnsureSize();
        EnsurePosition();
        float equalizerHorizontalSize = GetEqualizerHorizontalSize();
        float columnHorizontalSize = GetColumnWidth();

        float x = GetBeginX();

        while ((x - GetBeginX() + GetColumnWidth()) < equalizerHorizontalSize)
        {
            x += padding;
            // spawn top
            GameObject cTop = Instantiate(column, new Vector3(x, GetY(top, x), 0f), Quaternion.identity);
            topColumns.Add(cTop);
            // attach top
            cTop.transform.parent = top.transform;
            // spawn bot
            GameObject cBot = Instantiate(column, new Vector3(x, GetY(bot, x), 0f), Quaternion.identity);
            botColumns.Add(cBot);
            // attach bot
            cBot.transform.parent = bot.transform;
            // next iteration data
            x += columnHorizontalSize;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float yOffset = Mathf.Sin(Time.time / interval);

        foreach (GameObject c in topColumns)
        {
            var x = c.transform.position.x;
            Vector3 position = new Vector3(c.transform.position.x, GetY(top, x) + yOffset, c.transform.position.z);
            c.transform.position = position;
        }
        foreach (GameObject c in botColumns)
        {
            var x = c.transform.position.x;
            Vector3 position = new Vector3(c.transform.position.x, GetY(bot, x) + yOffset + GetColumnHeight(), c.transform.position.z);
            c.transform.position = position;
        }
    }

    float GetEqualizerHorizontalSize()
    {
        return top.transform.localScale.x;
    }

    float GetColumnWidth()
    {
        return column.transform.localScale.x;
    }

    float GetColumnHeight()
    {
        return column.transform.localScale.y;
    }

    float GetBeginX()
    {
        return top.transform.position.x - (GetEqualizerHorizontalSize() / 2) + (GetColumnWidth() / 2);
    }

    float GetBeginY(GameObject owner)
    {
        return owner.transform.position.y;
    }

    float GetY(GameObject owner, float x)
    {
        var middle = owner.transform.position.y - GetColumnHeight() / 2;

        return Mathf.Sin(x) * heightSpread + middle;
    }

    void EnsureSize()
    {
        bot.transform.localScale = top.transform.localScale;
    }

    void EnsurePosition()
    {
        bot.transform.position = new Vector3(top.transform.position.x, bot.transform.position.y, 0f);

        top.transform.position = new Vector3(top.transform.position.x, top.transform.position.y, 0f);
    }
}
