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
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x > startPosition.x + movingDistance ||
            transform.position.x < startPosition.x)
            speed = -speed;
    }
}
