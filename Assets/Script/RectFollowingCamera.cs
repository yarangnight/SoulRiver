using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class RectFollowingCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;

    private BoxCollider2D m_Collider;

    private void Awake()
    {
        m_Collider = gameObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(!(m_Collider.bounds.min.x < m_Player.transform.position.x &&//바운드 내에 플레이어가 존재하지 않으면
            m_Player.transform.position.x < m_Collider.bounds.max.x &&
            m_Collider.bounds.min.y < m_Player.transform.position.y &&
            m_Player.transform.position.y < m_Collider.bounds.max.y ))
        {
            transform.position = Vector3.Lerp(
                new Vector3(transform.position.x, transform.position.y, transform.position.z),
                new Vector3(m_Player.transform.position.x, m_Player.transform.position.y, transform.position.z), Time.deltaTime * 0.4f);
        }
    }
}
