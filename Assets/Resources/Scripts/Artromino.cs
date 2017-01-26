using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artromino : MonoBehaviour 
{
    private Artris parentArtris;
    private ArtrominoShadow minoShadow;

    private float deltaTime = 1.0f;
    private float timeKey = 0.0f;
	
	// Update is called once per frame
	void Update () 
    {
        Falling();
        if (Landing())
        {
            Land();
        }
	}

    void Falling()
    {
        if (Time.time - timeKey >= deltaTime)
        {
            timeKey = Time.time;
            transform.localPosition += new Vector3(0.0f, -1.0f, 0.0f);
        }        
    }

    bool Landing()
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

    bool Collided()
    {
        foreach (Transform mino in transform)
        {
            Vector3 pos = mino.transform.position;
            if (pos.x < -4.5 || pos.x > 4.5 || pos.z < -4.5 || pos.z > 4.5)
            {
                return true;
            }
        }

        return false;
    }

    // Use this for initialization
    public void Init (string spawnName, Artris parent) 
    {
        parentArtris = parent;
        transform.parent = parentArtris.transform;

        string spawnShadowName = spawnName + "_Shadow";
        GameObject spawnShadowObject = (GameObject)Instantiate(Resources.Load(spawnShadowName), transform.position, new Quaternion(0, 0, 0, 0));
        minoShadow = spawnShadowObject.GetComponent<ArtrominoShadow>();
        minoShadow.Init(this);

        timeKey = Time.time;
    }

    public void Move(Vector3 dir)
    {
        transform.localPosition += dir;
        if (Collided())
        {
            transform.localPosition -= dir;
        }
        else
        {
            minoShadow.updateShadow();
        }
    }

    public void Rotate(Vector3 dir)
    {
        transform.RotateAround(transform.position, dir, 90);
        if (Collided())
        {
            transform.RotateAround(transform.position, dir, -90);
        }
        else
        {
            minoShadow.updateShadow();
        }
    }

    public void Land()
    {   
        transform.localPosition = minoShadow.transform.localPosition;
        transform.localRotation = minoShadow.transform.localRotation;

        Destroy(minoShadow.gameObject);

        parentArtris.updateGrid();
    }

    public bool nullGrid(int indexX, int indexY, int indexZ)
    {         
        if (parentArtris)
        {
            return parentArtris.nullGrid(indexX, indexY, indexZ);
        }

        return true;
    }
}
