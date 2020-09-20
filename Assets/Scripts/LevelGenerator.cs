using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject Empty;
    public GameObject OutsideCorner;
    public GameObject OutsideWall;
    public GameObject InsideCorner;
    public GameObject InsideWall;
    public GameObject StandardPellet;
    public GameObject PowerPellet;
    public GameObject TJunction;
    private Quaternion rotate;

    void Awake()
    {
        int[,] levelMap = {
            {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
            {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
            {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
            {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
            {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
            {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
            {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
            {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
            {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
            {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
            {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
        };

        for (int x = 0; x < levelMap.GetLength(0); x++)
        {
            for (int y = 0; y < levelMap.GetLength(1); y++)
            {
                int i = levelMap[x, y];
                int up = 0;
                int down = 0;
                int right = 0;
                int left = 0;
                // LoadMap(i, x, y);
                if (x < levelMap.GetLength(0) - 1)
                {
                    down = levelMap[x + 1, y];
                }
                if (x > 0)
                {
                    up = levelMap[x - 1, y];
                }
                if (y < levelMap.GetLength(1) - 1)
                {
                    right = levelMap[x, y + 1];
                }
                if (y > 0)
                {
                    left = levelMap[x, y - 1];
                }

                if (i == 1)
                {
                    rotate = Quaternion.Euler(0, 0, 0);
                    if (up == 2 & right == 2)
                    {
                        rotate = Quaternion.Euler(0, 0, 90);
                    }
                    if (up == 2 & left == 2)
                    {
                        rotate = Quaternion.Euler(180, 180, 0);
                    }
                    if (down == 2 & left == 2)
                    {
                        rotate = Quaternion.Euler(0, 0, -90);
                    }
                    Instantiate(OutsideCorner, new Vector3(y+1, -x+31, 0), rotate);
                }
                if (i == 2)
                {
                    rotate = Quaternion.Euler(0, 0, 0);
                    if (up == 5)
                    {
                        rotate = Quaternion.Euler(0, 0, 90);
                    }
                    if (down == 5)
                    {
                        rotate = Quaternion.Euler(0, 0, -90);
                    }
                    Instantiate(OutsideWall, new Vector3(y+1, -x+31, 0), rotate);
                }
                if (i == 3)
                {
                    rotate = Quaternion.Euler(0, 0, 0);
                    if (up == (3 | 4))
                    {
                        if (right == (3 | 4))
                        {
                            rotate = Quaternion.Euler(0, 0, 90);
                        }
                        if (left == (3 | 4))
                        {
                            rotate = Quaternion.Euler(0, 0, 180);
                        }
                    }
                    if (down == (3 | 4))
                    {
                        if (left == (3 | 4))
                        {
                            rotate = Quaternion.Euler(0, 0, -90);
                        }
                    }
                    Instantiate(InsideCorner, new Vector3(y+1, -x+31, 0), rotate);
                }
                if (i == 4)
                {
                    rotate = Quaternion.Euler(0, 0, 0);
                    if (up == 5)
                    {
                        rotate = Quaternion.Euler(0, 0, -90);
                    }
                    if (down == 5)
                    {
                        rotate = Quaternion.Euler(0, 0, 90);
                    }
                    Instantiate(InsideWall, new Vector3(y+1, -x+31, 0), rotate);
                }
                if (i == 5)
                {
                    Instantiate(StandardPellet, new Vector3(y+1, -x+31, 0), Quaternion.identity);
                }
                if (i == 6)
                {
                    Instantiate(PowerPellet, new Vector3(y+1, -x+31, 0), Quaternion.identity);
                }
                if (i == 7)
                {
                    Instantiate(TJunction, new Vector3(y+1, -x+31, 0), Quaternion.identity);
                }
            }
        }
    }
    void Start()
    {

    }
}
