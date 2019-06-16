using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouderies : MonoBehaviour
{
    void LateUpdate()
    {
        var left = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        var right = Camera.main.ViewportToWorldPoint(Vector3.one).x;
        var top = Camera.main.ViewportToWorldPoint(Vector3.one).y;
        var bottom = Camera.main.ViewportToWorldPoint(Vector3.zero).y;

        float x = transform.position.x, y = transform.position.y;

        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, left + GetComponent<Renderer>().bounds.extents.x, right - GetComponent<Renderer>().bounds.extents.x)
                                        , Mathf.Clamp(transform.position.y, bottom + GetComponent<Renderer>().bounds.extents.y, top - GetComponent<Renderer>().bounds.extents.y),
                                        transform.position.z);


    }

}
