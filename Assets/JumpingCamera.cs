using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingCamera : MonoBehaviour
{
    bool isJumping = false;
    public float gravity = -1.2f;
    public float jumpVelocity = 9;
    private float verticalVelocity = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isJumping == false)
        {
            bool isRightClick = Input.GetMouseButtonDown(1);
            if(isRightClick)
            {
                verticalVelocity = jumpVelocity;
                isJumping = true;
            }

            
        }
        else //if (verticalVelocity != 0)
        {
            if (this.transform.position.y + verticalVelocity < 0)
            {
                this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                isJumping = false;
                verticalVelocity = 0;
            }
            else
            {
                this.transform.position += new Vector3(0, verticalVelocity, 0);
            }
            verticalVelocity += gravity;
        }
    }
}
