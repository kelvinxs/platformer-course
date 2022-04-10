using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable Unity.InefficientPropertyAccess

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target, farBackground, middleBackground;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float minHeight = 0;
    [SerializeField] private float maxHeight = 10;

    private Vector3 _lastPosition;

    void Start()
    {
        _lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var height = Mathf.Clamp(target.position.y + offset.y, minHeight, maxHeight);
        transform.position = new Vector3(target.position.x + offset.x, height, transform.position.z);

        var amountToMove = transform.position - _lastPosition;

        if (farBackground != null)
        {
            farBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0);
        }

        if (middleBackground != null)
        {
            middleBackground.position += new Vector3(amountToMove.x * .5f, amountToMove.y * .5f, 0);
        }

        _lastPosition = transform.position;
    }
}