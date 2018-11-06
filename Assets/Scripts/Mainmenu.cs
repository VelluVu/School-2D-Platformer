using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour {

    private void OnGUI()
    {

        if (GUI.Button(new Rect(10, 100, 100, 30), "Play Game"))
        {
            SceneManager.LoadScene("Map");
        }
        if (GUI.Button(new Rect(10, 140, 100, 30), "Save Game"))
        {
            GameStatus.status.Save();
        }
        if (GUI.Button(new Rect(10, 180, 100, 30), "Load Game"))
        {
            GameStatus.status.Load();
        }

    }
}
