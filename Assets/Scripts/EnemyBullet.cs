using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        //Set bullet on downward motion
        transform.position = transform.position + new Vector3(0f,-1*Time.deltaTime*speed,0f);
        if(transform.position.y < -5)
        {
            //Destroy once off screen
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Debug.Log("GAME OVER!");
        }
        if (collision.gameObject.CompareTag("Barricade"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
