using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraControls : MonoBehaviour
{
    [SerializeField]
    private new Camera camera;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private float dollySpeed;
    [SerializeField]
    private float zoomIncrement;
    [SerializeField]
    private float mouseTranslateSpeed;
    [SerializeField]
    private float keyboardTranslateSpeed;

    [SerializeField]
    private GameGrid gameGrid;
       

    private bool wasdMode;
    
    private SelectionPlane selectionPlane;
    private GameObject selectionPlaneObject;

    private void Start()
    {
        gameGrid = GameObject.FindWithTag("GameGrid").GetComponent<GridComponent>().gameGrid;
        selectionPlaneObject = GameObject.FindWithTag("SelectionPlane");
        selectionPlane = selectionPlaneObject.GetComponent<SelectionPlaneComponent>().selectionPlane;
        wasdMode = false;        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F))
        {
            wasdMode = !wasdMode;
        }

        if (wasdMode == false)
        {
            MouseTranslate();
            MoveSelectionPlane();
            Dolly();
            Zoom();
        }
        else
        {
            KeyboardMovement();
        }
    }

    void LateUpdate()
    {
        if (wasdMode == false)
        {
            TurntableRotate();
        }
        else
        {

        }
    }

    private void MoveSelectionPlane()
    {        
        if (Input.GetKey(KeyCode.LeftControl) != true)
        {
            return;
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0.0f)
        {
            if (selectionPlane.height < gameGrid.gridDimensions.y - 1)
            {
                selectionPlane.height += 1;
            }
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0.0f)
        {
            if (selectionPlane.height > 0)
            {
                selectionPlane.height -= 1;
            }
        }
        selectionPlaneObject.transform.position = new Vector3(
            selectionPlaneObject.transform.position.x, 
            selectionPlane.height * gameGrid.cellSize.y - gameGrid.cellSize.y / 2.0f, 
            selectionPlaneObject.transform.position.z);
    }

    private void MouseRotate()
    {
        float y = Input.GetAxisRaw("Mouse X") * rotationSpeed * Time.deltaTime;
        float x = -Input.GetAxisRaw("Mouse Y") * rotationSpeed * Time.deltaTime;

        Vector3 eulerRotation = new Vector3(x, y, 0.0f);
        transform.eulerAngles += eulerRotation;
    }

    private void TurntableRotate()
    {
        if (Input.GetButton("Fire3"))
        {
            float y = Input.GetAxisRaw("Mouse X") * rotationSpeed * Time.deltaTime;
            float x = -Input.GetAxisRaw("Mouse Y") * rotationSpeed * Time.deltaTime;

            Vector3 eulerRotation = new Vector3(x, y, 0.0f);
            transform.eulerAngles += eulerRotation;
        }
    }

    private void Dolly()
    {
        if (Input.GetKey(KeyCode.LeftControl) == true)
        {
            return;
        }
        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            return;
        }
        if (Input.GetKey(KeyCode.LeftAlt) == true)
        {
            return;
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0.0f)
        {
            Vector3 dollyVector = new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel") * dollySpeed * Time.deltaTime);

            camera.transform.Translate(dollyVector, Space.Self);
        }
    }

    private void Zoom()
    {
        if (Input.GetKey(KeyCode.LeftAlt) == true && Input.GetAxisRaw("Mouse ScrollWheel") != 0.0f)
        {
            float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomIncrement;
            camera.focalLength = camera.focalLength + zoom;
        }
    }

    private void MouseTranslate()
    {
        if (Input.GetKey(KeyCode.LeftShift) == true && Input.GetButton("Fire3"))
        {
            float x = -Input.GetAxisRaw("Mouse X") * mouseTranslateSpeed * Time.deltaTime;
            float y = -Input.GetAxisRaw("Mouse Y") * mouseTranslateSpeed * Time.deltaTime;

            Vector3 translateVector = new Vector3(x, y, 0.0f);
            transform.Translate(translateVector, Space.Self);
        }
    }

    private void KeyboardMovement()
    {
        float x = Input.GetAxisRaw("Horizontal") * keyboardTranslateSpeed * Time.deltaTime;
        float z = Input.GetAxisRaw("Vertical") * keyboardTranslateSpeed * Time.deltaTime;
        float y = Input.GetAxisRaw("UpDown") * keyboardTranslateSpeed * Time.deltaTime;

        Vector3 translateVector = new Vector3(x, y, z);
        transform.Translate(translateVector, Space.Self);
    }
}

