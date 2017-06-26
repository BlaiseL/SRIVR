using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class underwater : MonoBehaviour {
    public float waterLevel;
    private bool isUnderwater;
    private Color normalColor;
    private Color underwaterColor;
    // Use this for initialization
    void Start ()
    {
        RenderSettings.fog = true;
        normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        underwaterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
		if((transform.position.y < waterLevel) !=isUnderwater)
            isUnderwater = transform.position.y < waterLevel;
	    if (isUnderwater) SetUnderwater();
	    if (!isUnderwater) SetNormal();
	}

    void SetNormal()
    {
        RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = .002f;
    }

    void SetUnderwater()
    {
        RenderSettings.fogColor = underwaterColor;
        RenderSettings.fogDensity = .06f;
    }
}
