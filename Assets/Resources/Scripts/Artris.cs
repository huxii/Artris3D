﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Artris : MonoBehaviour 
{
    public int gridWidth;
    public int gridHeight;
    public float cubeLen;

    private float halfWidth;
    private float halfHeight;
    private float halfCube;
    private static Transform[ , , ] grid;

    public GameObject HUD;
    public GameObject EndScene;
    public Text scoreText;
    public Text EndScoreText;
    private int score;
  
    private Artromino currentArtromino;

	// Use this for initialization
	void Start () 
    {
        SetEnabled(false);

        gridWidth = 6;
        gridHeight = 12;
        cubeLen = 1.0f;
        halfWidth = gridWidth / 2;
        halfHeight = gridHeight / 2;
        halfCube = cubeLen / 2;
        grid = new Transform[gridWidth + 2, gridHeight + 8, gridWidth + 2];

        score = 0;
        HUD.SetActive(true);
        EndScene.SetActive(false);

        SpawnRandomArtromino();
	}
       

    private void GameOver()
    {
        SetEnabled(false);

        HUD.SetActive(false);
        EndScene.SetActive(true);
    }

    private void SpawnRandomArtromino()
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

    public void Pause()
    {
        enabled = !enabled;
        if (currentArtromino)
        {
            currentArtromino.enabled = !currentArtromino.enabled;
        }
    }

    public void SetEnabled(bool en)
    {
        enabled = en;
        if (currentArtromino)
        {
            currentArtromino.enabled = en;
        }
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
        EndScoreText.text = score.ToString();
    }

    public void UpdateGrid()
    {
        bool over = false;

        foreach (Transform mino in currentArtromino.transform)
        {
            int indexX = (int)Mathf.Round(mino.transform.position.x + halfWidth + 0.1f);
            int indexY = (int)Mathf.Round(mino.transform.position.y + 0.1f);
            int indexZ = (int)Mathf.Round(mino.transform.position.z + halfWidth + 0.1f);

            if (indexX <= 0 || indexX > gridWidth || indexY <= 0 || indexY > gridHeight || 
                indexZ <= 0 || indexZ > gridWidth)
            {
                over = true;
                break;
            }
            grid[indexX, indexY, indexZ] = mino;
        } 

        Destroy(currentArtromino);

        if (!over)
        {
            int num = 0;
            for (int y = 0; y < gridHeight; ++y)
            {
                if (FullRow(y))
                {
                    DeleteRow(y);
                    DropRow(y + 1);
                    --y;

                    ++num;
                }
            }

            if (num != 0)
            {
                score += (int)Mathf.Round(4 * Mathf.Pow(2, num));
            }
            UpdateScore();

            SpawnRandomArtromino();
        }
        else
        {
            GameOver();
        }
    }

    public bool NullGrid(int indexX, int indexY, int indexZ)
    {         
        if (indexY > gridHeight + 4)
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
        if (enabled)
        {
            currentArtromino.Move(new Vector3(0, 0, cubeLen));
        }
    }

    public void MoveBack() 
    {      
        if (enabled)
        {
            currentArtromino.Move(new Vector3(0, 0, -cubeLen));
        }
    }

    public void MoveLeft() 
    {
        if (enabled)
        {
            currentArtromino.Move(new Vector3(cubeLen, 0, 0));
        }
    }

    public void MoveRight()
    {
        if (enabled)
        {
            currentArtromino.Move(new Vector3(-cubeLen, 0, 0));
        }
    }

    public void TransformX()
    {        
        if (enabled)
        {
            currentArtromino.Rotate(new Vector3(1.0f, 0, 0));
        }
    }

    public void TransformY()
    {
        if (enabled)
        {
            currentArtromino.Rotate(new Vector3(0, 1.0f, 0));
        }
    }

    public void TransformZ()
    {
        if (enabled)
        {
            currentArtromino.Rotate(new Vector3(0, 0, 1.0f));
        }
    }    

    public void Land()
    {
        if (enabled)
        {
            currentArtromino.Land();
        }
    } 
}
