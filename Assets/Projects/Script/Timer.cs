using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    private bool _isTimerOn;
    private float _totalTime = 30;
    private float _currentTime;
    private int _displayedTime;

	void Start ()
	{
	    _currentTime = _totalTime;
	    _isTimerOn = false;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    if (_isTimerOn)
	    {
            _currentTime = _currentTime - Time.deltaTime;
            _displayedTime = (int)_currentTime;

	        if (_displayedTime <= 0)
	        {
	            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
	    
	    
	}

    private void OnGUI()
    {
        var startTimerButton = GUI.Button(new Rect(20, 260, 100, 30), "Start Timer");
        
        if (startTimerButton)
        {
            _isTimerOn = true;
            
        }

        if (_isTimerOn)
        {
            GUI.Label(new Rect(200, 0, 100, 100), "Time: " + _displayedTime.ToString());

        }

    }
}
