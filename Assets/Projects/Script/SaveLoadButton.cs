using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadButton : MonoBehaviour {
    private void OnGUI()
    {
        if (GUI.Button(new Rect(20,140, 100, 30), "Save"))
        {
            SaveGame.saveGame.Save();
        }

        if (GUI.Button(new Rect(20, 200, 100, 30), "Load"))
        {
            SaveGame.saveGame.Load();
        }

        if (GUI.Button(new Rect(20, 320, 100, 30), "Exit"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
