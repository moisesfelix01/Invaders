using UnityEngine;

public class UFOMovement : MonoBehaviour
{
    public float speed = 3.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set motion to the right
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * speed;
    }
    void Update()
    {
        //Once ufo leaves screen, destroy
        if (transform.position.x >= 9)
        {
            Destroy(gameObject);
        }
    }
}
