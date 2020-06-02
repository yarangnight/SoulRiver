using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public Transform cam;

    Vector2 prevPos = Vector2.zero;
    float prevDistance = 0f;

    void Start()
    {
        cam = Camera.main.transform;
    }
    public void OnDrag()
    {
        int touchCount = Input.touchCount;

        //if (touchCount == 1)
        //{
        //    if (prevPos == Vector2.zero)
        //    {
        //        prevPos = Input.GetTouch(0).position;
        //        return;
        //    }
        
        //Vector2 dir = (Input.GetTouch(0).position - prevPos).normalized;
        //Vector3 vec = new Vector3(dir.x, 0, dir.y);

        //cam.position -= vec * moveSpeed * Time.deltaTime;
        //prevPos = Input.GetTouch(0).position;
        //}

        if (touchCount == 2)
        {
            if (prevDistance == 0)
            {
                prevDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
                return;
            }
            float curDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
            float move = prevDistance - curDistance;

            Vector3 pos = cam.position;

            if (move < 0)
            {
                //pos.y -= moveSpeed * Time.deltaTime;
                cam.GetComponent<Camera>().orthographicSize -= moveSpeed * Time.deltaTime;
            }
            else if (move > 0) 
            {
                //pos.y += moveSpeed * Time.deltaTime;
                cam.GetComponent<Camera>().orthographicSize += moveSpeed * Time.deltaTime;
            }
            //cam.position = pos;
            prevDistance = curDistance;
        }
        
    }


    public void ExitDrag()
    {
        prevPos = Vector2.zero;
        prevDistance = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
