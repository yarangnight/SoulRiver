using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNavigator : Singleton<TileNavigator>//타일 네비게이션, 시작 타일에서 목적 타일까지 지나쳐야 하는 타일을 계산
{
    private List<Tile> m_Tiles = new List<Tile>();//전체 타일을 전부 저장하는 리스트

    public void RegisterTile(Tile newTile)
    {
        newTile.m_Id = m_Tiles.Count;
        m_Tiles.Add(newTile);
    }

    public Tile[] GetPath(Tile start, Tile dest)
    {
        Tile[] tileParents = new Tile[m_Tiles.Count];//인덱스 번호의 아이디의 타일의 부모 타일을 저장
        bool[] tileVisited = new bool[m_Tiles.Count];//인덱스 번호의 아이디의 타일의 방문 여부를 저장
        Queue<Tile> tileQueue = new Queue<Tile>();//BFS에 사용

        tileQueue.Enqueue(start);//시작 노드를 인큐
        tileParents[start.m_Id] = start;//시작 노드는 부모 노드가 자기 자신
        tileVisited[start.m_Id] = true;//시작 노드를 방문 했음으로 표시

        while (true)
        {
            Tile temp = tileQueue.Dequeue();//디큐
            if (temp == dest)//목적지인지 체크 맞으면 break;
            {
                break;
            }

            foreach (var v in temp.m_CloseTiles)
            {
                if (!tileVisited[v.m_Id])
                {
                    tileQueue.Enqueue(v);
                    tileVisited[v.m_Id] = true;
                    tileParents[v.m_Id] = temp;
                }
            }
        }

        if(tileVisited[dest.m_Id] == false)
        {
            return null;
        }


        int pathLength = 0;
        List<Tile> resultPath = new List<Tile>();
        Tile temp2 = dest;
        for (int i = 0; i < m_Tiles.Count; ++i)
        {
            resultPath.Add(temp2);
            if(temp2 == tileParents[temp2.m_Id])
            {
                pathLength = i;
                break;
            }
            temp2 = tileParents[temp2.m_Id];
        }
        resultPath.Reverse();

        return resultPath.ToArray();
    }
}
