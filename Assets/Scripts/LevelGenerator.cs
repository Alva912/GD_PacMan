using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject Empty, OutsideCorner, OutsideWall, InsideCorner, InsideWall, StandardPellet, PowerPellet, TJunction;

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

        int[,] levelMapRotate = {
            {0,-90,-90,-90,-90,-90,-90,-90,-90,-90,-90,-90,-90,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,-90,-90,-90,0,0,-90,-90,-90,-90,0,0},
            {0,0,0,0,0,180,0,0,0,0,0,180,0,0},
            {0,0,90,90,90,180,0,90,90,90,90,180,0,90},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,-90,-90,-90,0,0,-90,0,0,-90,-90,-90},
            {0,0,90,90,90,180,0,0,180,0,90,90,90,-90},
            {0,0,0,0,0,0,0,0,180,0,0,0,0,0},
            {90,90,90,90,90,-90,0,0,90,-90,-90,-90,0,0},
            {0,0,0,0,0,0,0,0,0,90,90,180,0,90},
            {0,0,0,0,0,0,0,0,180,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,180,0,0,-90,-90,0},
            {90,90,90,90,90,180,0,90,180,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };

        for (int x = 0; x < levelMap.GetLength(0); x++)
        {
            for (int y = 0; y < levelMap.GetLength(1); y++)
            {
                int i = levelMap[x, y];
                Quaternion rotate = Quaternion.Euler(0, 0, levelMapRotate[x, y]);

                if (i == 1)
                {
                    Instantiate(OutsideCorner, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(0));
                    Instantiate(OutsideCorner, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(1));
                    Instantiate(OutsideCorner, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(2));
                    Instantiate(OutsideCorner, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(3));
                }
                if (i == 2)
                {
                    Instantiate(OutsideWall, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(0));
                    Instantiate(OutsideWall, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(1));
                    Instantiate(OutsideWall, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(2));
                    Instantiate(OutsideWall, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(3));
                }
                if (i == 3)
                {
                    Instantiate(InsideCorner, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(0));
                    Instantiate(InsideCorner, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(1));
                    Instantiate(InsideCorner, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(2));
                    Instantiate(InsideCorner, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(3));
                }
                if (i == 4)
                {
                    Instantiate(InsideWall, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(0));
                    Instantiate(InsideWall, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(1));
                    Instantiate(InsideWall, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(2));
                    Instantiate(InsideWall, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(3));
                }
                if (i == 5)
                {
                    Instantiate(StandardPellet, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(0));
                    Instantiate(StandardPellet, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(1));
                    Instantiate(StandardPellet, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(2));
                    Instantiate(StandardPellet, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(3));
                }
                if (i == 6)
                {
                    Instantiate(PowerPellet, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(0));
                    Instantiate(PowerPellet, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(1));
                    Instantiate(PowerPellet, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(2));
                    Instantiate(PowerPellet, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(3));
                }
                if (i == 7)
                {
                    Instantiate(TJunction, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(0));
                    Instantiate(TJunction, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(1));
                    Instantiate(TJunction, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(2));
                    Instantiate(TJunction, new Vector3(y + 1, -x + 31, 0), rotate, transform.GetChild(3));
                }
            }
        }

        for (int i = 0; i < 4; i++)
        {
            transform.GetChild(i).GetChild(106).position += new Vector3(-0.125f, -0.125f, 0);
            transform.GetChild(i).GetChild(126).position += new Vector3(-0.25f, -0.25f, 0);
            transform.GetChild(i).GetChild(129).position += new Vector3(0.125f, 0.125f, 0);
            transform.GetChild(i).GetChild(137).position += new Vector3(0.125f, -0.125f, 0);
            transform.GetChild(i).GetChild(158).position += new Vector3(-0.25f, 0, 0);
        }

        transform.GetChild(1).position = new Vector3(29, 0, 0);
        transform.GetChild(1).rotation = Quaternion.Euler(180, 0, 180);
        transform.GetChild(2).position = new Vector3(0, 33, 0);
        transform.GetChild(2).rotation = Quaternion.Euler(0, 180, 180);
        transform.GetChild(3).position = new Vector3(29, 33, 0);
        transform.GetChild(3).rotation = Quaternion.Euler(0, 0, 180);
    }
}
