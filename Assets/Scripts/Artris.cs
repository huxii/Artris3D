using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artris : MonoBehaviour 
{

    public Transform currentArtromino;
    public Vector3 spawnLocation;

	// Use this for initialization
	void Start () 
    {
        spawnLocation = new Vector3(0, 0, 5);
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

        return Instantiate(Resources.Load(spawnName), spawnLocation, new Quaternion(0, 0, 0, 0)) as Transform;
    }

    public void MoveForward() 
    {
    }

    public void MoveBack() 
    {
    }

    public void MoveLeft() 
    {
    }

    public void MoveRight()
    {
    }
}
