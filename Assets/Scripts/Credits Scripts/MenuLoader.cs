using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(_LoadGame());

        IEnumerator _LoadGame()
        {
            yield return new WaitForSecondsRealtime(5f);
            //Async to get access 
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync("MainMenu");
            //second exclamation mark used to supress null warning
            while(!loadOperation!.isDone) yield return null;
        }
    }
}
