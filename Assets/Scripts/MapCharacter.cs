using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCharacter : MonoBehaviour {

    float speed;
    /// <summary>
    /// tee vähintään 3 level ja implementoi ne karttasceneen.
    /// luo firebase prefab joka instanssoidaan näppäimestä "f" kasvattaa healthiä
    /// Kun pelaaja painaa fire 1 heittää kirveksen kaarella jos osuu niin jää kiinni
    /// tuli partickeli
    ///
    /// kirveeseen luodaan collider kirveiden päälle voi hyppiä
    /// kirveiden pitää pyöriä ilmassa
    /// estä pelaajaa hyppäämästä ilmassa
    /// </summary>

    private void Start()
    {

        //On != null vain silloin kun tullaan jostain ruudusta takaisin karttaan
        if (GameStatus.currentLevel != null)
        {
            GameObject.Find(GameStatus.currentLevel).GetComponent<LoadLevel>().Cleared(true);
            transform.position = GameObject.Find(GameStatus.currentLevel).transform.position;

        }
        speed = 1f;

    }

    private void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 
            Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelTrigger") && collision.GetComponent<LoadLevel>().cleared == false)
        {
            if (collision.gameObject.name != GameStatus.currentLevel)
            {
                GameStatus.currentLevel = collision.gameObject.name;
                SceneManager.LoadScene(collision.gameObject.GetComponent<LoadLevel>().levelToLoad);
            }
        }
    }
}

