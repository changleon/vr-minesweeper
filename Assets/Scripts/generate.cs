using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;


public class generate : MonoBehaviour
{
    private int xAxis = 5;
    private int yAxis = 5;
    private int zAxis = 5;
    public GameObject Cube;
    public GameObject Mine;

    // Currently all arbitrary values based on Spica Minesweeper
    // Spica difficulty proportions
    private double proportionEasy = 10d / 81;
    private double proportionInt = 40d / 256;
    private double proportionAdv = 99d / 480;

    // Arbitrary dimensions
    private double dimensionsEasy = 5.0;
    private double dimensionsInt = 8.0;
    private double dimensionsAdv = 10.0;

    // Start is called before the first frame update
    void Start()
    {
        /*GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    */}

    // Update is called once per frame
    void Update()
    {
        // Generate 5x5x5 of cubes
        if (Input.GetKeyDown(KeyCode.G))
        {
            for (int i = 0; i < xAxis; i++)
            {
                for (int j = 0; j < yAxis; j++)
                {
                    for (int k = 0; k < zAxis; k++)
                    {
                        Instantiate(Cube, new Vector3(i, j, k), Quaternion.identity);
                    }
                }
            }
        }

        // Easy level
        if (Input.GetKeyDown(KeyCode.E))
        {
            generateLevel(dimensionsEasy, proportionEasy);
        }

        // Intermediate level
        if (Input.GetKeyDown(KeyCode.I))
        {
            generateLevel(dimensionsInt, proportionInt);
        }

        // Advanced level
        if (Input.GetKeyDown(KeyCode.A))
        {
            generateLevel(dimensionsAdv, proportionAdv);
        }


    }

    // Level Generation, given a difficulty's dimensions and proportion
    void generateLevel(double dimension, double proportion)
    {
        // FIXME: Use "Random.Range(min, max)"
        
        int spaces = (int) Round(Pow(dimension, 3.0f));
        int mines = (int) Round(proportion * spaces);
        int dimensionInt = (int) Round(dimension);
        int minesGenerated = 0;
        
        for (int i = 0; i < dimensionInt; i++)
        {
            for (int j = 0; j < dimensionInt; j++)
            {
                for (int k = 0; k < dimensionInt; k++)
                {
                    // generate a mine
                    if (minesGenerated < mines && Random.Range(1, spaces) < mines)
                    {
                        Instantiate(Mine, new Vector3(i, j, k), Quaternion.identity);
                        minesGenerated++;
                    } 
                    // generate a space
                    else
                    {
                        Instantiate(Cube, new Vector3(i, j, k), Quaternion.identity);
                    }
                }
            }
        }
    }
}
