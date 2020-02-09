using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    //public int whichLayer = 9;
    Plane mouseIntersectionPlane;
    bool isMouseFollowing = false;
    Vector3 startWorldDragPos;
    Vector3 startCameraPos;
    Vector3 cameraOffset;
    //public LayerMask whichLayer;

    Vector2 mousePosition;
    public GameObject rootCamera;
    public Camera mainCamera;

    Vector3 velocity, lastDragPosition;

    void Start()
    {
        mouseIntersectionPlane = new Plane(Vector3.up, 0f);
    }

    private Vector3 GetMouseInWorld()
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        float distanceToPlane;

        if (mouseIntersectionPlane.Raycast(ray, out distanceToPlane))
        {
            Vector3 pointOfIntersection = ray.GetPoint(distanceToPlane);
            return pointOfIntersection;
        }
        return Vector3.zero;
    }
    private void Update()
    {
        trackMousePosition();
        if (isMouseFollowing == false && velocity != Vector3.zero)
        {
            mainCamera.transform.position += velocity;
            velocity *= 0.85f;
            if (velocity.magnitude < 0.1)
            {
                velocity = Vector3.zero;
                //isMouseFollowing = false;
            }
        }
    }

    private void trackMousePosition()
    {
        UpdateMousePosition();
        Vector3 mousePos = GetMouseInWorld();
        if (isMouseFollowing == false)
        {
            transform.position = mousePos;
        }
        else
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                velocity = lastDragPosition - mousePos;
                lastDragPosition = mousePos;
                //velocity, lastDragPosition
                Vector3 diff = startWorldDragPos - mousePos;
                Vector3 newCameraPos = diff + startCameraPos;

                mainCamera.transform.position = newCameraPos;
                Debug.Log("camera pos: " + Camera.main.transform.position);
            }
        }
    }

    //void Mous

    void OnMouseDown()
    {
        isMouseFollowing = true;
        startCameraPos = mainCamera.transform.position;
        //Camera.main.transform.position;

        //mainCamera.transform.parent = null;
        startWorldDragPos = GetMouseInWorld();
        lastDragPosition = startWorldDragPos;
        cameraOffset = startWorldDragPos - startCameraPos;
    }

    private void OnMouseUp()
    {
        isMouseFollowing = false;
        //mainCamera.transform.parent = rootCamera.transform;
    }
    void UpdateMousePosition()
    {
        if (Input.mousePosition.y > 0 && Input.mousePosition.y < Screen.height && Input.mousePosition.x > 0 && Input.mousePosition.x < Screen.width)
        {
            mousePosition = Input.mousePosition;
        }
        else
        {
            mousePosition += new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }
}
