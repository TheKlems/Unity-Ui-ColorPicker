using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

    public ColorPickerUnityUI ColorPickerToUse;

    private bool isCamera;
    private bool isLight;

	// Use this for initialization
	void Start () {
		if (gameObject.GetComponent<Camera>())
        {
            isCamera = true;
        }
        else
        {
            isCamera = false;
        }
        if (gameObject.GetComponent<Light>())
        {
            isLight = true;
        }else
        {
            isLight = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (ColorPickerToUse)
        {
            if (!isCamera && !isLight && gameObject.GetComponent<Renderer>().material)
            {
                gameObject.GetComponent<Renderer>().material.color = ColorPickerToUse.value;
            }
            if (isLight)
            {
                gameObject.GetComponent<Light>().color = ColorPickerToUse.value;
            }
            
        }else
        {
            Debug.LogWarning("No 'ColorPickerToUse' was found.");
            return;
        }
	}
}
