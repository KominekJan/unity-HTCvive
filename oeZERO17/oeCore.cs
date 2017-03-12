﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// oeObjClass - octopusengine 
// 0.19 (2017/01 firts public)
// 0.20 (2017/02 Hackathon)
// 0.30 (2017/03 Demo03 oeCore)
// 0.40 (2017/03 tag oeSave)
//---------------------------
// todo - better Tag obj?
// test web-server - ZERO.json upload.. 

public class oeCore : MonoBehaviour
{
    //public GameObject[] oeBasicTag;
    public bool ListAllObjects = false;
    public bool ListAllObjectsOe = false;
    public bool saveTest = false;
    public bool loadInfo = false;
    public bool saveInfo = false;
    public int saveTime = 1000;

    GameObject goTag;
    int iObj = 0;
    int iTag = 0; //index for tag

    string textFile;
    int cntBTC;
    int cntLED;
    int cntU;
    public string data17File;
    public string data17FileDict;
    public string dataString;
    string[] dataLines16; //
    string lineOfText;

    // 2016: ///= new string[255];// 
    //string[] oeObjName = { "oeObjZERO" }; //board-foto/light/cube-wall-.../... 
    List<GameObject> oeObjList = new List<GameObject>();
    string[] oeObjName; // = new string[255]; 
                        // = { "oeObjZERO", "oeObjINFO1", "oeObjINFO2", "obj2", "obj3", "obj4", "obj5", "obj6", "obj7", "obj8", "obj9", "cubeYel1", "cubeYel2", "cubeYel2", "cubeYel4", "cubeYel5", "cubeYel6", "cubeYel7", "cubeYel8", "cubeYel9", "light1", "light2", "light3", "lightR", "lightG", "lightB" }; //board-foto/light/cube-wall-.../... 
    Dictionary<int, string> oeObjNameDict = new Dictionary<int, string>(); // index > name - ala asoc

    GameObject[] oeObjSave; /// = new GameObject[255]; // 2017: oeObjSave.name = oeObjName
    public GameObject goObj0;
    public GameObject[] goObj;// = new GameObject[255]; //list.lenght?
    int indexObj;

    public Transform originalValue;
    private LineRenderer line;
    private Color lineColor = Color.red;
    private int lineWidth = 1;
    public Renderer rend1;
    TextMesh textObject0;

    //================================================================================================
    //start-inicializace
    void Start()
    {
        Debug.Log("--- oeCore.Start ---"); //semi-constructor

        if (ListAllObjects)
        {
            GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
            foreach (object go in allObjects)
            {
                Debug.Log(iObj + " > " + go.ToString());
                iObj++;
            }
        }

        Debug.Log("-------- TAG -------- list and dict:"); //https://docs.unity3d.com/ScriptReference/GameObject.FindGameObjectsWithTag.html
        oeObjSave = GameObject.FindGameObjectsWithTag("oeSave");

        indexObj = 0;
        //https://msdn.microsoft.com/cs-cz/library/aa288453(v=vs.71).aspx
        foreach (GameObject goTag in oeObjSave)
        {
            //Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation);
            Debug.Log("oeSave > " + indexObj + " > " + goTag.name);
            oeObjList.Add(goTag);
            oeObjNameDict.Add(indexObj, goTag.name);
            
            //oeObjName[indexObj++] = goTag.name;
            //oeObjName[indexObj] = new string goTag.name;

            //oeObjName[indexObj] = goTag.name;

            //goObj[indexObj] = goTag; //GameObject.Find(value);
            indexObj++;
        }
        int numObj = indexObj; 

        Debug.Log("-------- LIST --------");
        //goObj = new GameObject[oeObjName.Length];
        //goObj = new GameObject[oeObjName.Length];
        string[] oeObjName = new string[numObj];

        indexObj = 0;

        foreach (GameObject goItem in oeObjList)
        {
            //Instantiate(respawnPrefab, respawn.transform.position, respawn.transform.rotation);
            ///Debug.Log("oeList >>> " + goItem.name);
            //goObj[indexObj] = goItem;
            oeObjName[indexObj] = goItem.name;
            indexObj++;
        }

        if (ListAllObjectsOe)
        {
            string oeNameS = ":.TAG.:";
            foreach (string oeName in oeObjName)
            {
                oeNameS += ", " + oeName;
            }
            Debug.Log("oeObjName >> " + oeNameS);
        }

        //Scene scene = SceneManager.GetActiveScene();
        //scene.name; // name of scene
        string sName = Application.loadedLevelName;
        data17File = Application.dataPath + "/Resources/" + sName + ".json";
        data17FileDict = Application.dataPath + "/Resources/" + sName + "-d.json";

        Debug.Log("----------------------oeCore17.Start(End)");       

       
        //Debug.Log(data16File);        
        
        goObj = new GameObject[oeObjName.Length];
        
        indexObj = 0;
        foreach (string value in oeObjName)
        {
            goObj[indexObj] = GameObject.Find(value);
            indexObj++;
        }        

        //dataLines16 = new string[255];
        goObj0 = GameObject.Find("oeObjZERO");
        textObject0 = GameObject.Find("text0").GetComponent<TextMesh>();
        rend1 = goObj0.GetComponent<Renderer>();

        /*
        var jsonD = System.IO.File.ReadAllText(@"d:\unity-temp-data/file2.json");
        Debug.Log("---------------- JSON-File2 " + jsonD);
        //textObject.text = json;
        */
        oeLoad();

        oeSaveTest();


    }
    //------------------------------------------------/start----------------------------------------

    //timer cca60 FPS
    void Update()
    {
        cntU++;
        int kazdych = saveTime;

        if (cntU % kazdych == 0)
        {
            rend1.material.color = Color.red;
            oeSave();
            ///oeSaveDict();
        }

        if ((cntU - 100) % kazdych * 5 == 0)
        {
            rend1.material.color = Color.green;
            oeLoad();
        }
    }
    //------------------------------------------------------------------------------------------------

    private void oeLoad()
    {
        //Debug.Log("loadData():");
        var file = new System.IO.StreamReader(data17File, System.Text.Encoding.UTF8, true, 128);

        dataString = file.ReadToEnd();
        file.Close();

        //Debug.Log("dataString: " + dataString);
        oeObjWrapper wrapperLoad = JsonUtility.FromJson<oeObjWrapper>(dataString);
        //Debug.Log("wrapper, fst object: " + wrapperLoad.oeObjects[0].oT);

        int index = 0;
        foreach (var obj in wrapperLoad.oeObjects)
        {
            if (loadInfo) Debug.Log("oeLoad " + index + " -> " + obj.oN);
            //obj.setPropertiesToGameObject(goObj[index]); //ok
            obj.setPropertiesToGameObject(goObj[index]);
            //oeObjNameDict[index]

            index++;
        }
    }

    //-----------------------------------------------
    private void oeSaveTest()
    {
        if (saveTest)           
        {
            Debug.Log("---------------------- save test------------------------");
            //oeObjClass[] oeObjArray = new oeObjClass[goObj.Length];
            int index = 0;
            foreach (var goName in goObj)
            {
                //Debug.Log("save: " + index + " > " + go.name); //go nemá name? err
                //Debug.Log("save: " + index + " > " + goName.name);
                Debug.Log("save: " + index + " > " + oeObjNameDict[index]);

                //oeObjArray[index] = new oeObjClass(go, go.name, index);
                index++;
            }
        }
    }


    private void oeSave()
    {
        //Debug.Log("---------------------- save ------------------------");
        oeObjClass[] oeObjArray = new oeObjClass[goObj.Length];
        int index = 0;
        foreach (var go in goObj)
        {
            if (saveInfo) Debug.Log("oeSave: " + index + " > " + oeObjNameDict[index]); //Debug.Log("save: " + index + " > " + oeObjName[index]);
            oeObjArray[index] = new oeObjClass(go, oeObjNameDict[index], index);
            index++;
        }
        oeObjWrapper wrapperSave = new oeObjWrapper();
        wrapperSave.oeObjects = oeObjArray;
        string json = JsonUtility.ToJson(wrapperSave);
        System.IO.File.WriteAllText(data17File, json);
    }

    private void oeSaveDict()
    {
        //oeObjClass[] oeObjArray = new oeObjClass[goObj.Length];
        Dictionary<string, oeObjClass> oeObjDict =  new Dictionary<string, oeObjClass>();
        int index = 0;
        foreach (var go in goObj)
        {
            //oeObjArray[index] = new oeObjClass(go, index);
            index++;
            oeObjDict.Add(go.name, new oeObjClass(go, go.name, index));
        }

        oeObjWrapperDict wrapperSaveDict = new oeObjWrapperDict();
        //wrapperSave.oeObjects = oeObjArray;
        //wrapperSaveDict.oeObjectsDict = oeObjDict; ///*** chyba oeObjDict?
        string json = JsonUtility.ToJson(wrapperSaveDict);

        ///Debug.Log(data16File);
        System.IO.File.WriteAllText(data17FileDict, json);
    }   


}
