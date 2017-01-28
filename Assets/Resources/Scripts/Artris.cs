using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artris : MonoBehaviour 
{
    public int gridWidth;
    public int gridHeight;
    public float cubeLen;

    private float halfWidth;
    private float halfHeight;
    private float halfCube;
    private static Transform[ , , ] grid;
  
    private Artromino currentArtromino;

	// Use this for initialization
	void Start () 
    {
        gridWidth = 6;
        gridHeight = 12;
        cubeLen = 1.0f;
        halfWidth = gridWidth / 2;
        halfHeight = gridHeight / 2;
        halfCube = cubeLen / 2;
        grid = new Transform[gridWidth + 2, gridHeight + 2, gridWidth + 2];;

        SpawnRandomArtromino();
        SetEnabled(false);
	}

    public void Pause()
    {
        if (currentArtromino)
        {
            currentArtromino.enabled = !currentArtromino.enabled;
        }
    }

    public void SetEnabled(bool en)
    {
        if (currentArtromino)
        {
            currentArtromino.enabled = en;
        }
    }

    public void SpawnRandomArtromino()
    {
        int rand = Random.Range(1, 8);
        string spawnName = "";
        Vector3 spawnLocation = new Vector3(0.0f, gridHeight, 0.0f);
        switch (rand)
        {
            case 1:
                spawnName = "Prefabs/I";
                spawnLocation = new Vector3(0.0f, gridHeight, 0.0f);
                break;
            case 2:
                spawnName = "Prefabs/J0";
                spawnLocation = new Vector3(0.0f, gridHeight + cubeLen, 0.0f);
                break;
            case 3:
                spawnName = "Prefabs/J1";
                spawnLocation = new Vector3(0.0f, gridHeight + cubeLen, 0.0f);
                break;
            case 4:
                spawnName = "Prefabs/O";
                spawnLocation = new Vector3(0.0f, gridHeight + cubeLen, 0.0f);
                break;
            case 5:
                spawnName = "Prefabs/T";
                spawnLocation = new Vector3(0.0f, gridHeight + cubeLen, 0.0f);
                break;
            case 6:
                spawnName = "Prefabs/Z0";
                spawnLocation = new Vector3(0.0f, gridHeight + cubeLen, 0.0f);
                break;
            case 7:
                spawnName = "Prefabs/Z1";
                spawnLocation = new Vector3(0.0f, gridHeight + cubeLen, 0.0f);
                break;
        }

        GameObject spawnObject = (GameObject)Instantiate(Resources.Load(spawnName), spawnLocation, new Quaternion(0, 0, 0, 0));
        currentArtromino = spawnObject.GetComponent<Artromino>();
        currentArtromino.Init(spawnName, this);
    }

    public void UpdateGrid()
    {
        foreach (Transform mino in currentArtromino.transform)
        {
            int indexX = (int)Mathf.Round(mino.transform.position.x + halfWidth + 0.1f);
            int indexY = (int)Mathf.Round(mino.transform.position.y + 0.1f);
            int indexZ = (int)Mathf.Round(mino.transform.position.z + halfWidth + 0.1f);

            if (indexX <= 0 || indexX > gridWidth || indexY <= 0 || indexY > gridHeight || 
                indexZ <= 0 || indexZ > gridWidth)
            {
                continue;
            }
            grid[indexX, indexY, indexZ] = mino;
        } 

        Destroy(currentArtromino);

        for (int y = 0; y < gridHeight; ++y)
        {
            if (FullRow(y))
            {
                DeleteRow(y);
                DropRow(y + 1);
                --y;
            }
        }

        SpawnRandomArtromino();
    }

    public bool NullGrid(int indexX, int indexY, int indexZ)
    {         
        if (indexY > gridHeight)
        {
            return true;
        }
        if (indexX <= 0 || indexX > gridWidth || indexY <= 0 || 
        indexZ <= 0 || indexZ > gridWidth)
        {
            return false;
        }

        return (grid[indexX, indexY, indexZ] == null);
    }

    // Game Pads control
    public void MoveForward() 
    {
        currentArtromino.Move(new Vector3(0, 0, cubeLen));
    }

    public void MoveBack() 
    {        
        currentArtromino.Move(new Vector3(0, 0, -cubeLen));
    }

    public void MoveLeft() 
    {
        currentArtromino.Move(new Vector3(cubeLen, 0, 0));
    }

    public void MoveRight()
    {
        currentArtromino.Move(new Vector3(-cubeLen, 0, 0));
    }

	public void TransformX()
	{
        currentArtromino.Rotate(new Vector3(1.0f, 0, 0));
	}

    public void TransformY()
    {
        currentArtromino.Rotate(new Vector3(0, 1.0f, 0));
    }

    public void TransformZ()
    {
        currentArtromino.Rotate(new Vector3(0, 0, 1.0f));
    }    

    public void Land()
    {
        currentArtromino.Land();
    }  

    private bool CheckRows(ref List<int> delRows)
    {
        delRows.Clear();
        for (int y = 1; y <= gridHeight; ++y)
        {
            if (FullRow(y))
            {
                delRows.Add(y);
            }
        }
            
        return (delRows.Count > 0);
    }

    private bool FullRow(int y)
    {
        for (int x = 1; x <= gridWidth; ++x)
        {
            for (int z = 1; z <= gridWidth; ++z)
            {
                if (grid[x, y, z] == null)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void DeleteRow(int y)
    {
        for (int x = 1; x <= gridWidth; ++x)
        {
            for (int z = 1; z <= gridWidth; ++z)
            {
                Destroy(grid[x, y, z].gameObject);
                grid[x, y, z] = null;
            }
        }
    }

    private void DropRow(int y)
    {
        for (int Y = y; Y <= gridHeight; ++ Y)
        {
            for (int x = 1; x <= gridWidth; ++x)
            {
                for (int z = 1; z <= gridWidth; ++z)
                {
                    if (!NullGrid(x, Y, z))
                    {
                        grid[x, Y - 1, z] = grid[x, Y, z];
                        grid[x, Y, z] = null;
                        grid[x, Y - 1, z].position += new Vector3(0, -1.0f, 0);
                    }
                }
            }
        }
    }
}
