using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BlockGenerator : MonoBehaviour
{
    GameObject[] BlockPrefab;  //블럭 프리팹을 담아 놓을 곳 
    GameObject[,] BlockPrefab_R; // 회전된 블록을 담아 놓을 곳
    public GameObject BP; // 블럭들의 부모를 담아놓음
    public GameObject Arrangement = null; // 현재 블럭 인스턴스를 받음  
    public bool CheckException; // 겹침 예외판정
    BlockDrag BD; // 블럭드래그 스트립트 컴포넌트 캐싱
    Transform PlayerPos; // 플레이어 위치 값 정보
    SelectBlock_UI SelectBlock;
    List<float> Sort_PS; // oder in layer 순위 설정을 위해 y축 좌표를 리스트에 넣어둠
    List<Vector3> Search_PS; // 맵에 있는 모든 타일과 블럭 좌표 리스트

    public SpriteRenderer BlockColorAlpha; // Arrang_obj 함수에서 Block 색의 Alpha 값을 받아옴

    int CheckNum = -1; // Select 클래스의 Check의 값을 담아두는 임시변수 Rotation 함수에서 쓰기 위함.
    int ClickNum = 0; //회전을 몇 번 했는지 확인  
    Color BlockColor = new Color(); // 블록의 원래 색을 담아둠
    public bool CheckDrag = false; // 드래그가 시작됬는지 확인

    // 맵에 배치된 타일과 블록의 요소 타일들의 거리를 비교
    // 가장 가까운 타일을 검출 후 그 타일의 좌표 저장
    // 가장 가까운 타일의 블록의 가장 가까운 자식 타일을 가져다 붙임(Angle을 구하여 90도 마다 상하좌우로 배치)
    // 선택되서 옮겨진 타일이 움직인 거리만큼 나머지 아직 움직이지 않은 블럭의 자식 타일들을 옮겨줌
    // 블록과 블록이 붙게 할 수 있을 수 있도록 거리를 비교후 가져다 붙임
    // 이미 타일이 배치되어 있는 곳은 겹쳐서 놓을 수 없게해야함 (맵에 있는 모든 타일의 좌표 값을 저장 후 배치할 블록의 타일들과 비교)
    // 아직 배치되지 않은 블럭은 드래그 전까지 order in layer 값이 가장 높게 유지시킴


    // 드래그 중인 블록은 투명색을 유지시켜야함
    // 드래그 중에도 블럭은 정렬과 예외 검사 시킴
    // 드래그 후에 정렬과 예외 검사를 마친후 블록이 겹치는 예외 발생시 블록을 빨간색으로 바꿈
    // 예외가 있는 블럭이 맵에 있는 경우 새로운 프리팹 생성 제한
    // 드래그 종료 후 다시 정렬 시키고 색은 원래대로 되돌려야함

    private void Awake()
    {
    }


    // Start is called before the first frame update
    void Start()
    {
        BP = GameObject.Find("BlockParent");
        BD = GameObject.Find("BlockDrag").GetComponent<BlockDrag>(); //BlockDrag의 스크립트 컴포넌트를 가져옴
        SelectBlock = GameObject.Find("SelectBlock").GetComponent<SelectBlock_UI>(); //SelectBlock의 스크립트 컴포넌트

        // CheckNum = -1;
        // ClickNum = 0;
        BlockPrefab = new GameObject[7];// 클릭 시 생성되는 블럭
        BlockPrefab[0] = Resources.Load("Chapter 1/Tile/BlockPrefab1") as GameObject;
        BlockPrefab[1] = Resources.Load("Chapter 1/Tile/BlockPrefab2") as GameObject;
        BlockPrefab[2] = Resources.Load("Chapter 1/Tile/BlockPrefab3") as GameObject;
        BlockPrefab[3] = Resources.Load("Chapter 1/Tile/BlockPrefab4") as GameObject;
        BlockPrefab[4] = Resources.Load("Chapter 1/Tile/BlockPrefab5") as GameObject;
        BlockPrefab[5] = Resources.Load("Chapter 1/Tile/BlockPrefab6") as GameObject;
        BlockPrefab[6] = Resources.Load("Chapter 1/Tile/BlockPrefab7") as GameObject;

        BlockPrefab_R = new GameObject[7, 3]; //회전시 생성되는 블록
        BlockPrefab_R[0, 0] = Resources.Load("Chapter 1/Tile/BlockPrefab1") as GameObject;
        BlockPrefab_R[1, 0] = Resources.Load("Chapter 1/Tile/BlockPrefab2_R1") as GameObject;
        BlockPrefab_R[2, 0] = Resources.Load("Chapter 1/Tile/BlockPrefab3_R1") as GameObject;
        BlockPrefab_R[2, 1] = Resources.Load("Chapter 1/Tile/BlockPrefab3_R2") as GameObject;
        BlockPrefab_R[2, 2] = Resources.Load("Chapter 1/Tile/BlockPrefab3_R3") as GameObject;
        BlockPrefab_R[3, 0] = Resources.Load("Chapter 1/Tile/BlockPrefab4_R1") as GameObject;
        BlockPrefab_R[3, 1] = Resources.Load("Chapter 1/Tile/BlockPrefab4_R2") as GameObject;
        BlockPrefab_R[3, 2] = Resources.Load("Chapter 1/Tile/BlockPrefab4_R3") as GameObject;
        BlockPrefab_R[4, 0] = Resources.Load("Chapter 1/Tile/BlockPrefab5_R1") as GameObject;
        BlockPrefab_R[5, 0] = Resources.Load("Chapter 1/Tile/BlockPrefab6_R1") as GameObject;
        BlockPrefab_R[6, 0] = Resources.Load("Chapter 1/Tile/BlockPrefab7_R1") as GameObject;
        BlockPrefab_R[6, 1] = Resources.Load("Chapter 1/Tile/BlockPrefab7_R2") as GameObject;
        BlockPrefab_R[6, 2] = Resources.Load("Chapter 1/Tile/BlockPrefab7_R3") as GameObject;
    }

    public void Arrange_Obj()
    { //타일들의 위치를 확인하기 위해 부모를 받아옴
        GameObject TP = GameObject.Find("TileParent");




        // 기존 맵 배치 타일과 블록의 자식 타일들의 거리를 비교
        float distance = Vector3.Distance(a: TP.transform.GetChild(0).transform.position, b: Arrangement.transform.GetChild(0).transform.position);
        // 가장 가까운 타일을 검출 후 그 타일의 좌표 저장
        Vector3 Nearest_Tile = new Vector3(TP.transform.GetChild(0).transform.position.x, TP.transform.GetChild(0).transform.position.y, 0);
        // 가장 가까운 타일과 가장 가까운 블록의 자식 타일의 번호 저장
        int BTileNum = 0;
        for (int i = 0; i < TP.transform.childCount; i++)
        {// TileParent가 가진 타일들의 수만큼
         //기본 배치 타일과 지금 배치하려는 블럭의 타일 비교
            for (int j = 0; j < Arrangement.transform.childCount; j++) //현재 블럭의 자식인 타일 수 만큼
            {
                if (distance > Vector3.Distance(a: TP.transform.GetChild(i).transform.position,       //타일과 가장 가까운 블럭을 검출
                                                b: Arrangement.transform.GetChild(j).transform.position))
                {
                    distance = Vector3.Distance(a: TP.transform.GetChild(i).transform.position, b: Arrangement.transform.GetChild(j).transform.position);
                    Nearest_Tile = new Vector3(TP.transform.GetChild(i).transform.position.x, TP.transform.GetChild(i).transform.position.y, 0);
                    BTileNum = j; //선택된 블럭의 타일
                }

            }
        }


        //이미 배치 시킨 블럭의 타일과 현재 배치 시키려는 블럭의 타일의 비교
        for (int i = 0; i < BP.transform.childCount; i++) // 이미 배치 시킨 블럭 덩어리 수
        {
            for (int j = 0; j < BP.transform.GetChild(i).transform.childCount; j++) //이미 배치 시킨 각 블럭 덩어리의 각각 자식 타일 수 
            {
                if (BP.transform.GetChild(i).transform.position != Arrangement.transform.position) //배치하려는 블럭과 배치되어있는 블럭이 같지 않으면
                {
                    for (int z = 0; z < Arrangement.transform.childCount; z++)
                    {//이미 배치된 블럭과 가장 가까운 블럭을 검출
                        if (distance > Vector3.Distance(
                            a: BP.transform.GetChild(i).transform.GetChild(j).transform.position,
                            b: Arrangement.transform.GetChild(z).transform.position))
                        {
                            distance = Vector3.Distance(
                            a: BP.transform.GetChild(i).transform.GetChild(j).transform.position,
                            b: Arrangement.transform.GetChild(z).transform.position);
                            Nearest_Tile = new Vector3(BP.transform.GetChild(i).transform.GetChild(j).transform.position.x,
                                           BP.transform.GetChild(i).transform.GetChild(j).transform.position.y, 0);
                            BTileNum = z; //선택된 블럭의 타일 인덱스 값
                        }
                    }
                }
            }
        }


        // 옮긴 거리를 계산하기 위해 선택된 블럭 타일의 이전 좌표를 저장
        Vector3 Memory_PS = new Vector3(Arrangement.transform.GetChild(BTileNum).transform.position.x, Arrangement.transform.GetChild(BTileNum).transform.position.y, 0);


        float Angle = Quaternion.FromToRotation(Vector3.up, Memory_PS - Nearest_Tile).eulerAngles.z; // 기존 타일과 배치될 블럭의 자식 타일의 각도를 구함

        // 가장 가까운 타일에 블록의 가장 가까운 자식 타일을 가져다 붙임
        if (Angle > 90.0f && Angle < 180.0f)// 앞에 붙는 경우
            Arrangement.transform.GetChild(BTileNum).transform.position = new Vector3(Nearest_Tile.x - 0.44f, Nearest_Tile.y - 0.22f, 0);
        if (Angle > 270.0f && Angle < 360.0f) // 뒤에 붙는 경우
            Arrangement.transform.GetChild(BTileNum).transform.position = new Vector3(Nearest_Tile.x + 0.44f, Nearest_Tile.y + 0.22f, 0);
        if (Angle > 0.0f && Angle < 90.0f) // 위에 붙는 경우
            Arrangement.transform.GetChild(BTileNum).transform.position = new Vector3(Nearest_Tile.x - 0.44f, Nearest_Tile.y + 0.22f, 0);
        if (Angle > 180.0f && Angle < 270.0f) // 아래에 붙는 경우
            Arrangement.transform.GetChild(BTileNum).transform.position = new Vector3(Nearest_Tile.x + 0.44f, Nearest_Tile.y - 0.22f, 0);

        //4.선택되서 옮겨진 타일이 움직인 거리만큼 나머지 움직이지 않은 자식 타일들을 옮겨줌
        for (int i = 0; i < Arrangement.transform.childCount; i++)
        {
            if (i != BTileNum)
            {
                if (Angle > 90.0f && Angle < 180.0f) // 앞에 붙는 경우
                    Arrangement.transform.GetChild(i).transform.position =
                    new Vector3(Arrangement.transform.GetChild(i).transform.position.x + (Nearest_Tile.x - Memory_PS.x) - 0.44f,
                    Arrangement.transform.GetChild(i).transform.position.y + (Nearest_Tile.y - Memory_PS.y) - 0.22f, 0);

                if (Angle > 270.0f && Angle < 360.0f) // 뒤에 붙는 경우
                    Arrangement.transform.GetChild(i).transform.position =
                    new Vector3(Arrangement.transform.GetChild(i).transform.position.x + (Nearest_Tile.x - Memory_PS.x) + 0.44f,
                    Arrangement.transform.GetChild(i).transform.position.y + (Nearest_Tile.y - Memory_PS.y) + 0.22f, 0);

                if (Angle > 0.0f && Angle < 90.0f) // 위에 붙는 경우
                    Arrangement.transform.GetChild(i).transform.position =
                    new Vector3(Arrangement.transform.GetChild(i).transform.position.x + (Nearest_Tile.x - Memory_PS.x) - 0.44f,
                    Arrangement.transform.GetChild(i).transform.position.y + (Nearest_Tile.y - Memory_PS.y) + 0.22f, 0);

                if (Angle > 180.0f && Angle < 270.0f) // 아래에 붙는 경우
                    Arrangement.transform.GetChild(i).transform.position =
                    new Vector3(Arrangement.transform.GetChild(i).transform.position.x + (Nearest_Tile.x - Memory_PS.x) + 0.44f,
                    Arrangement.transform.GetChild(i).transform.position.y + (Nearest_Tile.y - Memory_PS.y) - 0.22f, 0);
            }
        }


        Search_PS = new List<Vector3>(); //맵에 있는 모든 타일의 좌표 (겹치는거 방지)
        Sort_PS = new List<float>(); // 맵에 있는 모든 타일의 y축의 값을 받는다. (order in layer 순위 변경)
        for (int i = 0; i < TP.transform.childCount; i++) // 기존 맵 배치 타일 수 
        {
            Sort_PS.Add(TP.transform.GetChild(i).transform.position.y); // 기존 맵배치 타일의 y축 저장           
        }

        for (int j = 0; j < BP.transform.childCount; j++) //드래그로 배치되어진 블록 수
        {
            for (int z = 0; z < BP.transform.GetChild(j).transform.childCount; z++) //배치되어진 블록의 각 자식 타일 수
            {
                Sort_PS.Add(BP.transform.GetChild(j).transform.GetChild(z).transform.position.y); // 블록타일의 자식 타일 y축 저장              
            }
        }

        for (int i = 0; i < TP.transform.childCount; i++) // 맵에 있는 타일 수 
        {
            Search_PS.Add(TP.transform.GetChild(i).transform.position); // 맵에 있는 타일의 좌표를 저장
        }

        for (int j = 0; j < BP.transform.childCount - 1; j++) //드래그로 배치되어진 블록 수에서 현재 배치하려는 블록은 제외
        {
            for (int z = 0; z < BP.transform.GetChild(j).transform.childCount; z++) //배치 되어있는 블록들의 수
            {  //배치 되어있는 블록들의 각각 자식 타일들의 좌표를 저장
                Search_PS.Add(BP.transform.GetChild(j).transform.GetChild(z).transform.position);
            }
        }

        Sort_PS.Sort(); //축을 값이 낮은 순서대로 정렬 (오름차순)
        for (int i = 0; i < Sort_PS.Count; i++)
        {
            for (int j = 0; j < TP.transform.childCount; j++)// 기존 맵 배치 타일
            {
                if (Sort_PS[i] == TP.transform.GetChild(j).transform.position.y) //Sort_PS[0]이 가장 높은 layer 값을 가져야함 (인덱스의 역순)
                {//y축을 기준으로 오름차순된 리스트에서 y축이 더 낮은 타일이 더 높은 레이어를 갖는다.
                    TP.transform.GetChild(j).GetComponent<SpriteRenderer>().sortingOrder = Sort_PS.Count - i;

                }
            }

            for (int z = 0; z < BP.transform.childCount; z++) //현재 블럭들의 수
            {
                for (int z_1 = 0; z_1 < BP.transform.GetChild(z).transform.childCount; z_1++) // 각 블럭 덩어리의 각 자식 타일들
                { //모든 블록들의 모든 자식 타일
                    if (Sort_PS[i] == BP.transform.GetChild(z).transform.GetChild(z_1).transform.position.y)
                    {
                        BP.transform.GetChild(z).transform.GetChild(z_1).GetComponent<SpriteRenderer>().sortingOrder = Sort_PS.Count - i; // " 위와 동일  
                    }
                }
            }
        }
        Sort_PS.Clear();  // 한 번 찍어낸 프리팹의 좌표는 다 지워줌
    }

    public void BlockBeingDrag()
    {
        //배치하려는 블록을 투명색으로 적용
        for (int i = 0; i < Arrangement.transform.childCount; i++)
        {
            BlockColorAlpha = Arrangement.transform.GetChild(i).transform.GetComponent<SpriteRenderer>();
            Color color = BlockColorAlpha.color;
            color.a = 0.5f; // 반투명 범위 0 ~ 1.0f
            BlockColorAlpha.color = color;
        }
    }

    public void BlockEndDrag()
    { //배치 후 불투명색으로 적용
        for (int i = 0; i < Arrangement.transform.childCount; i++)
        {
            BlockColorAlpha = Arrangement.transform.GetChild(i).transform.GetComponent<SpriteRenderer>();
            Color color = BlockColorAlpha.color;
            color.a = 1.0f; // 반투명  기존 범위 0 ~ 1.0f
            BlockColorAlpha.color = color;
        }
    }

    public void Exception() //겹치는 경우 
    {
        bool CheckException = true;
        for (int i = 0; i < Search_PS.Count; i++)
        {
            for (int j = 0; j < Arrangement.transform.childCount; j++)
            {
                if (Search_PS[i] == Arrangement.transform.GetChild(j).transform.position)
                    CheckException = false; // 맵에 있는 타일 중 하나라도 겹치면 예외 검출
            }
        }

        if (CheckException == false)
        { //겹쳐있다면 레드로 
            for (int i = 0; i < Arrangement.transform.childCount; i++)
                Arrangement.transform.GetChild(i).transform.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (CheckException != false)
        {// 안겹쳐있다면 기존 블록의 색으로
            for (int i = 0; i < Arrangement.transform.childCount; i++)
            {
                Arrangement.transform.GetChild(i).transform.GetComponent<SpriteRenderer>().color = BlockColor;
            }
        }
    }

    public bool confirmexcept() // 배치한 블록 중 예외사항이 있는 경우 새로운 블록을 생성할 수 없도록 함
    {

        if (Arrangement != null) //빨간 블록이 맵에 남아있는 경우
            return (Arrangement.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.r == Color.red.r
                && Arrangement.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.g == Color.red.g
                 && Arrangement.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color.b == Color.red.b);
        else
            return false;
    }

    void RotateBlock() //블록 회전
    {
        //CheckNum 선택된 블록의 번호
        //ClickNum 회전 횟수
        if (CheckNum != -1 && CheckNum > 0 && ClickNum < 3) // 사각형 블록은 제외
        {
            Destroy(Arrangement); //새로운 블럭을 생성하기 위해 현재 블럭 삭제
            if ((CheckNum == 1 || CheckNum == 4 || CheckNum == 5) && ClickNum > 0) // 회전이 한 번만 있는 도형 인덱스 범위 제한
            {
                Arrangement = Instantiate(BlockPrefab[CheckNum]) as GameObject;
                ClickNum = 0;
            }
            else
            {
                Arrangement = Instantiate(BlockPrefab_R[CheckNum, ClickNum]) as GameObject;
                ClickNum++;
            }
        }
        else if (CheckNum != -1 && CheckNum > 0 && ClickNum >= 3)
        {   //인덱스의 범위를 넘어가면 초기 블럭 생성           
            Destroy(Arrangement);

            Arrangement = Instantiate(BlockPrefab[CheckNum]) as GameObject;
            ClickNum = 0;
        }

        // 이전 블럭과 생성되는 블록의 위치가 같으면 이전 블록을 인식 못함
        //임시로 랜덤 값을 넣어서 뺴줌 수정 예정)
        float RandomX = UnityEngine.Random.Range(1.5f, 2.5f);
        float RandomY = UnityEngine.Random.Range(0.01f, 0.09f);

        Arrangement.transform.position = new Vector3(Camera.main.transform.position.x - RandomX, Camera.main.transform.position.y - RandomY, 0);
        Arrangement.transform.SetParent(BP.transform);
    }

    void FirstLayer() //  드래그 전 레이어 값은 가장 높아야함
    {
        if (Arrangement != null && !CheckDrag)
        {
            for (int i = 0; i < Arrangement.transform.childCount; i++) //임시로 높은 값 넣어둠 레이어 순위를 받도록 바꿀 예정
                Arrangement.transform.GetChild(i).transform.GetComponent<SpriteRenderer>().sortingOrder = 3000;
        }
    }




    // Update is called once per fram  
    void Update()
    {
        if (SelectBlock.Check != -1 && Arrangement == null) // 현재 블럭 인스턴스가 비여있고 선택된 블럭이 있다면
        {
            // 이전 블럭과 생성되는 블록의 위치가 같으면 이전 블록을 인식 못함
            //임시로 랜덤 값을 넣어서 뺴줌 수정 예정
            float RandomX = UnityEngine.Random.Range(2.0f, 3.0f);
            float RandomY = UnityEngine.Random.Range(0.01f, 0.09f);

            if (!confirmexcept()) // 예외 사항을 컨펌받아 아직 예외가 남아있다면 프리팹을 생성하지 않는다.             
            {
                Arrangement = Instantiate(BlockPrefab[SelectBlock.Check]) as GameObject;
                BlockColor = Arrangement.transform.GetChild(0).transform.GetComponent<SpriteRenderer>().color; //블록 기존컬러 저장

                // 이전 블록과 겹치지 않기위해 랜덤값을 빼줌 (임시)
                Arrangement.transform.position = new Vector3(Camera.main.transform.position.x - RandomX, Camera.main.transform.position.y - RandomY, 0);

                Arrangement.transform.SetParent(BP.transform); // BlockPrefab의 부모로 BP를 설정
                CheckNum = SelectBlock.Check; //SelectBlock에서 몇번 블록이 클릭되었는지 확인
            }
            SelectBlock.Check = -1; // 초기 값으로 돌려놓음
        }

        FirstLayer(); // 드래그 전에 블록의 레이어 값은 가장 높아야 함.

        if (Input.GetKeyDown(KeyCode.R) && Arrangement != null)// 블록 회전
            RotateBlock();


        if (Arrangement != null && CheckDrag)
        { // 프리팹이 있는 경우 계속 정렬과 검사를 반복[드래그하기 시작 전까지 멈춤]    
            Arrange_Obj();
            Exception();
        }

        if (Arrangement != null) // 아직 설치중인 블럭은 투명색으로
            BlockBeingDrag();
    }
}