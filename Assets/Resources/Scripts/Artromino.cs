using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artromino : MonoBehaviour 
{
    private Artris parentArtris;
    private ArtrominoShadow minoShadow;
    private GameObject indicatorObject;

    private float halfWidth;
    private float halfHeight;
    private float halfCube;

    private float deltaTime = 1.5f;
    private float timeKey = 0.0f;
	
    void Start()
    {
    }

	// Update is called once per frame
	void Update () 
    {
        if (Landing())
        {
            Land();
        }
        Falling();
	}

    void Falling()
    {
        if (Time.time - timeKey >= deltaTime)
        {
            timeKey = Time.time;
            Falling(transform);
            Falling(indicatorObject.transform);
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
            int indexX = (int)Mathf.Round(pos.x + halfWidth + 0.1f);
            int indexY = (int)Mathf.Round(pos.y + 0.1f);
            int indexZ = (int)Mathf.Round(pos.z + halfWidth + 0.1f);
            if ( !NullGrid(indexX, indexY, indexZ) )
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

        string indicatorName = "Prefabs/Indicator";
        indicatorObject = (GameObject)Instantiate(Resources.Load(indicatorName), transform.position, new Quaternion(0, 0, 0, 0));
        indicatorObject.transform.parent = transform.parent;
        indicatorObject.transform.position = transform.position;

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
            indicatorObject.transform.position += dir;
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
        Destroy(indicatorObject);

        parentArtris.UpdateGrid();
    }

    public bool NullGrid(int indexX, int indexY, int indexZ)
    {         
        if (parentArtris)
        {
            return parentArtris.NullGrid(indexX, indexY, indexZ);
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
            if (!NullGrid(indexX, indexY - 1, indexZ))
            {
                return true;
            }
        }

        return false;
    }
}
