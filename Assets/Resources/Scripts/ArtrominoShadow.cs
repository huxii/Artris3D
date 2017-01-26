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
        foreach (Transform mino in transform)
        {
            int indexY = (int)Mathf.Round(mino.transform.position.y + 0.1f);
            if (indexY <= 1)
            {
                return true; 
            }

            int indexX = (int)Mathf.Round(mino.transform.position.x + 5.1f);
            int indexZ = (int)Mathf.Round(mino.transform.position.z + 5.1f);
            if (!nullGrid(indexX, indexY - 1, indexZ))
            {
                return true;
            }
        }

        return false;
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
        transform.localPosition = parentArtromino.transform.localPosition;
        transform.localRotation = parentArtromino.transform.localRotation;
        while (true)
        {
            transform.localPosition += new Vector3(0.0f, -1.0f, 0.0f);
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
