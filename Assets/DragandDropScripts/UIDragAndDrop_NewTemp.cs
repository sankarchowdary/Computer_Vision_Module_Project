using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Linq;
using TMPro;


public class UIDragAndDrop_NewTemp : MonoBehaviour
{
    public List<GameObject> TargetObj, FinalObj, Arrow,dummyObjs;
    public List<Vector3> FinalPos,FinalRot;
    public GameObject ObjTODrag,MainObj;
    public Vector3 screenSpace, offSet, curScreenSpace, curPosition;
    public Vector3 Init_Pos,Final_Pos,Init_Rot,Final_Rot;
    public bool dragging,IsLiquid, IsAR;
    public int CurrObj_Cnt,CurrentStepCount;
    public float MovingTime;
    public Vector3 DragPositionValues;
    public int temp_int;
    public String Label_name;
    public GameObject prefab_label;
    public Sprite correct,wrong;
    public bool needLastSibling;
    RaycastHit hit;
    public GameObject cube, Sphere;
    public Material cubemat, speheremat;
    public static UIDragAndDrop_NewTemp Instance;
    public Vector3 ObjectRotation;

    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Invoke("CallStart",0.02f);
        OnMouseDown();
      
    }

    public void CallStart()
    {
        //MainObj = GameObject.Find("MainObject");


       // TargetObj = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name.Contains(NameChange("_UI", "_Target"))).ToList();
       // FinalObj = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name.Contains(NameChange("_UI", "_Final"))).ToList();
     //   Arrow = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name.Contains(NameChange("_UI", "_Arrow"))).ToList();
        
        PlayerPrefs.SetInt("CurrentStepCount", 0);
        
      //  TargetObj = TargetObj.OrderBy(x => x.name).ToList();
      //  FinalObj = FinalObj.OrderBy(x => x.name).ToList();
     //   Arrow = Arrow.OrderBy(x => x.name).ToList();

      //  CurrObj_Cnt = 0;
        
        for (int i = 0; i < TargetObj.Count; i++)
        {
           // FinalObj[i].SetActive(false);
           // FinalObj[i].name = FinalObj[i].name + i.ToString();
            TargetObj[i].SetActive(false);
           // TargetObj[i].name = TargetObj[i].name + i.ToString();
           // TargetObj[i].GetComponent<BoxCollider>().enabled = true;
           
           // iTween.MoveAdd(Arrow[i], iTween.Hash("y", 0.15f, "time", 0.5f, "islocal", true, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.easeInOutSine));
           // Arrow[i].SetActive(false);
           // transform.GetChild(1).GetChild(1).GetComponent<Image>().enabled = false;
            //  FinalPos.Add(FinalObj[i].transform.localPosition);
            //  FinalRot.Add(FinalObj[i].transform.localEulerAngles);

           
        }
        //TargetObj[0].tag = "aaa";
        //  Final_Pos = FinalObj[CurrObj_Cnt].transform.localPosition;
        //  Final_Rot = FinalObj[CurrObj_Cnt].transform.localEulerAngles;

        AddListener(EventTriggerType.PointerDown, MouseDown2, this.gameObject);
        AddListener(EventTriggerType.BeginDrag, OnMouseDown, this.gameObject);
        AddListener(EventTriggerType.EndDrag, OnMouseUp, this.gameObject);
        AddListener(EventTriggerType.PointerUp, OnMouseUp, this.gameObject);

        /*if (GameObject.Find("Target"))
        {
            CamOrbit = GameObject.Find("Target").GetComponent<orbit>();
            IsAR = false;
        }
        else
        {
            ArOrbit = MainObj.transform.parent.gameObject.GetComponent<AROrbitControls>();
            IsAR = true;
        }*/
    }

    public void Restart()
    {
        
        PlayerPrefs.SetInt("CurrentStepCount", 0);
        
        CurrObj_Cnt = 0;
      //  transform.GetChild(1).GetChild(1).GetComponent<Image>().enabled = false;
        //ObjTODrag = null;
        for (int i = 0; i < TargetObj.Count; i++)
        {
            if(i<FinalObj.Count)
            Destroy(FinalObj[i]);
           // FinalObj[i].SetActive(false);
            TargetObj[i].SetActive(false);
            TargetObj[i].GetComponent<BoxCollider>().enabled = true;
            if (TargetObj[i].name.Contains("_done"))
            {
                string name = TargetObj[i].name.Remove(TargetObj[i].name.Length - 5);
                TargetObj[i].name=name;
            }
           // iTween.MoveAdd(Arrow[i], iTween.Hash("y", 0.15f, "time", 0.5f, "islocal", true, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.easeInOutSine));
          //  Arrow[i].SetActive(false);
          //  FinalObj[i].transform.localPosition =FinalPos[i];
          //  FinalObj[i].transform.localEulerAngles = FinalRot[i];
        }
        FinalObj = new List<GameObject>();
        GetComponent<Image>().raycastTarget = true;
    }


    public string NameChange(string ToRemove,string ToAdd)
    {
        string Name = "";
        Name = transform.name.Replace(ToRemove, ToAdd); 


        return Name;

    }
    public void MouseDown2()
    {
        //dragging = true;
        //DisableOrbit();
    }
    public void OnMouseDown()
    {
        dummyObjs[0].SetActive(true);
        //   cube.GetComponent<MeshRenderer>().enabled = 

        // DisableOrbit();
        if (!ObjTODrag)
            ObjTODrag = Instantiate(prefab_label, new Vector3(ObjTODrag.transform.localPosition.x, ObjTODrag.transform.localPosition.y, ObjTODrag.transform.localPosition.z),Quaternion.identity) as GameObject;
        //  print("objtodrag.........."+ObjTODrag);
        //ObjTODrag.GetComponentInChildren<TextMeshPro>().text = Label_name;
        ObjTODrag.gameObject.SetActive(true);

        ObjTODrag.transform.localEulerAngles = ObjectRotation;

        // ObjTODrag = GameObject.Find("MainCamera").transform.GetChild(1).gameObject;

        screenSpace = Camera.main.WorldToScreenPoint(ObjTODrag.transform.localPosition);
       // ObjTODrag = GameObject .Find("MainCamera").transform.GetChild(0).gameObject;


        curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace);

        Init_Pos = curPosition;
        //Init_Pos = this.gameObject.transform.position;
        ObjTODrag.transform.localPosition =new Vector3( curPosition.x, curPosition.y, curPosition.z-50);
      

        for (int i = 0; i < TargetObj.Count; i++)
        {

            if (TargetObj[i].name.Contains("_done"))
            {

              TargetObj[i].SetActive(false);
                dummyObjs[i].SetActive(false);

            }
            else
            {
              //  TargetObj[i].SetActive(true);
               // dummyObjs[i].SetActive(true);

            }

        }
        //   Arrow[CurrObj_Cnt].SetActive(true);

        dragging = true;

    }

    void Update()
    {
        if (dragging)
        {
            print("drag");
            curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
            curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace);
            // curPosition = new Vector3 (curPosition.x, Mathf.Clamp (curPosition.y, intialGroundTouchvalue+GroundTouchValue, 100f), curPosition.z);
            if (ObjTODrag)
            {
                ObjTODrag.transform.localPosition = new Vector3(curPosition.x, curPosition.y, curPosition.z);
                Debug.Log(ObjTODrag.transform.localScale);

                // ObjTODrag.GetComponentInChildren<TextMeshPro>().text = Label_name;
            }
            

        }
    }

    public void OnMouseUp()
    {
        dummyObjs[0].SetActive(false);
        //EnableOrbit();
        print("dragfalse");
        if (!ObjTODrag || !dragging)
            return;

        //iTween.RotateTo(ObjTODrag, iTween.Hash("x", Init_Rot.x, "y", Init_Rot.y, "z", Init_Rot.z, "time", 0.5f, "islocal", true, "easetype", iTween.EaseType.linear));
      //  if (PlayerPrefs.GetInt("CurrentStepCount") == CurrentStepCount)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < TargetObj.Count; i++)                   
                {
                    Debug.Log(hit.collider.name + " == " + TargetObj[i].name);

                    if (Physics.Raycast(ray, out hit))
                    {

                       


                        if (hit.collider.name == TargetObj[i].name)
                        {
                            prefab_label.GetComponent<BoxCollider>().enabled = false;
                            prefab_label.SetActive(false);

                            TargetObj[0].SetActive(true);
                            //   TargetObj[0].GetComponent<MeshRenderer>().enabled = true;
                            if (ObjTODrag)
                                Destroy(ObjTODrag);
                            //   ObjTODrag.SetActive(false);
                            dummyObjs[i].SetActive(false);

                            hit.collider.gameObject.name = hit.collider.gameObject.name + "_done";
                            break;
                        }
                    }
                    else
                    {
                        //if (ObjTODrag)
                        //    Destroy(ObjTODrag);

                    }
                }
                

            }
            else if (dragging && ObjTODrag)
            {
                //MainObj.SendMessage("WrongPlacement");
            //    iTween.MoveTo(ObjTODrag, iTween.Hash("x", Init_Pos.x, "y", Init_Pos.y, "z", Init_Pos.z, "time", MovingTime, "oncomplete", "ResetToInitPos", "oncompletetarget", this.gameObject, "islocal", true, "easetype", iTween.EaseType.linear));
                
            }
          
        }
        //else
        //{
        //    //MainObj.SendMessage("WrongDragDrop");
        //    iTween.MoveTo(ObjTODrag, iTween.Hash("x", Init_Pos.x, "y", Init_Pos.y, "z", Init_Pos.z, "time", MovingTime, "oncomplete", "ResetToInitPos", "oncompletetarget", this.gameObject, "islocal", true, "easetype", iTween.EaseType.linear)); 
        //}

        dragging = false;
        
        //TargetObj[CurrObj_Cnt].SetActive(false);
    }
    
    public void UpdateNextStep()
    {
        for (int i = 0; i < TargetObj.Count; i++)
        {
            //  Arrow[i].SetActive(false);
            TargetObj[i].SetActive(false);
        }

        Destroy(ObjTODrag);

       

        //TargetObj[temp_int].GetComponent<BoxCollider>().enabled = false;
        //TargetObj[temp_int].name = "done";
        //FinalObj[temp_int].SetActive(true);

        //this.gameObject.SetActive(false);
          this.transform.SetAsLastSibling();// SetActive(false);
        //  transform.GetChild(1).GetChild(1).GetComponent<Image>().enabled = true;





        //if (CurrObj_Cnt < FinalObj.Count-1)
        //{

        //    CurrObj_Cnt++;
        //}

        //  GetComponent<Image>().raycastTarget = false;


        ObjTODrag = null;
        // MainObj.SendMessage("PlaySequenceStep");
        //  print("update to next step.....");
        //  print("update to next step.....");

        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        prefab_label.GetComponent<BoxCollider>().enabled = false;
        prefab_label.SetActive(false);

        TargetObj[0].SetActive(true);
        //   TargetObj[0].GetComponent<MeshRenderer>().enabled = true;
        if (ObjTODrag)
            Destroy(ObjTODrag);
        //   ObjTODrag.SetActive(false);
       dummyObjs[0].SetActive(false);
        dummyObjs[0].GetComponent<BoxCollider>().enabled = false;

      //  hit.collider.gameObject.name = hit.collider.gameObject.name + "_done";
      //  break;
    }
    public void ResetToInitPos()
    {

        Destroy(ObjTODrag);
        for (int i = 0; i < TargetObj.Count; i++)
        {
           // Arrow[i].SetActive(false);
            TargetObj[i].SetActive(false);
        }
    }

    public void EnableOrbit()
    {

        if (!IsAR)
        {
           
        }
        else
        {
           
        }
    }

    public void DisableOrbit()
    {

        if (!IsAR)
        {
            
        }
        else
        {
         
        }
    }

    public void AddListener(EventTriggerType eventType, Action MethodToCall, GameObject TriggerObjToAdd)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback.AddListener((data) => MethodToCall());
        TriggerObjToAdd.GetComponent<EventTrigger>().triggers.Add(entry);
    }
}
