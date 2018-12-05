using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour {


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player") || other.name == "cuboidFall(Clone)" )
    //    {
    //        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //    }
    //}

    public void Restart()
    {
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
