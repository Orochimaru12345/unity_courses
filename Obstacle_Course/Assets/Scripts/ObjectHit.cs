using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    [SerializeField] float restoreColorTimeout = 0.1f;

    MeshRenderer myMeshRenderer;
    Color defaultColor;

    private void Start()
    {
        myMeshRenderer = GetComponent<MeshRenderer>();
        defaultColor = myMeshRenderer.material.color;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (myMeshRenderer && other.gameObject.CompareTag("Player"))
        {
            myMeshRenderer.material.color = Color.grey;
            StartCoroutine(turnColorBack());
        }
    }

    IEnumerator turnColorBack()
    {
        yield return new WaitForSeconds(restoreColorTimeout);
        myMeshRenderer.material.color = defaultColor;
    }
}
