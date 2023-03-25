using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateThings : MonoBehaviour
{
    [Header("Tiles Reference")]
    public Tile tilePrefab;

    [Header("Obstacles Settings")]
    [HideInInspector] public GameObject obstaclePrefab;

    [Header("Player")]
    public GameObject player;

    [Header("Enemie")]
    [HideInInspector] public GameObject enemie;

}
