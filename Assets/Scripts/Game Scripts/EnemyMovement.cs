using System.Collections;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private float stepsTaken = 0;
    private bool movingRight = false;
    private bool movingDown = true;
    public GameObject enemyBulletPrefab;
    public GameObject bulletFlashPrefab;
    public AudioClip shoot;
    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        //group motion through coroutine
        StartCoroutine(SideStep());
    }

    // Update is called once per frame
    void Update()
    {
        //Randomly send out bullets
        int random = Random.Range(0,20000);
        if(random == 1)
        {
            Instantiate(enemyBulletPrefab, transform.position + new Vector3(0f,-0.25f,0), Quaternion.identity);
            StartCoroutine(BulletFlash());
            audioSource.PlayOneShot(shoot);
        }
    }

    public IEnumerator SideStep()
    {
        while(true){
            //if reached the edge of screen change direction
            if ((stepsTaken % 7) == 0)
            {
                movingRight = !movingRight;
                movingDown = !movingDown;
            }
            //shift down
            if (movingDown == true)
            {
                Vector2 newPosition = _rigidBody.position + Vector2.down/2;
                _rigidBody.MovePosition(newPosition);
                movingRight = !movingRight;
            }
            //shift side
            else
            {
                if (movingRight)
                {
                    Vector2 newPosition = _rigidBody.position + Vector2.right;
                    _rigidBody.MovePosition(newPosition);
                    stepsTaken++;
                }
                else
                {
                    Vector2 newPosition = _rigidBody.position + Vector2.left;
                    _rigidBody.MovePosition(newPosition);
                    stepsTaken++;
                }
            }
            //two second before next move
            yield return new WaitForSeconds(2f);
        }
    }

    private IEnumerator BulletFlash()
    {
        GameObject flash =Instantiate(bulletFlashPrefab, gameObject.transform.position - new Vector3(0f,0.5f,0f), Quaternion.identity);
        yield return new WaitForSecondsRealtime(0.25f);
        Destroy(flash);
    }
}
