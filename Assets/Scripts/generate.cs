using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class generate : MonoBehaviour
{
    private int xAxis = 5;
    private int yAxis = 5;
    private int zAxis = 5;
    public GameObject Cube;

    // Start is called before the first frame update
    void Start()
    {
        /*GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
    */}

    // Update is called once per frame
    void Update()
    {
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
    }
}
