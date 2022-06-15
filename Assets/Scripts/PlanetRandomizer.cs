using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetRandomizer : MonoBehaviour
{
    [SerializeField] string planetName;
    [SerializeField] string planetType;
    [SerializeField] string galaxy;

    [Header("")]
    [SerializeField] string[] possibleGalaxies;
    [SerializeField] string[] possiblePlanetTypes;
    [SerializeField] string[] possiblePlanetNameChunks;
    
    [Header("")]
    [SerializeField] Text planetNameText;
    [SerializeField] Text galaxyText;
    [SerializeField] Text planetTypeText;

    void Start()
    {
        planetName = GeneratePlanetName();
        planetType = GeneratePlanetType();
        galaxy = GenerateGalaxy();

        planetNameText.text = planetName;
        galaxyText.text = galaxy + "-" + Random.Range(50, 1000) + " galaxy";
        planetTypeText.text = planetType + " planet";
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

    string GeneratePlanetType () 
    {
        return possiblePlanetTypes[Random.Range(0, possiblePlanetTypes.Length)];
    }

    string GenerateGalaxy () 
    {
        return possibleGalaxies[Random.Range(0, possibleGalaxies.Length)];
    }
}