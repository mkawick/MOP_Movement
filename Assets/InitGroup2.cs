#pragma strict
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGroup2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child != transform)
            {
                //child is your child transform
                SetUvs(child);
            }
        }
    }

    void SetUvs(Transform child)
    {
        // Get the mesh
        Mesh theMesh = child.gameObject.GetComponent<MeshFilter>().mesh;

        // Now store a local reference for the UVs
        Vector2[] theUVs = new Vector2[theMesh.uv.Length];
        theUVs = theMesh.uv;

        // set UV co-ordinates
        theUVs[0] = new Vector2(0, 0);
        theUVs[1] = new Vector2(0, 0);
        theUVs[2] = new Vector2(0, 0);
        theUVs[3] = new Vector2(0, 0);

        theUVs[4] = new Vector2(0, 0);
        theUVs[5] = new Vector2(0, 0);
        theUVs[8] = new Vector2(0, 0);
        theUVs[9] = new Vector2(0, 0);

        theUVs[10] = new Vector2(0, 0);
        theUVs[11] = new Vector2(0, 0);
        theUVs[12] = new Vector2(0, 0);
        theUVs[13] = new Vector2(0, 0);

        // Assign the mesh its new UVs
        theMesh.uv = theUVs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
