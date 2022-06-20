using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform playerSpawnPos;

    [Header("")]
    [SerializeField] SpriteRenderer[] grounds;
    [SerializeField] SpriteRenderer[] mainLS;
    [SerializeField] SpriteRenderer[] trimLS;
    [SerializeField] SpriteRenderer[] flora;
    [SerializeField] SpriteRenderer[] background;

    void Start () 
    {
        ChangeColors(grounds, PlanetRandomizer.instance.groundColor);
        ChangeColors(mainLS, PlanetRandomizer.instance.mainLSColor);
        ChangeColors(trimLS, PlanetRandomizer.instance.trimLSColor);
        ChangeColors(flora, PlanetRandomizer.instance.floraColor);
        ChangeColors(background, PlanetRandomizer.instance.backgroundColor);
    }

    void ChangeColors (SpriteRenderer[] srs, Color col) 
    {
        foreach (SpriteRenderer sr in srs) 
        {
            sr.color = col;
        }
    }
}
