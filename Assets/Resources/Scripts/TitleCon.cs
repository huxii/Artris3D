using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleCon : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {		
	}
	
	// Update is called once per frame
	void Update () 
    {	
	}

    public void StartLevel() 
    {
        SceneManager.LoadScene("Artris3D");
    }
}
