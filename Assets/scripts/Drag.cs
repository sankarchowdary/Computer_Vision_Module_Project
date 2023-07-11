using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{ 

public GameObject thisImage, prefab_label, ObjTODrag;
public Vector3 startPosition, DragPositionValues;
public Vector3 screenSpace, offSet, curScreenSpace, curPosition, Init_Pos;
public bool dragging;


    void Start()
   {
        dragging = true;
   }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragging)
        {
           
            ObjTODrag = Instantiate(prefab_label, new Vector3(ObjTODrag.transform.localPosition.x, ObjTODrag.transform.localPosition.y, ObjTODrag.transform.localPosition.z), Quaternion.identity);
            
            ObjTODrag.SetActive(true);
           
            dragging = false;
            thisImage.SetActive(false);
         //   UIDragAndDrop_NewTemp.Instance.dragging= true;
        }


    }
    void Update()
    {
        


    }

    public void OnDrag(PointerEventData eventData)
    {

       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
               



    }
    public void OnMouseDown()
    {
        print("hi");
        if (!ObjTODrag)
            ObjTODrag = Instantiate(prefab_label, DragPositionValues = prefab_label.transform.localPosition, Quaternion.identity) as GameObject;
        //  print("objtodrag.........."+ObjTODrag);
        //ObjTODrag.GetComponentInChildren<TextMeshPro>().text = Label_name;
        ObjTODrag.gameObject.SetActive(true);

        screenSpace = Camera.main.WorldToScreenPoint(ObjTODrag.transform.localPosition);



        curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace);

        Init_Pos = curPosition;
        //Init_Pos = this.gameObject.transform.position;
        ObjTODrag.transform.localPosition = new Vector3(curPosition.x, curPosition.y, curPosition.z - 5);
        return;
    }

}
    