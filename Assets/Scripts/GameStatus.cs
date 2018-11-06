using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;


public class GameStatus : MonoBehaviour {

    public static string currentLevel;
    public static GameStatus status;

    public float experience;
    public float health;
    public float previousHealth;
    public float maxHealth;

    public bool Level1;
    public bool Level2;
    public bool Level3;



	void Awake () {

        Debug.Log(Application.persistentDataPath);

        if (status == null)
        {
            DontDestroyOnLoad(gameObject);
            status = this;
        }
        else if (status != this)
        {
            Destroy(gameObject);
        }

        maxHealth = 100f;
        health = maxHealth;
     

        
    }
    private void Update()
    {
        //Debug.Log("CurrentLevel: " + currentLevel);

        if (Input.GetKey(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenu");
        }

    }

    public void Save()
    {
        
        Debug.Log("Save");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savegame.dat");
        PlayerData data = new PlayerData();
        data.health = health;
        data.maxHealth = maxHealth;
        data.experience = experience;
        data.currentLevel = currentLevel;
        data.Level1 = Level1;
        data.Level2 = Level2;
        data.Level3 = Level3;
        bf.Serialize(file, data);
        file.Close();
        

    }

    public void Load()
    {
        
        if(File.Exists(Application.persistentDataPath + "/savegame.dat"))
        {
            Debug.Log("Load");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savegame.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            health = data.health;
            maxHealth = data.maxHealth;
            experience = data.experience;
            currentLevel = data.currentLevel;
            Level1 = data.Level1;
            Level2 = data.Level2;
            Level3 = data.Level3;

        }

    }
}


[Serializable]
class PlayerData
{

    public float experience;
    public float health;
    public float maxHealth;
    public string currentLevel;
    public bool Level1;
    public bool Level2;
    public bool Level3;

}