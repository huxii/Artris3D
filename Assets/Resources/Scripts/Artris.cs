using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artris : MonoBehaviour 
{
    private Artromino currentArtromino;

	// Use this for initialization
	void Start () 
    {
        SpawnRandomArtromino();
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public void SpawnRandomArtromino()
    {
        int rand = Random.Range(1, 8);
        string spawnName = "";
        Vector3 spawnLocation = new Vector3(0.0f, 20f, 0.0f);
        switch (rand)
        {
            case 1:
                spawnName = "Prefabs/I";
                spawnLocation = new Vector3(0.0f, 20.0f, 0.0f);
                break;
            case 2:
                spawnName = "Prefabs/J0";
                spawnLocation = new Vector3(0.0f, 21.0f, 0.0f);
                break;
            case 3:
                spawnName = "Prefabs/J1";
                spawnLocation = new Vector3(0.0f, 21.0f, 0.0f);
                break;
            case 4:
                spawnName = "Prefabs/O";
                spawnLocation = new Vector3(0.0f, 21.0f, 0.0f);
                break;
            case 5:
                spawnName = "Prefabs/T";
                spawnLocation = new Vector3(0.0f, 21.0f, 0.0f);
                break;
            case 6:
                spawnName = "Prefabs/Z0";
                spawnLocation = new Vector3(0.0f, 21.0f, 0.0f);
                break;
            case 7:
                spawnName = "Prefabs/Z1";
                spawnLocation = new Vector3(0.0f, 21.0f, 0.0f);
                break;
        }
                
        GameObject spawnObject = (GameObject)Instantiate(Resources.Load(spawnName), spawnLocation, new Quaternion(0, 0, 0, 0));
        spawnObject.transform.parent = transform;
        currentArtromino = spawnObject.GetComponent<Artromino>();
        currentArtromino.parentArtris = this;
    }

    public void MoveForward() 
    {
        currentArtromino.Move(new Vector3(0, 0, 1.0f));
    }

    public void MoveBack() 
    {        
        currentArtromino.Move(new Vector3(0, 0, -1.0f));
    }

    public void MoveLeft() 
    {
        currentArtromino.Move(new Vector3(1.0f, 0, 0));
    }

    public void MoveRight()
    {
        currentArtromino.Move(new Vector3(-1.0f, 0, 0));
    }

	public void VerticalTransform()
	{
        currentArtromino.Rotate(new Vector3(0, 0, 90));
	}

    public void HorizontalTransform()
    {
        currentArtromino.Rotate(new Vector3(0, 90, 0));
    }
       
}
