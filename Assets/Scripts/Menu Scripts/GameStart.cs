using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void LoadGame()
    {
        StartCoroutine(_LoadGame());

        IEnumerator _LoadGame()
        {
            //Async to get access 
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("Level1");
            //second exclamation mark used to supress null warning
            while(!loadOperation!.isDone) yield return null;
        }
    }
}
