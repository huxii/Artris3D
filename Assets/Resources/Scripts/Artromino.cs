using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artromino : MonoBehaviour {

    private float deltaTime = 1.0f;
    private float timeKey = 0.0f;

	// Use this for initialization
	void Start () {
        timeKey = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Falling();
	}

    void Falling()
    {
        if (Time.time - timeKey >= deltaTime)
        {
            timeKey = Time.time;
            transform.localPosition += new Vector3(0.0f, -1.0f, 0.0f);
        }        
    }
}
