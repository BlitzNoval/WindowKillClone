using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    

         void OnCollisionEnter2D(Collision2D collision)
    {
         Debug.Log("Player Collided wt XP");
            Destroy(gameObject);
    }

}

