using System;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public Transform Cube;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown("a"))
        //{
        //    Cube.transform.Rotate(0, 90 * Time.deltaTime, 0);
        //}
        //if (Input.GetKeyDown("d"))
        //{
        //    Cube.transform.Rotate(0, -90 * Time.deltaTime, 0);
        //}
        //if (Input.GetKeyDown("w"))
        //{
        //    Cube.transform.Rotate(-90 * Time.deltaTime, 0, 0);
        //}
        //if (Input.GetKeyDown("s"))
        //{
        //    Cube.transform.Rotate(90 * Time.deltaTime, 0, 0);
        //}
        //for(int i = 0;  i < 16; i++)
        //{
            rotateCube();
        //}
    }
    void rotateCube()
    {
        if (Input.GetKeyDown("a"))
        {
           Cube.transform.Rotate(0, 22.5f, 0);
        }
        if (Input.GetKeyDown("d"))
        {
            Cube.transform.Rotate(0, -22.5f, 0);
        }
        if (Input.GetKeyDown("w"))
        {
            Cube.transform.Rotate(-22.5f, 0, 0);
        }
        if (Input.GetKeyDown("s"))
        {
            Cube.transform.Rotate(22.5f, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Cube.transform.Rotate(0, 0, 22.5f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Cube.transform.Rotate(0, 0, -22.5f);
        }
    }
}
