using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridSnaper : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_targetSprite = null;

    void LateUpdate()
    {
        float x = transform.position.x * 2;
        float y = transform.position.y * 4;

        if (Mathf.Ceil(x) == 1 && Mathf.Floor(y) == 4)
        {
            Debug.Log("eee");
        }

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
        if(m_targetSprite != null)
        {
            m_targetSprite.sortingOrder = -(int)y;
        }
    }
}
