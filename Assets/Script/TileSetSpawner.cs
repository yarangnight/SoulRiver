using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSetSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_TileSetArr;//인스펙터에서 사용하기 위한 2차원 배열


    public void SpawnTileSet(int num)
    {
        Instantiate(m_TileSetArr[num]);
    }
}
