using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artromino : MonoBehaviour {

    public Artris parentArtris;
    private float deltaTime = 1.0f;
    private float timeKey = 0.0f;
    private bool landed = false;

	// Use this for initialization
	void Start () {
        timeKey = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!landed)
        {
            Falling();
            if (Landed())
            {
                parentArtris.SpawnRandomArtromino();
            }
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

    bool Landed()
    {
        foreach (Transform mino in transform)
        {
            int indexY = (int)Mathf.Round(mino.transform.position.y + 0.1f);
            if (indexY <= 1)
            {
                landed = true; 
                break;
            }

            int indexX = (int)Mathf.Round(mino.transform.position.x + 5.1f);
            int indexZ = (int)Mathf.Round(mino.transform.position.z + 5.1f);
            if (!parentArtris.nullGrid(indexX, indexY - 1, indexZ))
            {
                landed = true;
                break;
            }
        }

        if (landed)
        {
            foreach (Transform mino in transform)
            {
                parentArtris.updateGrid(
                    (int)Mathf.Round(mino.transform.position.x + 5.1f),
                    (int)Mathf.Round(mino.transform.position.y + 0.1f),
                    (int)Mathf.Round(mino.transform.position.z + 5.1f),
                    mino
                );
            }  
        }

        return landed;
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

    public void Move(Vector3 dir)
    {
        transform.localPosition += dir;
        if (Collided())
        {
            transform.localPosition -= dir;
        }
    }

    public void Rotate(Vector3 dir)
    {
        transform.RotateAround(transform.position, dir, 90);
        if (Collided())
        {
            transform.RotateAround(transform.position, dir, -90);
        }
    }
}
