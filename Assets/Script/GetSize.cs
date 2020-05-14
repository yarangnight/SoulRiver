using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSize : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 GetSpriteSize()
    {
        Vector3 worldSize = Vector3.zero;
        if(this.GetComponent<SpriteRenderer>())
        {
            Vector2 spriteSize = this.GetComponent<SpriteRenderer>().sprite.rect.size;
            Vector2 localSpriteSize = spriteSize / this.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
            worldSize = localSpriteSize;
            worldSize.x *= this.transform.lossyScale.x;
            worldSize *= this.transform.lossyScale.y;
        }
        else { Debug.Log("SpriteRenderer Null"); }

        return worldSize;
    }
}
