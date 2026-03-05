using System;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 5.0f;

    public static event Action<GameObject> targetHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set motion up
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //broadcast a message if collision occurs
        targetHit?.Invoke(collision.gameObject);
    }
}
