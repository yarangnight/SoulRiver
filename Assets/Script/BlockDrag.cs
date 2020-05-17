using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDrag : MonoBehaviour
{   
    
    BlockGenerator BG; //블록 프리팹들의 부모

  
    void Start()
    {
        BG = GameObject.Find("BlockGenerator").GetComponent<BlockGenerator>(); //스크립트 컴포넌트를 가져옴
    }

    //IEnumerator OnMouseDown() // 마우스 드래그와 정렬와 예외 검출이 동시에 되야함으로 코루틴을 돌려줌
    //{ //BlcokGenerator 클래스의 Update 함수와 외부 클래스에서 돌리는 코루틴에 대한 관계는 아직 파악을 못했음
    //    //그래서 임시로 Update함수에서 돌아야 하는 함수들 중 일부를 코루틴에 가져와서 돌림(Update에 놓으면 안될 때가 있음)

    //    bool Once = true; //드래그 시작 시 최초 한 번만 실행
    //    BG.CheckDrag = true; // 드래그가 시작됨을 BlobkGenerator 스크립트로 통보

    //    Vector3 scrSpace = Camera.main.WorldToScreenPoint(transform.position);
    //    Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, scrSpace.z)); //드래그 떄마다 차이나는 마우스 간격을 매워줌
    //    Vector3 StartPos = new Vector3(); // 최초 한 번 transform의 좌표를 검출
    //    Vector3[] Distance = new Vector3[4]; //현재 블럭 좌표의 거리를 계산

    //    while (Input.GetMouseButton(0))
    //    {
    //        //드래그 시작시 정렬
    //        BG.Arrange_Obj();

    //        // 드래그 할 떄  선택된 타일을 제외한 멍떄리는 블럭 요소들을 따라가게 해줘야함
    //        if (Once)
    //        {
    //            StartPos = new Vector3(transform.position.x, transform.position.y, 0);
    //            for (int i = 0; i < BG.Arrangement.transform.childCount; i++)
    //            { //선택된 블럭 타일과  Block의 자식 타일들이 같지 않으면 
    //                // 드래그되는 블록의 타일로 인식 후 거리를 얻어옴
    //                if (transform.name != BG.Arrangement.transform.GetChild(i).transform.name)
    //                    Distance[i] = BG.Arrangement.transform.GetChild(i).transform.position - StartPos;
    //            }
    //            Once = false;
    //        }

    //        Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, scrSpace.z);
    //        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
          
    //        transform.position = curPosition;

    //        for (int i = 0; i < BG.Arrangement.transform.childCount; i++)
    //        { //선택된 블럭의 타일을 제외한 나머지 타일들을 선택된 블럭의 타일이 이동한 만큼 옮겨줌
    //            Vector3 MemberPos = BG.Arrangement.transform.GetChild(i).transform.position;
    //            if (transform.name != BG.Arrangement.transform.GetChild(i).transform.name)
    //                BG.Arrangement.transform.GetChild(i).transform.position = curPosition + Distance[i];
    //        }
           
    //        yield return new WaitForEndOfFrame();
    //    }
       
    //}

    //public void OnMouseUp() 
    //{
    //    BG.CheckDrag = false;
    //    // 배치한 블록 중 예외사항이 있는 경우 새로운 블록을 생성할 수 없도록 함
    //    if (!BG.confirmexcept()) 
    //    {//마우스를 놓았을 떄 예외가 없는 경우 정렬 후 인스턴스를 비워줌
    //        BG.Arrange_Obj();
    //        BG.BlockEndDrag();
    //        BG.Arrangement = null;
    //    }
       
    //}
    
}
