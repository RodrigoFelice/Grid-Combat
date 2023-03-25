using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemies_Detection : MonoBehaviour
{
   public List<GameObject> colisores = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tile"))
        {
            colisores.Add(other.gameObject);
        }
        if (other.CompareTag("Player"))
        {
            colisores.Add(other.gameObject);

            GetComponentInParent<Enemie_Behaviour>().canAttackPlayer = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (colisores.Contains(other.gameObject))
        {
            colisores.Remove(other.gameObject);
        }
        
         if (other.CompareTag("Player"))
        {
            colisores.Remove(other.gameObject);

            GetComponentInParent<Enemie_Behaviour>().canAttackPlayer = false;
        }
    }
}
