using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie_Behaviour : MonoBehaviour
{   
    [HideInInspector] public bool canAttackPlayer;

    [Header("Sprite Settings")]
    public Color red,white;
    [SerializeField] SpriteRenderer renderer;

    [Header("Health Settings")]
    [SerializeField] HealthSystem healthSystem;


    void Start() => TurnSystem.singleton.enemie = this.gameObject;


    public void Enemies_Turn() => StartCoroutine(Delay_Before_Action(1f));


    IEnumerator Delay_Before_Action(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        if(canAttackPlayer) Player.singleton.GetComponent<HealthSystem>().TakeDamage();

        else GetComponent<Enemie_Movement>().Start_Enemie_Movement();
    }

    public void EndTurn() => TurnSystem.singleton.Convert_String_To_GameState("PlayerTurn");


    public void SetAsTarget(Color newColor) => renderer.color = newColor;
}
