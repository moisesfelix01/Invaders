using System.Collections;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public GameObject squidPrefab;
    public GameObject bugPrefab;
    public GameObject jellyfishPrefab;
    public GameObject ufoPrefab;
    public GameObject barricadePrefab;
    private bool ufoSpawned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //load in enemies
        Vector2 topLeftSpawn = new Vector2(-8.0f,3.5f);
        for(int i = 0; i < 10; i++)
        {
            Instantiate(squidPrefab, topLeftSpawn, Quaternion.identity);
            Instantiate(bugPrefab, topLeftSpawn + Vector2.down, Quaternion.identity);
            Instantiate(jellyfishPrefab, topLeftSpawn + Vector2.down * 2, Quaternion.identity);
            Instantiate(jellyfishPrefab, topLeftSpawn + Vector2.down * 3, Quaternion.identity);

            topLeftSpawn = topLeftSpawn + Vector2.right;
        }

        //load in barricades
        Vector2 bottomLeftSpawn = new Vector2(-7.0f,-2.0f);
        Instantiate(barricadePrefab, bottomLeftSpawn, Quaternion.identity);
        Instantiate(barricadePrefab, bottomLeftSpawn + Vector2.right * 7, Quaternion.identity);
        Instantiate(barricadePrefab, bottomLeftSpawn + Vector2.right * 14, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        //if no ufo has spawned recently
        if (!ufoSpawned){
            // 1/4000 chance per frame to spawn
            int random = Random.Range(0,4000);
            if (random == 1)
            {
                //want to wait two seconds at least before spawning another one
                StartCoroutine(OneUFO());
            }
        }
    }

    public IEnumerator OneUFO()
    {
        ufoSpawned = true;
        Vector2 ufoSpawn = new Vector2(-8.0f,4.5f);
        Instantiate(ufoPrefab, ufoSpawn, Quaternion.identity);
        yield return new WaitForSeconds(2.0f);
        ufoSpawned = false;
    }

    
}
