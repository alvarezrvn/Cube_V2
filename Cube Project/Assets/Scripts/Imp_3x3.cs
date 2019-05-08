
using UnityEngine;

using UnityEngine.SceneManagement;

using System.Collections;

using System.Collections.Generic;



public class Imp_3x3 : MonoBehaviour
{

    public Camera Camera1;

    public Camera Camera2;

    public GUIStyle labelStyle;

    private float horizontalSlider = 5;

    private float rotationSpeed = 0;

    private int rotationCounter = 0;

    private bool shuffleLocked = false;

    private bool rotationLocked = false;



    void OnGUI()

    {

        if (GUI.Button(new Rect(33, 15, 80, 25), "Shuffle") && !this.shuffleLocked)

            StartCoroutine(ShuffleCube());

        if (GUI.Button(new Rect(Screen.width - 112, Screen.height - 40, 80, 25), "Menu"))

            SceneManager.LoadScene("Menu");

        if (GUI.Button(new Rect(Screen.width - 112, Screen.height - 70, 80, 25), "Reset"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);



        // Controla a velocidade de rotaçao das faces do cubo
        labelStyle.normal.textColor = Color.white;

        GUI.Label(new Rect((Screen.width / 2) - 55, 5, 100, 15), "Rotation Speed: " + horizontalSlider, labelStyle);

        horizontalSlider = Mathf.Round(GUI.HorizontalSlider(new Rect((Screen.width / 2) - 50, 25, 100, 100), horizontalSlider, 1, 10));



        // Converte o intervalo de [1, 10] para [0.1,0.3]

        // Formula

        // NewValue = (((OldValue - OldMin) * (NewMax - NewMin)) / (OldMax - OldMin)) + NewMin

        rotationSpeed = (((horizontalSlider - 1f) * (0.35f - 0.1f)) / (10f - 1f)) + 0.1f;

    }



    // Update is called once per frame

    void Update()

    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Camera1.enabled = true;
            Camera2.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Camera1.enabled = false;
            Camera2.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Y) && !this.rotationLocked)
            StartCoroutine(RotateCube('x', 90));

        if (Input.GetKeyDown(KeyCode.N) && !this.rotationLocked)
            StartCoroutine(RotateCube('x', -90));

        if (Input.GetKeyDown(KeyCode.Semicolon) && !this.rotationLocked)
            StartCoroutine(RotateCube('y', 90));

        if (Input.GetKeyDown(KeyCode.A) && !this.rotationLocked)
            StartCoroutine(RotateCube('y', -90));

        if (Input.GetKeyDown(KeyCode.Q) && !this.rotationLocked)
            StartCoroutine(RotateCube('z', 90));

        if (Input.GetKeyDown(KeyCode.P) && !this.rotationLocked)
            StartCoroutine(RotateCube('z', -90));

        if (Input.GetKeyDown(KeyCode.J) && !this.rotationLocked)
            StartCoroutine(RotateFace("TopFace", 'y', 90));

        if (Input.GetKeyDown(KeyCode.F) && !this.rotationLocked)
            StartCoroutine(RotateFace("TopFace", 'y', -90));

        if (Input.GetKeyDown(KeyCode.L) && !this.rotationLocked)
            StartCoroutine(RotateFace("BottomFace", 'y', 90));

        if (Input.GetKeyDown(KeyCode.S) && !this.rotationLocked)
            StartCoroutine(RotateFace("BottomFace", 'y', -90));

        if (Input.GetKeyDown(KeyCode.D) && !this.rotationLocked)
            StartCoroutine(RotateFace("LeftFace", 'x', -90));

        if (Input.GetKeyDown(KeyCode.E) && !this.rotationLocked)
            StartCoroutine(RotateFace("LeftFace", 'x', 90));

        if (Input.GetKeyDown(KeyCode.K) && !this.rotationLocked)
            StartCoroutine(RotateFace("RightFace", 'x', -90));

        if (Input.GetKeyDown(KeyCode.I) && !this.rotationLocked)
            StartCoroutine(RotateFace("RightFace", 'x', 90));

        if (Input.GetKeyDown(KeyCode.H) && !this.rotationLocked)
            StartCoroutine(RotateFace("FrontFace", 'z', -90));

        if (Input.GetKeyDown(KeyCode.G) && !this.rotationLocked)
            StartCoroutine(RotateFace("FrontFace", 'z', 90));

        if (Input.GetKeyDown(KeyCode.O) && !this.rotationLocked)
            StartCoroutine(RotateFace("BackFace", 'z', -90));

        if (Input.GetKeyDown(KeyCode.W) && !this.rotationLocked)
            StartCoroutine(RotateFace("BackFace", 'z', 90));

    }



    public void DoRotate(string faceTag, char axisName, float angle)

    {

        GameObject face = GameObject.FindGameObjectWithTag(faceTag);

        GameObject[] slices = GameObject.FindGameObjectsWithTag("Slice");



        foreach (GameObject slice in slices)

        {

            switch (axisName)

            {

                case 'x':

                    if (Mathf.RoundToInt(slice.transform.position.x) == Mathf.RoundToInt(face.transform.position.x))

                        slice.transform.parent = face.transform;

                    break;

                case 'y':

                    if (Mathf.RoundToInt(slice.transform.position.y) == Mathf.RoundToInt(face.transform.position.y))

                        slice.transform.parent = face.transform;

                    break;

                case 'z':

                    if (Mathf.RoundToInt(slice.transform.position.z) == Mathf.RoundToInt(face.transform.position.z))

                        slice.transform.parent = face.transform;

                    break;

            }

        }



        Quaternion rotation;

        switch (axisName)

        {

            case 'x':

                rotation = Quaternion.Euler(angle, 0, 0);

                break;

            case 'y':

                rotation = Quaternion.Euler(0, angle, 0);

                break;

            case 'z':

                rotation = Quaternion.Euler(0, 0, angle);

                break;

            default:

                rotation = Quaternion.Euler(0, 0, 0);

                break;

        }

        face.transform.rotation = rotation;

    }



    public void AnimateRotate(string faceTag, char axisName, float angle)

    {

        StartCoroutine(RotateFace(faceTag, axisName, angle));

    }



    IEnumerator ShuffleCube()

    {

        this.shuffleLocked = true;

        int i = 0;

        while (i < 25)

        {

            if (!this.rotationLocked)

            {

                switch (Random.Range(0, 11))

                {

                    case 0:

                        StartCoroutine(RotateFace("TopFace", 'y', 90f));

                        break;

                    case 1:

                        StartCoroutine(RotateFace("TopFace", 'y', -90f));

                        break;

                    case 2:

                        StartCoroutine(RotateFace("BottomFace", 'y', -90f));

                        break;

                    case 3:

                        StartCoroutine(RotateFace("BottomFace", 'y', 90f));

                        break;

                    case 4:

                        StartCoroutine(RotateFace("LeftFace", 'x', -90f));

                        break;

                    case 5:

                        StartCoroutine(RotateFace("LeftFace", 'x', 90f));

                        break;

                    case 6:

                        StartCoroutine(RotateFace("RightFace", 'x', -90f));

                        break;

                    case 7:

                        StartCoroutine(RotateFace("RightFace", 'x', 90f));

                        break;

                    case 8:

                        StartCoroutine(RotateFace("FrontFace", 'z', -90f));

                        break;

                    case 9:

                        StartCoroutine(RotateFace("FrontFace", 'z', 90f));

                        break;

                    case 10:

                        StartCoroutine(RotateFace("BackFace", 'z', -90f));

                        break;

                    case 11:

                        StartCoroutine(RotateFace("BackFace", 'z', 90f));

                        break;

                }

                i++;

            }

            else

                yield return null;

        }

        this.shuffleLocked = false;

    }



    IEnumerator RotateFace(string faceTag, char axisName, float angle)

    {

        GameObject face = GameObject.FindGameObjectWithTag(faceTag);

        GameObject[] slices = GameObject.FindGameObjectsWithTag("Slice");



        if (!this.shuffleLocked)

            this.rotationCounter++;



        this.rotationLocked = true;



        foreach (GameObject slice in slices)

        {

            switch (axisName)

            {

                case 'x':

                    if (Mathf.RoundToInt(slice.transform.position.x) == Mathf.RoundToInt(face.transform.position.x))

                        slice.transform.parent = face.transform;

                    break;

                case 'y':

                    if (Mathf.RoundToInt(slice.transform.position.y) == Mathf.RoundToInt(face.transform.position.y))

                        slice.transform.parent = face.transform;

                    break;

                case 'z':

                    if (Mathf.RoundToInt(slice.transform.position.z) == Mathf.RoundToInt(face.transform.position.z))

                        slice.transform.parent = face.transform;

                    break;

            }

        }



        float i = 0.0f;

        while (i < 1.0f)

        {

            i += rotationSpeed;

            Quaternion rotation;

            switch (axisName)

            {

                case 'x':

                    rotation = Quaternion.Euler(angle, 0, 0);

                    break;

                case 'y':

                    rotation = Quaternion.Euler(0, angle, 0);

                    break;

                case 'z':

                    rotation = Quaternion.Euler(0, 0, angle);

                    break;

                default:

                    rotation = Quaternion.Euler(0, 0, 0);

                    break;

            }

            face.transform.rotation = Quaternion.Lerp(face.transform.rotation, rotation, i);

            yield return null;

        }



        foreach (GameObject slice in slices)

        {

            switch (axisName)

            {

                case 'x':

                    if (Mathf.RoundToInt(slice.transform.position.x) == Mathf.RoundToInt(face.transform.position.x))

                        slice.transform.parent = this.gameObject.transform;

                    break;

                case 'y':

                    if (Mathf.RoundToInt(slice.transform.position.y) == Mathf.RoundToInt(face.transform.position.y))

                        slice.transform.parent = this.gameObject.transform;

                    break;

                case 'z':

                    if (Mathf.RoundToInt(slice.transform.position.z) == Mathf.RoundToInt(face.transform.position.z))

                        slice.transform.parent = this.gameObject.transform;

                    break;

            }

        }



        face.transform.rotation = Quaternion.Euler(0, 0, 0);

        this.rotationLocked = false;

    }



    IEnumerator RotateCube(char axis, float axisValue)

    {

        GameObject innerPoint = GameObject.FindGameObjectWithTag("InnerPoint");

        GameObject[] slices = GameObject.FindGameObjectsWithTag("Slice");



        this.rotationLocked = true;



        foreach (GameObject slice in slices)

            slice.transform.parent = innerPoint.transform;



        float i = 0.0f;

        Quaternion rotation;

        while (i < 1.0f)

        {

            i += 0.1f;

            switch (axis)

            {

                case 'x':

                    rotation = Quaternion.Euler(axisValue, 0, 0);

                    break;

                case 'y':

                    rotation = Quaternion.Euler(0, axisValue, 0);

                    break;

                case 'z':

                    rotation = Quaternion.Euler(0, 0, axisValue);

                    break;

                default:

                    rotation = Quaternion.Euler(0, 0, 0);

                    break;

            }

            innerPoint.transform.rotation = Quaternion.Lerp(innerPoint.transform.rotation, rotation, i);

            yield return null;

        }



        foreach (GameObject slice in slices)

            slice.transform.parent = this.gameObject.transform;



        innerPoint.transform.rotation = Quaternion.Euler(0, 0, 0);



        this.rotationLocked = false;

    }

}

