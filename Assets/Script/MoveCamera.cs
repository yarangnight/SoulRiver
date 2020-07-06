using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public float zoomSpeed;
    public float moveSpeed;
    public Transform cam;

    [SerializeField] private float m_CameraMaxSize;
    [SerializeField] private float m_CameraMinSize;
    [SerializeField] private TileSetSpawner m_TileSetSpawner = null;


    private bool m_isDragOn = false;

    private bool m_isToggleEnbaled = false;

    Vector2 prevPos = Vector2.zero;
    float prevDistance = 0f;

    void Start()
    {
        cam = Camera.main.transform;
        StartCoroutine(ToggleOn());
    }
    public void OnDrag()
    {
        int touchCount = Input.touchCount;

        if (touchCount == 1 && m_isDragOn && m_TileSetSpawner.m_NowTileSet == null)
        {
            if (prevPos == Vector2.zero)
            {
                prevPos = Input.GetTouch(0).position;
                return;
            }

            Vector2 dir = (Input.GetTouch(0).position - prevPos)/*.normalized*/;
            Vector3 vec = new Vector3(dir.x, dir.y,0 );

            cam.position -= vec * moveSpeed * Time.deltaTime;
            prevPos = Input.GetTouch(0).position;
        }

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
                float temp = cam.GetComponent<Camera>().orthographicSize - zoomSpeed * Time.deltaTime;

                if(temp < m_CameraMinSize)
                {
                    temp = m_CameraMinSize;
                }

                cam.GetComponent<Camera>().orthographicSize = temp;

                
            }
            else if (move > 0) 
            {
                //pos.y += moveSpeed * Time.deltaTime;
                float temp = cam.GetComponent<Camera>().orthographicSize + zoomSpeed * Time.deltaTime;

                if (temp > m_CameraMaxSize)
                {
                    temp = m_CameraMaxSize;
                }

                cam.GetComponent<Camera>().orthographicSize = temp;
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

    public void Toggle()
    {
        if (m_isToggleEnbaled)
        {
            m_isDragOn = !m_isDragOn;
            Debug.Log("MoveCamera ToggleEnabled");
        }
    }

    private IEnumerator ToggleOn()
    {
        yield return new WaitForSeconds(2.0f);
        m_isToggleEnbaled = true;
        Debug.Log("toggleON");
    }
}
