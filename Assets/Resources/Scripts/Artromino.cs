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
                landed = true;
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
            Vector3 pos = mino.transform.position;
            if (pos.y <= 0.5)
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

    public void Move(Vector3 dir)
    {
        transform.localPosition += dir;
        if (Collided())
        {
            transform.localPosition -= dir;
        }
    }

    public void Rotate(Vector3 angle)
    {
        transform.Rotate(angle);
        if (Collided())
        {
            transform.Rotate(-angle);
        }
    }
}
