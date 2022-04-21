using System;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject prefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var transform1 = transform;
            var son = Instantiate(prefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            // son.transform.parent = this.transform;
            // son.transform.Translate(0,0,0);
        }
    }
}