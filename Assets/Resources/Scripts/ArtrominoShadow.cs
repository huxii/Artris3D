using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtrominoShadow : MonoBehaviour {

    private Artromino parentArtromino;

	// Update is called once per frame
	void Update () 
    {
		
	}

    bool Landed()
    {
        return parentArtromino.Landing(transform);
    }
        
    // Use this for initialization
    public void Init (Artromino parent) 
    {
        parentArtromino = parent;
        transform.parent = parentArtromino.transform.parent;

        updateShadow();
    }

    public void updateShadow()
    {
        transform.position = parentArtromino.transform.position;
        transform.rotation = parentArtromino.transform.rotation;
        while (true)
        {
            parentArtromino.Falling(transform);
            if (Landed())
            {
                break;
            }
        }
    }

    public bool nullGrid(int indexX, int indexY, int indexZ)
    {         
        if (parentArtromino)
        {
            return parentArtromino.nullGrid(indexX, indexY, indexZ);
        }

        return true;
    }
}
