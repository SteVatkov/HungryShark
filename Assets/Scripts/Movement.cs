using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector3 offset;

    private float ZCoord;

    private void OnMouseDown()
    {
        ZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        offset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = ZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }
}
