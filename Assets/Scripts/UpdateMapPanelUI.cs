using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMapPanelUI : MonoBehaviour
{
    [SerializeField] Image water;
    [SerializeField] Image land;
    [SerializeField] Image clouds;
    [SerializeField] Image fog;

    public void UpdatePanel (PlanetRandomizer pr)
    {
        water.color = pr.upperSkyColor;
        land.color = pr.groundColor;
        clouds.color = pr.cloudsColor;
        fog.color = new Color(pr.lowerSkyColor.r, pr.lowerSkyColor.g, pr.lowerSkyColor.b, 0.4f);
    }
}
