using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingBox : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float movingDistance;
    Vector2 startPosition;

    void Awake()
    {
        startPosition = transform.position;
    }

    private int direction = 1;
    void Update()
    {
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
        if (transform.position.x > startPosition.x + movingDistance)
            direction = -1;
        else if (transform.position.x < startPosition.x)
            direction = 1;
    }
}
