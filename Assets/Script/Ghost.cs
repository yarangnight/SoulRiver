using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ghost : MonoBehaviour
{

    private UnityEvent m_onGainEvent = new UnityEvent();

    public UnityEvent m_OnGainEvent { get => m_onGainEvent; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if(collision.gameObject.GetComponent<PlayerController>() != null)
        {
            m_OnGainEvent.Invoke();
            Destroy(gameObject);
        }
    }
}
