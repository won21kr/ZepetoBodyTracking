using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class AvatarBodyTransform : MonoBehaviour
{
    //bind bone class
    public SnowAvatarBone AvatarBone = new SnowAvatarBone();
    public GameObject RootBone;

    //detect last update time
    private float lastUpdateTm;
    //detect update interval
    private float updateInterval;
    //last face value update time
    private float lastFaceUpdateTm;

    private void Start()
    {
        RefreshAvatarBone();   

        lastFaceUpdateTm = lastUpdateTm = Time.realtimeSinceStartup;
        updateInterval = 1.0f;
    }

    //refresh avatar bone bind,and init data
    public void RefreshAvatar()
    {
        RefreshAvatarBone();
        lastFaceUpdateTm = lastUpdateTm = Time.realtimeSinceStartup;
        updateInterval = 1.0f;

    }


    int poseIdx = 0;
    float deltaTime = 0.0f;
    private void Update()
    {
        if (AvatarBone.pelvis == null)
        {
            RefreshAvatar();
            return;
        }
        
        deltaTime += Time.deltaTime;
        if(deltaTime > 1.0f) // 0.5sec
        {
            //int[] poseArray = {0, 2, 4, 6, 8, 18};
            // int[] poseArray = {0, 2, 4, 6, 8, 18, 20, 22, 24};
            int[] poseArray = {0, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26};
                    
            int pose = poseArray[poseIdx];
            
            poseIdx++;
            if(poseIdx >= poseArray.Length)
                poseIdx = 0;
            
            loadCustomPose("pose/"+pose +".json");
//            Debug.Log(">>pose: " + "pose/"+pose +".json, " + deltaTime);

            deltaTime = 0.0f;

            float currentTm = Time.realtimeSinceStartup;
            updateInterval = currentTm - lastUpdateTm;
            lastUpdateTm = currentTm;
        }
    }

    //refresh avatar bone
    private void RefreshAvatarBone()
    {
        AvatarBone.RefreshAvatarBone(GameObject.Find("avatar_root"));
    }
    //load tpose json for current avatar 

    public void loadCustomPose(string path)
    {   
        string tposeJson = Path.Combine(Application.streamingAssetsPath, path);
        byte[] bytes = null;
#if UNITY_ANDROID && !UNITY_EDITOR
        using (WWW www = new WWW(tposeJson))
        {
            while (!www.isDone) { }
            bytes = www.bytes;
        }
#else
        bytes = File.ReadAllBytes(tposeJson);
#endif
        if(bytes==null) 
        {
            Debug.LogError("pose not exist : " + path);
            return;
        }

        string jsonStr = Encoding.Default.GetString(bytes);
        if(jsonStr!=null)
            AvatarBone.updateFromTransformModel(JsonUtility.FromJson<TransformJsonModel>(jsonStr), false);
    }

    private void OnDestroy()
    {
        // loadInitPose(LoadInitPoseWithHead);
    }
    //for test file
    private int frame = 0;
    //for test
    public void poseSnapToJson()
    {
        WriteTestJsonFile();
        Debug.Log(">>saved called" );
    }

    private void WriteTestJsonFile()
    {
        string dir = "./input/";
        if (Application.platform == RuntimePlatform.Android)
        {
            dir = "/sdcard/input/";
        }
        string json = AvatarBone.toJson();
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        File.WriteAllText(dir + (frame++) + ".json", json);

        Debug.Log(">>saved : " +dir + (frame++) + ".json");
    }
    //for test
    private void ReadTestJsonFileToUpdate()
    {
        string dir = "./input/";
        if (Application.platform == RuntimePlatform.Android)
        {
            dir = "/sdcard/input/";
        }
        if (Directory.Exists(dir))
        {
            string tmp = "";
            if (frame < 10)
            {
                tmp = "000";
            }
            else if (frame >= 10 && frame < 100)
            {
                tmp = "00";
            }
            else if (frame >= 100 && frame < 1000)
            {
                tmp = "0";
            }
            string file = dir + tmp + (frame++) + ".json";
            if (File.Exists(file))
            {
                string json = File.ReadAllText(file);
                AvatarBone.updateFromTransformModel(JsonUtility.FromJson<TransformJsonModel>(json), true);
            }
            else
            {
                frame = 0;
            }
        }
    }
}
