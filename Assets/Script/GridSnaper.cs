using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridSnaper : MonoBehaviour//정렬은 반드시 업데이트에서 하자 LateUpdate를 실험해 봤는데 trasnform.position이 정렬되기 전 값이 나오게 된다//
{
    [SerializeField]
    private SpriteRenderer m_targetSprite = null;

    [SerializeField]
    private bool m_HaveToSort = false;

    private void Awake()
    {
        LateUpdate();
    }

    void LateUpdate()
    {
        float x = transform.position.x * 2;
        float y = transform.position.y * 4;

        if ((int)(Mathf.Floor(x) + Mathf.Floor(y)) % 2 == 0)//xy의 합이 짝수
        {
            if (((x - Mathf.Floor(x)) + (y - Mathf.Floor(y))) >= 1)
            {
                x = Mathf.Ceil(x);
                y = Mathf.Ceil(y);
                if (x == 1 && y == 4)
                {
                    Debug.Log("eee");
                }
            }
            else
            {
                x = Mathf.Floor(x);
                y = Mathf.Floor(y);
                if (x == 1 && y == 4)
                {
                    Debug.Log("eee");
                }
            }
        }
        else//xy의 합이 홀수
        {
            if (((Mathf.Ceil(x) - x) + (y - Mathf.Floor(y))) >= 1)
            {
                x = Mathf.Floor(x);
                y = Mathf.Ceil(y);
                if (x == 1 && y == 4)
                {
                    Debug.Log("eee");
                }
            }
            else
            {
                x = Mathf.Ceil(x);
                y = Mathf.Floor(y);
                if ((int)(x + y) % 2 != 0)
                {
                    x -= 1;
                }

            }
        }

        transform.position = new Vector3(x / 2, y / 4, transform.position.z);
        if(m_targetSprite != null && m_HaveToSort)
        {
            m_targetSprite.sortingOrder = -(int)y;
        }
    }
}
