using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetRandomizer : MonoBehaviour
{
    public static PlanetRandomizer instance;

    [SerializeField] string planetName;
    [SerializeField] PlanetType planetType;
    [SerializeField] string galaxy;
    [SerializeField] SpriteRenderer sky;

    [Header("Picked Colors")]
    public Color lowerSkyColor;
    public Color upperSkyColor;
    public Color groundColor;
    public Color mainLSColor;
    public Color trimLSColor;
    public Color floraColor;
    public Color backgroundColor;

    [Header("Settings")]
    [SerializeField] PlanetType[] possiblePlanetTypes;
    [SerializeField] string[] possibleGalaxies;
    [SerializeField] string[] possiblePlanetNameChunks;
    
    [Header("UI")]
    [SerializeField] Text planetNameText;
    [SerializeField] Text galaxyText;
    [SerializeField] Text planetTypeText;

    void Awake () => instance = this;

    void Start()
    {
        planetName = GeneratePlanetName();
        planetType = GeneratePlanetType();
        galaxy = GenerateGalaxy();

        planetNameText.text = planetName;
        galaxyText.text = galaxy;
        planetTypeText.text = planetType.typeName + " planet";

        SetPlanet();
    }

    string GeneratePlanetName () 
    {
        string planetName = "";
        int chunkAmounts = Random.Range(2, 6);

        for (int i = 0; i < chunkAmounts; i++) 
        {
            string chunk = possiblePlanetNameChunks[Random.Range(0, possiblePlanetNameChunks.Length)];

            if (i == 0) 
                chunk = char.ToUpper(chunk[0]) + chunk.Substring(1);
                
            planetName += chunk;
        }

        return planetName;
    }

    PlanetType GeneratePlanetType () 
    {
        return possiblePlanetTypes[Random.Range(0, possiblePlanetTypes.Length)];
    }

    string GenerateGalaxy () 
    {
        return possibleGalaxies[Random.Range(0, possibleGalaxies.Length)] + "-" + Random.Range(50, 1000) + " galaxy";
    }

    public void SetPlanet () 
    {
        //Color randColor = gradient.Evaluate (Random.Range (0f, 1f));
        lowerSkyColor = planetType.lowerSkyColor.Evaluate(Random.Range(0f, 1f));
        upperSkyColor = planetType.upperSkyColor.Evaluate(Random.Range(0f, 1f));

        groundColor = planetType.schemeColor.Evaluate(Random.Range(0f, 1f));

        mainLSColor = new Color(groundColor.r - 0.025f, groundColor.g - 0.025f, groundColor.b - 0.025f);
        trimLSColor = new Color(groundColor.r - 0.05f, groundColor.g - 0.05f, groundColor.b - 0.05f);
        floraColor = new Color(groundColor.r - 0.05f, groundColor.g - 0.025f, groundColor.b - 0.025f);
        backgroundColor = new Color(groundColor.r * 0.05f, groundColor.g * 0.05f, groundColor.b * 0.05f);

        sky.material.SetColor("_Top_Color", upperSkyColor);
        sky.material.SetColor("_Bottom_Color", lowerSkyColor);
    }
}

[System.Serializable]
public struct PlanetType 
{
    public string typeName;
    
    [Header("Sky")]
    public Gradient lowerSkyColor;
    public Gradient upperSkyColor;

    [Header("Landscape")]
    public Gradient schemeColor;
}