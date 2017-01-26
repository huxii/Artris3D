using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artromino : MonoBehaviour 
{
    private Artris parentArtris;
    private ArtrominoShadow minoShadow;

    private float halfWidth;
    private float halfHeight;
    private float halfCube;

    private float deltaTime = 1.0f;
    private float timeKey = 0.0f;
	
    void Start()
    {
    }

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
            Falling(transform);
        }        
    }

    bool Landing()
    {
        return Landing(transform);
    }
        
    bool Collided()
    {
        foreach (Transform mino in transform)
        {
            Vector3 pos = mino.transform.position;
            float limit = halfWidth - halfCube;
            int indexX = (int)Mathf.Round(pos.x + halfWidth + 0.1f);
            int indexZ = (int)Mathf.Round(pos.z + halfWidth + 0.1f);
            if (indexX < 1 || indexX > 6 || indexZ < 1 || indexZ > 6)
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

        halfWidth = parentArtris.gridWidth / 2;
        halfHeight = parentArtris.gridHeight / 2;
        halfCube = parentArtris.cubeLen / 2;

        string spawnShadowName = spawnName + "_Shadow";
        GameObject spawnShadowObject = (GameObject)Instantiate(Resources.Load(spawnShadowName), transform.position, new Quaternion(0, 0, 0, 0));
        minoShadow = spawnShadowObject.GetComponent<ArtrominoShadow>();
        minoShadow.Init(this);

        timeKey = Time.time;
    }

    public void Move(Vector3 dir)
    {
        transform.position += dir;
        if (Collided())
        {
            transform.position -= dir;
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
        transform.position = minoShadow.transform.position;
        transform.rotation = minoShadow.transform.rotation;

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

    public void Falling(Transform target)
    {
        target.position += new Vector3(0.0f, -halfCube * 2, 0.0f);      
    }

    public bool Landing(Transform target)
    {        
        foreach (Transform mino in target)
        {
            int indexY = (int)Mathf.Round(mino.transform.position.y + 0.1f);
            if (indexY <= 1)
            {
                return true;
            }

            int indexX = (int)Mathf.Round(mino.transform.position.x + halfWidth + 0.1f);
            int indexZ = (int)Mathf.Round(mino.transform.position.z + halfWidth + 0.1f);
            if (!nullGrid(indexX, indexY - 1, indexZ))
            {
                return true;
            }
        }

        return false;
    }
}
