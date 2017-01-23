using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artris : MonoBehaviour 
{
    public Vector3 spawnLocation;
    Transform currentArtromino;

	// Use this for initialization
	void Start () 
    {
        spawnLocation = new Vector3(0.1f, 4.1f, -0.1f);
        currentArtromino = SpawnRandomArtromino();
        currentArtromino.parent = transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    Transform SpawnRandomArtromino()
    {
        int rand = Random.Range(1, 8);
        string spawnName = "";
        switch (rand)
        {
            case 1:
                spawnName = "I";
                break;
            case 2:
                spawnName = "J0";
                break;
            case 3:
                spawnName = "J1";
                break;
            case 4:
                spawnName = "O";
                break;
            case 5:
                spawnName = "T";
                break;
            case 6:
                spawnName = "Z0";
                break;
            case 7:
                spawnName = "Z1";
                break;
        }

        GameObject SpawnObject = (GameObject)Instantiate(Resources.Load(spawnName), spawnLocation, new Quaternion(0, 0, 0, 0));
        return  SpawnObject.transform;
    }

    public void MoveForward() 
    {
        currentArtromino.localPosition += new Vector3(0, 0, 1.0f);
    }

    public void MoveBack() 
    {
        currentArtromino.localPosition += new Vector3(0, 0, -1.0f);
    }

    public void MoveLeft() 
    {
        currentArtromino.localPosition += new Vector3(1.0f, 0, 0);
    }

    public void MoveRight()
    {
        currentArtromino.localPosition += new Vector3(-1.0f, 0, 0);
    }

	public void Transform()
	{
		currentArtromino.Rotate(0, 0, 90);
	}
}
