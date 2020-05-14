using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class SelectBlock_UI : MonoBehaviour//, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /* public void OnEndDrag(PointerEventData eventData) { }
     public void OnBeginDrag(PointerEventData eventData) { }
     public void OnDrag(PointerEventData eventData) { }*/

    //UI SelectBlock 마우스 이벤트 0~6 생성값을 BlockGenerator에서 확인함
    public int Check;
    public void OnClickBlock1Btn() { Check = 0; } 
    public void OnClickBlock2Btn() { Check = 1; }
    public void OnClickBlock3Btn() { Check = 2; }
    public void OnClickBlock4Btn() { Check = 3; }
    public void OnClickBlock5Btn() { Check = 4; }
    public void OnClickBlock6Btn() { Check = 5; }
    public void OnClickBlock7Btn() { Check = 6; }
    // Start is called before the first frame update
    void Start() {Check = -1;}
}
