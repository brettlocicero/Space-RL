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
    [SerializeField] SpriteRenderer[] coloredFlora;
    [SerializeField] SpriteRenderer[] background;
    [SerializeField] ParticleSystemRenderer[] clouds;
    [SerializeField] SpriteRenderer[] fogs;

    PlanetRandomizer pr;

    void Start () 
    {
        pr = PlanetRandomizer.instance;
        Generate();
    }

    void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            pr.SetPlanet();
            Generate();
        }
    }

    void Generate () 
    {
        ChangeColors(grounds, pr.groundColor);
        ChangeColors(mainLS, pr.mainLSColor);
        ChangeColors(trimLS, pr.trimLSColor);
        ChangeColors(flora, pr.floraColor);
        ChangeColors(coloredFlora, new Color(pr.lowerSkyColor.r * 3f, pr.lowerSkyColor.g * 3f, pr.lowerSkyColor.b * 3f));
        ChangeColors(background, pr.backgroundColor);

        foreach (ParticleSystemRenderer cloud in clouds) 
        {
            cloud.material.SetColor("_BaseColor", new Color(pr.upperSkyColor.r * 1.5f, pr.upperSkyColor.g * 1.5f, pr.upperSkyColor.b * 1.5f));
        }

        foreach (SpriteRenderer sr in fogs) 
        {
            float alpha = sr.color.a;
            sr.color = new Color(pr.lowerSkyColor.r, pr.lowerSkyColor.g, pr.lowerSkyColor.b, alpha);
        }
    }

    void ChangeColors (SpriteRenderer[] srs, Color col) 
    {
        foreach (SpriteRenderer sr in srs) 
        {
            sr.color = col;
        }
    }
}
