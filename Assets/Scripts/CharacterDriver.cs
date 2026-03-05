using System;
using System.Collections;
using System.Numerics;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CharacterController : MonoBehaviour
{
    public float speed = 20.0f;
    public float edge = 8.0f;
    public GameObject bulletPrefab;
    private Rigidbody2D _rigidBody;
    private Vector3 bulletOffset = new Vector3(0f, 0.8f, 0f);
    private bool bulletExists = false;
    private GameObject currentBullet;
    private Coroutine bulletUpdateRoutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        PlayerBullet.targetHit += BulletCollided;
    }

    void OnDisable()
    {
        PlayerBullet.targetHit -= BulletCollided;
    }

    void Update()
    {
        //if space is pressed and bullet doesnt exist
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && !bulletExists)
        {
            //create bullet from player position
            currentBullet = Instantiate(bulletPrefab, transform.position + bulletOffset, Quaternion.identity);
            //bullet now exists
            bulletExists = true;
            //start coroutine that bestroys bullet after it leaves the screen (1.7 seconds)
            bulletUpdateRoutine = StartCoroutine(UpdateBulletExists(currentBullet));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Get direction of keyboard pressed
        float direction = 0f;
        
        //Keyboard Input
        if (Keyboard.current.dKey.isPressed)
        {
            direction += 1f;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            direction -= 1f;
        }

        Vector2 velocity = new Vector2(0f,0f);
        //update velocity.x if direction not 0
        //clamp to edges of screen
        if(direction != 0){
            if (direction == -1)
            {
                velocity.x = -1;
                Vector2 newPosition = _rigidBody.position + velocity * speed * Time.fixedDeltaTime;
                newPosition.x = Math.Clamp(newPosition.x, -edge, edge);
                _rigidBody.MovePosition(newPosition);
            }
            if (direction == 1)
            {
                velocity.x = 1;
                Vector2 newPosition = _rigidBody.position + velocity * speed * Time.fixedDeltaTime;
                newPosition.x = Math.Clamp(newPosition.x, -edge, edge);
                _rigidBody.MovePosition(newPosition);
            }      
        }
    }

    //if the bullet collides, subscribed to bullet event
    void BulletCollided(GameObject _object)
    {
        // bullet hit something
        //update flag
        bulletExists = false;

        //Stop Coroutine and set it to null
        if (bulletUpdateRoutine != null)
        {
            StopCoroutine(bulletUpdateRoutine);
            bulletUpdateRoutine = null;
        }

        //destroy bullet if it still exists
        if (currentBullet != null)
        {
            Destroy(currentBullet);
            currentBullet = null;
        }
    }


    private IEnumerator UpdateBulletExists(GameObject bullet)
    {
        //wait 1.7 seconds
        yield return new WaitForSeconds(1.7f);

        //if bullet still exists, hasnt collided
        if (bullet == currentBullet)
        {
            bulletExists = false;
            currentBullet = null;
        }
        //if it still exists, destroy
        if (bullet != null)
        {
            Destroy(bullet);
        }
    }
}
