using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [Range(1f, 5f)] public float speed = 1f;

    public bool _isStart = false;

    float posVal;
    Vector2 startPos;
    float newPos;

    void Start()
    {
        startPos = transform.position;
        posVal = transform.GetChild(0).transform.position.x;
    }

    void Update()
    {
        newPos = Mathf.Repeat(Time.time * speed, posVal);
        transform.position = startPos + Vector2.left * newPos;
    }
}
