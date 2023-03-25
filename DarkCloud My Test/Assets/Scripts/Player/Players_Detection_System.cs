using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players_Detection_System : MonoBehaviour
{
    
 private List<GameObject> colisores = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tile"))
        {
            colisores.Add(other.gameObject);
            other.GetComponent<Tile>().AppearPossiblePath(true);  
        }
        if (other.CompareTag("Enemie"))
        {
            colisores.Add(other.gameObject);
            other.GetComponent<HealthSystem>().canBeSelected = true;
            other.GetComponent<Enemie_Behaviour>().SetAsTarget(other.GetComponent<Enemie_Behaviour>().red);

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (colisores.Contains(other.gameObject) && other.CompareTag("Tile"))
        {
            other.GetComponent<Tile>().AppearPossiblePath(false);
        }

        if (other.CompareTag("Enemie"))
        {
            other.GetComponent<HealthSystem>().canBeSelected = false;
            other.GetComponent<Enemie_Behaviour>().SetAsTarget(other.GetComponent<Enemie_Behaviour>().white);

        }

        colisores.Remove(other.gameObject);
    }

    
}
