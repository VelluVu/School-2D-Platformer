using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBonfire : MonoBehaviour {

    float healAmount;
    float distance;
    GameObject player;

    private void Start()
    {
        distance = 15;
        player = GameObject.FindGameObjectWithTag("Player");
        healAmount = 1f * Time.deltaTime;
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position,player.transform.position) < distance)
        player.GetComponent<CharacterControl>().TakeDamage(-healAmount);
    }

}
