using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;


public class GridScript : MonoBehaviour
{
    public GameObject SpacePrefab;

    // FIXME: idk i feel like i need to do something here
    private GameSettings _settings;

    private List<List<List<Space>>> _map = new List<List<List<Space>>>();
    public List<List<List<Space>>> Map
    {
        get { return _map; }
    }

    #region FIXME: move to GameManager under GameSettings
    const int dimension = 3;
    const int xAxis = dimension;
    const int yAxis = dimension;
    const int zAxis = dimension;
    public GameObject Cube;
    public GameObject Mine;

    // Currently all arbitrary values based on Spica Minesweeper
    // Spica difficulty proportions
    const double proportionTest = 1d / 9;
    const double proportionEasy = 10d / 81;
    const double proportionInt = 40d / 256;
    const double proportionAdv = 99d / 480;

    // Arbitrary dimensions
    const double dimensionsEasy = 5.0;
    const double dimensionsInt = 8.0;
    const double dimensionsAdv = 10.0;

    private readonly int spaces = (int)Round(Pow(dimension, 3.0f));
    private readonly int mines = (int)Round(proportionTest * Pow(dimension, 3.0f));
    #endregion 
    // FIXME: add GameSettings class to GameManager

    // Start is called before the first frame update
    void Start()
    {
        /*GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    */
    }

    /*// Update is called once per frame
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
                        Space space = ((GameObject)Instantiate(Cube, new Vector3(i, j, k), Quaternion.identity)).GetComponent<Space>();
                        space.Position = new Vector3(i, j, k);
                        //Instantiate(Cube, new Vector3(i, j, k), Quaternion.identity);
                    }
                }
            }

        }
        //Tile tile = ((GameObject)Instantiate(TilePrefab, new Vector3(i - Mathf.Floor(_settings.Width / 2), 0, -j + Mathf.Floor(_settings.Height / 2)), Quaternion.identity)).GetComponent<Tile>();

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

        // Clear all Cube and Mine GameObjects from the screeen
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObejct.Destroy(Cube, true);
            GameObejct.Destroy(Mine, true);
        }
    }

    // FIXME: deprecated?
    // Level Generation, given a difficulty's dimensions and proportion
    void generateLevel(double dimension, double proportion)
    {
        // FIXME: Use "Random.Range(min, max)"

        int spaces = (int)Round(Pow(dimension, 3.0f));
        int mines = (int)Round(proportion * spaces);
        int dimensionInt = (int)Round(dimension);
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
    }*/

    /* ---------------------------------- NEW WORK ---------------------------------- */

    public void GenerateMap(GameSettings Settings)
    {
        _settings = Settings;
        _map = new List<List<List<Space>>>();
        for (int i = 0; i < _settings.Width; i++)
        {
            List<List<Space>> plane = new List<List<Space>>();
            for (int j = 0; j < _settings.Height; j++)
            {
                List<Space> row = new List<Space>();
                for (int k = 0; k < _settings.Depth; k++)
                {
                    Space space = ((GameObject)Instantiate(Cube, new Vector3(i, j, k), Quaternion.identity)).GetComponent<Space>();
                    space.Position = new Vector3(i, j, k);
                    row.Add(space);

                    // FIXME: probably wanna use these but what do they mean
                    //tile.transform.parent = transform;
                    //tile.GetComponent<Tile>().Grid = this;
                }
                plane.Add(row);
            }
            _map.Add(plane);
        }
        PlaceMinesOnSpaces();
    }

    void PlaceMinesOnSpaces()
    {
        HashSet<Vector3> mineLocations = new HashSet<Vector3>();

        // randomly pick locations on which mines will be placed.

        // FIXME:
        while (mineLocations.Count < _settings.Mines)
//        while (mineLocations.Count < mines)
        {
            int x = Random.Range(0, xAxis);
            int y = Random.Range(0, yAxis);
            int z = Random.Range(0, zAxis);

            mineLocations.Add(_map[x][y][z].Position);
        }

        // place mines on locations
        foreach (Vector3 loc in mineLocations)
            _map[(int)loc.x][(int)loc.y][(int)loc.z].PlaceMineOnSpace();

        //Debug.Log("mine located at: " + x + ", " + y + ", " + z);
        //Debug.Log(mineLocations.Count + " mines randomly placed on the grid");
    }

    void UpdateTileValues()
    {
        // update tile values
        foreach (List<List<Space>> plane in _map)
        {
            // for each tile
            foreach (List<Space> row in plane)
            {
                foreach(Space space in row)
                {
                    SetNeighbors(space);

                    // if current tile is not a mine
                    if (!space.IsMine())
                    {
                        // traverse neighbors to update tile value
                        int NearbyMineCount = 0;
                        foreach (Vector3 pos in space.NeighborSpacePositions)
                        {
                            // increment nearby mine count by 1 if a neighbor tile is a mine
                            Space _space = _map[(int)pos.x][(int)pos.y][(int)pos.z];
                            if (_space.IsMine())
                                ++NearbyMineCount;
                        }

                        // update the tile value
                        space.SpaceValue = NearbyMineCount;
                    }
                }
            }
        }
    }

    void SetNeighbors(Space space)
    {
        // first, add every possible neighbor tile position to the possible-neighbors list
        // FIXME: there are 26. add all of them
        List<Vector3> NeighborPositions = new List<Vector3>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                for (int k = -1; k < 2; k++)
                {
                    NeighborPositions.Add(new Vector3(space.Position.x + i, space.Position.y + j, space.Position.z + k));
                }
            }
        }

        // then remove those positions which are beyond boundary
        for (int i = NeighborPositions.Count - 1; i >= 0; --i)
        {
            Vector3 pos = NeighborPositions[i];
            if (pos.x < 0 || pos.x >= _settings.Width || pos.y < 0 || pos.y >= _settings.Height || pos.z < 0 || pos.z >= _settings.Depth)
            {
                NeighborPositions.RemoveAt(i);
            }
        }

        // set the correct neighbor positions in the given tile
        space.NeighborSpacePositions = NeighborPositions;
    }
}
