using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artris : MonoBehaviour 
{
    private Transform currentArtromino;
    private int gridWidth = 10;
    private int gridHeight = 20;

	// Use this for initialization
	void Start () 
    {
        SpawnRandomArtromino();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Landed())
        {
            SpawnRandomArtromino();
        }
	}

    public void SpawnRandomArtromino()
    {
        int rand = Random.Range(1, 8);
        string spawnName = "";
        Vector3 spawnLocation = new Vector3(0.0f, 20.5f, -0.5f);
        switch (rand)
        {
            case 1:
                spawnName = "Prefabs/I";
                spawnLocation = new Vector3(0.0f, 20.5f, -0.5f);
                break;
            case 2:
                spawnName = "Prefabs/J0";
                spawnLocation = new Vector3(0.5f, 20.5f, -0.5f);
                break;
            case 3:
                spawnName = "Prefabs/J1";
                spawnLocation = new Vector3(-0.5f, 20.5f, -0.5f);
                break;
            case 4:
                spawnName = "Prefabs/O";
                spawnLocation = new Vector3(0.0f, 20.5f, -0.5f);
                break;
            case 5:
                spawnName = "Prefabs/T";
                spawnLocation = new Vector3(0.5f, 20.5f, -0.5f);
                break;
            case 6:
                spawnName = "Prefabs/Z0";
                spawnLocation = new Vector3(0.5f, 20.5f, -0.5f);
                break;
            case 7:
                spawnName = "Prefabs/Z1";
                spawnLocation = new Vector3(-0.5f, 20.5f, -0.5f);
                break;
        }
                
        GameObject SpawnObject = (GameObject)Instantiate(Resources.Load(spawnName), spawnLocation, new Quaternion(0, 0, 0, 0));
        currentArtromino = SpawnObject.transform;
        currentArtromino.parent = transform;
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

	public void VerticalTransform()
	{
		currentArtromino.Rotate(0, 0, 90);
	}

    public void HorizontalTransform()
    {
        currentArtromino.Rotate(0, 90, 0);
    }
        
    bool Landed()
    {
        return false;
    }

    bool Collided()
    {
        return false;
    }
}
