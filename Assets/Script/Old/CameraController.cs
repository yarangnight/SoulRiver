using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player; // 플레이어 오브젝트의 위치를 확인하기 위해 사용
    public BoxCollider2D bound; // 카메라가 맵 밖을 비추는 것을 방지하기 위해 사용

    //박스 컬라이더 영역의 최소 최대 xyz 값을 지님
    private Vector3 minBound;
    private Vector3 maxBound;

    //카메라의 반너비, 반높이 값을 지닐 변수
    private float halfWidth;
    private float halfHeight;
    
    //카메라의 반높이 값을 구할 속성을 이용하기 위한 변수
    private Camera theCamera;
    // Start is called before the first frame update
    
    

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        this.Player = GameObject.Find("Player"); 

        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerPos = this.Player.transform.position;
        transform.position = new Vector3(PlayerPos.x, PlayerPos.y, transform.position.z);
        
        float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
        float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

        this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
    }
    
}

