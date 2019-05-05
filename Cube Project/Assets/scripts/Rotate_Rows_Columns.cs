using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CLayer
{
    F, //Front Face z-axis
    R, //Right Face x-axis
    U, //Top Face y-axis
    L, //Left Face x-axis
    B, //Back Face z-axis
    D, //DownFace y-axis
}

public struct CubeLayer
{
    public Transform center;
    public Transform c0, c1, c2, c3;
    public Vector3 normal;
    public CubeLayer(Transform aCenter, Transform aC0, Transform aC1, Transform aC2, Transform aC3, Vector3 aNormal)
    {
        m_Index = -1;
        center = aCenter;
        normal = aNormal;
        c0 = aC0;
        c1 = aC1;
        c2 = aC2;
        c3 = aC3;
    }//end CubeLayer
    public CubeLayer RotateCW()                                                             // c0  c1 -->  c3  c0
    {                                                                                       // c3  c2 -->  c2  c1
        return new CubeLayer(center, c3, c0, c2, c1, normal);
    }//end rotate cw
    public CubeLayer RotateCCW()                                                             // c0  c1 -->  c1  c2
    {                                                                                        // c3  c2 -->  c0  c3
        return new CubeLayer(center, c1, c2, c3, c0, normal);
    }//end rotate ccw
    public Transform this[int index]
    {
        get
        {
            switch (index)
            {
                case 0: return center;
                case 1: return c0;
                case 2: return c1;
                case 3: return c2;
                case 4: return c3;
                default: return null;
            }
        }
        set
        {
            switch (index)
            {
                case 0: center = value; return;
                case 1: c0 = value; return;
                case 2: c1 = value; return;
                case 3: c2 = value; return;
                case 4: c3 = value; return;
            }
        }
    }//end this[index]
    private int m_Index;
    public CubeLayer GetEnumerator()
    {
        Reset();
        return this;
    }
    public bool MoveNext()
    {
        return m_Index++ < 10;
    }
    public Transform Current
    {
        get { return this[m_Index]; }
    }
    public void Reset()
    {
        m_Index = -1;
    }
} // struct CubeLayer

public class Rotate_Rows_Columns : MonoBehaviour
{
    /*
     * Array Layout 
     * 
     *     c6---c7
     *   c4---c5 |
     *   |   c8  |
     *   | c2---c3
     *   c0---c1 
     * 
     */
    public Transform[] cubes;
    public Transform rotatePivot;
    private bool m_Rotating = false;

    public CubeLayer this[CLayer layer]
    {
        get
        {
            var c = cubes;
            switch(layer){
                default:
                case CLayer.F: return new CubeLayer(c[8], c[0], c[4], c[5], c[1], Vector3.forward);
                case CLayer.R: return new CubeLayer(c[8], c[1], c[5], c[7], c[3], -Vector3.right);
                case CLayer.U: return new CubeLayer(c[8], c[4], c[6], c[7], c[5], -Vector3.up);
                case CLayer.L: return new CubeLayer(c[8], c[2], c[6], c[4], c[0], Vector3.right);
                case CLayer.B: return new CubeLayer(c[8], c[2], c[3], c[7], c[6], -Vector3.forward);
                case CLayer.D: return new CubeLayer(c[8], c[0], c[1], c[3], c[2], Vector3.up);
            }
        }
        set
        {
            var c = cubes;
            var v = value;
            switch (layer)
            {
                case CLayer.F:
                    c[8] = v.center;
                    c[0] = v.c2;
                    c[4] = v.c3;
                    c[5] = v.c0;
                    c[1] = v.c1;
                    break;
                case CLayer.U:
                    c[8] = v.center;
                    c[0] = v.c1;
                    c[4] = v.c0;
                    c[5] = v.c3;
                    c[1] = v.c2;
                    break;

            }
        }
    }
   
    // Start is called before the first frame update
    void Start()
    {
        if(rotatePivot == null)
        {
            rotatePivot = new GameObject("rotate pivot").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Rotating)
        {
            return;
        }
        bool clockwise = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        if (Input.GetKeyDown(KeyCode.F))
        {
            RotateLayer(CLayer.F, !clockwise);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            RotateLayer(CLayer.U, !clockwise);
        }
    }

    public void RotateLayerCW(CLayer aLayer)
    {
        var layer = this[aLayer];
        this[aLayer] = layer.RotateCW();
        StartCoroutine(RotateLayer(layer, -90f, 5f));
    }
    public void RotateLayerCCW(CLayer aLayer)
    {
        var layer = this[aLayer];
        this[aLayer] = layer.RotateCCW();
        StartCoroutine(RotateLayer(layer, 90f, 5f));
    }
    public void RotateLayer(CLayer aLayer, bool aCounterClockwise)
    {
        if (aCounterClockwise)
        {
            RotateLayerCCW(aLayer);
        }
        else
        {
            RotateLayerCW(aLayer);
        }
    }
    IEnumerator RotateLayer(CubeLayer aLayer, float aDegree, float aSpeed)
    {
        m_Rotating = true;
        rotatePivot.localPosition = Vector3.zero;
        rotatePivot.localRotation = Quaternion.identity;
        foreach (Transform t in aLayer)
        {
            t.parent = rotatePivot;
        }

        Quaternion target = Quaternion.AngleAxis(aDegree, aLayer.normal);
        for (float t = 0f; t <= 1f; t += aSpeed * Time.deltaTime)
        {
            rotatePivot.localRotation = Quaternion.Slerp(Quaternion.identity, target, t);
            yield return null;
        }
        rotatePivot.localRotation = target;
        foreach (Transform t in aLayer)
        {
            t.parent = transform;
        }
        m_Rotating = false;
    }
}
