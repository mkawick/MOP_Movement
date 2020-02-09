using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectAttachCameraToMouse : MonoBehaviour
{
    Plane mouseIntersectionPlane;

    [SerializeField]
    private GameObject rootCamera;

    [SerializeField]
    private Camera mainCamera;

    Vector2 mousePosition;
    bool isTrackingPlayerMovement;
    Vector3 startHandPosition;
    Vector3 startCameraPosition;

    //Vector3 lastDragPosition;
    Queue <Vector3> lastDragPositions = new Queue<Vector3>(); 
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        isTrackingPlayerMovement = false;
        mouseIntersectionPlane = new Plane(Vector3.up, 0f);
        //mouseIntersectionPlane.
    }

    // Update is called once per frame
    void Update()
    {
        //trackMousePosition();
        UpdatePositionBasecOnVelocity();
    }

    private void FixedUpdate()
    {
        trackMousePosition();
    }

    void TestForMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTrackingPlayerMovement == false)
            {
                isTrackingPlayerMovement = true;

                startHandPosition = this.transform.position;
                startCameraPosition = rootCamera.transform.position;

                ClearVelocityTracking();
                AddValueAndTrackVelocity(startHandPosition);

                Vector3 miw = GetMouseInWorld();
                Debug.Log("MP:" + mousePosition + ", miw:" + miw);
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isTrackingPlayerMovement = false;
        }
    }
    private void UpdatePositionBasecOnVelocity()
{
        trackMousePosition();
        if (isTrackingPlayerMovement == false && velocity != Vector3.zero)
        {
            rootCamera.transform.position += velocity;
            velocity *= 0.85f;
            if (velocity.magnitude < 0.1)
            {
                velocity = Vector3.zero;
                //isMouseFollowing = false;
            }
        }
    }

    void OnMouseDown()
    {
       /* isMouseFollowing = true;
        ///mainCamera.transform.parent = this.transform;

        startHandPosition = this.transform.position;
        startCameraPosition = rootCamera.transform.position;

        Vector3 miw = GetMouseInWorld();
        Debug.Log("MP:" + mousePosition + ", miw:" + miw);*/
    }
    private void OnMouseUp()
    {
       // isMouseFollowing = false;
        //mainCamera.transform.parent = rootCamera.transform;
    }

    //----------------------------------------------------------
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
    private void trackMousePosition()
    {
        UpdateMousePosition();
        Vector3 mousePos = GetMouseInWorld();
        TestForMouseDown();
        if (isTrackingPlayerMovement == true )
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                HandleMouseMoveWithMouseButtonDown(mousePos);
            }
        }
        else
        {
            transform.position = mousePos;
        }
    }
    void HandleMouseMoveWithMouseButtonDown(Vector3 mousePos)
    {
        /*velocity = lastDragPosition - mousePos;
        lastDragPosition = mousePos;*/
        AddValueAndTrackVelocity(mousePos);

        Vector3 currentOffset = startHandPosition - mousePos;
        transform.position = mousePos;
        rootCamera.transform.position = startCameraPosition - currentOffset;
        Debug.Log("mousePos:" + mousePos + ", currentOffset:" + currentOffset);
    }

    void ClearVelocityTracking()
    {
        lastDragPositions.Clear();
    }
    void AddValueAndTrackVelocity(Vector3 newPosition)
    {
        int maxVelocityTracker = 4;
        if(lastDragPositions.Count > maxVelocityTracker)
        {
            lastDragPositions.Dequeue();
        }

        lastDragPositions.Enqueue(newPosition);
        if (lastDragPositions.Count > 1)
        {
            Vector3[] pointArray = lastDragPositions.ToArray();

            Vector3 dist = Vector3.zero;

            for (int i = 0; i < pointArray.Length - 1; i++)
            {
                dist += pointArray[i + 1] - pointArray[i];
            }
            dist *= 1.0f / pointArray.Length;
            velocity = dist;
        }
        else
        {
            velocity = Vector3.zero;
        }
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
