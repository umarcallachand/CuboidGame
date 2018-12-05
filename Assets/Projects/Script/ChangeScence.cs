using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScence : MonoBehaviour
{
    private GameObject _player;
    private SoundManager _audioManager;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TopBottomCollider"))
        {
            _audioManager = _audioManager = FindObjectOfType<SoundManager>();
            StartCoroutine(ChangeLevelAnimation());
        }

    }

    private IEnumerator ChangeLevelAnimation()
    {
        _player.GetComponent<Rigidbody>().useGravity = false;
        _player.GetComponent<BoxCollider>().enabled = false;
        _player.GetComponent<PlayerRoll>().HasWon = true;
        foreach (var p in _player.GetComponentsInChildren<BoxCollider>())
        {
            p.enabled = false;
        }
        _audioManager.Play("Win");

        _player.GetComponentInChildren<Animator>().enabled = true;

        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        GameObject.Find("cuboid2").GetComponent<BoxCollider>().isTrigger = true;
        _audioManager.Play("Win");

        yield return new WaitForSeconds(0.65f);
        ChangeScene();
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartGame()
    {
        ChangeScene();
    }
}
