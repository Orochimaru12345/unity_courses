using System;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color blockedColor = Color.yellow;
    [SerializeField] Color exploredColor = Color.blue;
    [SerializeField] Color pathColor = Color.magenta;

    TextMesh label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
    bool labelEnabled = false;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();

        label = GetComponent<TextMesh>();

        GetCoordinates();
        DisplayCoordinates();
        UpdateObjectname();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            GetCoordinates();
            DisplayCoordinates();
            UpdateObjectname();
        }

        ToggleLabels();
        SetLabelColor();
    }

    void DisplayCoordinates()
    {
        if (labelEnabled)
        {
            label.text = $"{coordinates.x}, {coordinates.y}";
        }
        else
        {
            label.text = "";
        }

    }

    void UpdateObjectname()
    {
        // transform.parent.name = coordinates.ToString();
    }

    void GetCoordinates()
    {
        if (gridManager == null)
        {
            return;
        }

        coordinates.x = (Int32)(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = (Int32)(transform.parent.position.z / gridManager.UnityGridSize);

    }

    void SetLabelColor()
    {
        if (gridManager == null)
        {
            return;
        }

        Node node = gridManager.GetNode(coordinates);

        if (node == null)
        {
            return;
        }

        if (node.isWalkable == false)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            labelEnabled = !labelEnabled;
            DisplayCoordinates();
        }
    }
}

