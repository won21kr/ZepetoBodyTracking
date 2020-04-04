using System.Collections.Generic;
using UnityEngine;

public class SnowAvatarBone
{
    public GameObject avatarObj;
    public List<Transform> bodyList = new List<Transform>();

    public virtual void applyModelToTransform(Transform target, TransformJsonObject transformJsonObject)
    {
        if(target){
            target.localRotation = transformJsonObject.getRotation();
        }            
    }

    public virtual void updateFromTransformModel(TransformJsonModel transformJsonModel, bool includeHead = false)
    {
        if (transformJsonModel != null)
        {
            // var jsonObjects = transformJsonModel.GetCon
            // foreach(TransformJsonObject transform in transformJsonModel) {
            // }
  
            applyModelToTransform(pelvis, transformJsonModel.pelvis);
            applyModelToTransform(waist, transformJsonModel.waist);
            applyModelToTransform(chest, transformJsonModel.chest);
            applyModelToTransform(neck, transformJsonModel.neck);
            applyModelToTransform(head, transformJsonModel.head);
            applyModelToTransform(leftClavicle, transformJsonModel.leftClavicle);
            applyModelToTransform(leftUpperArm, transformJsonModel.leftUpperArm);
            applyModelToTransform(leftForeArm, transformJsonModel.leftForeArm);
            applyModelToTransform(leftHand, transformJsonModel.leftHand);
            applyModelToTransform(leftFinger0, transformJsonModel.leftFinger0);
            applyModelToTransform(leftFinger0_1, transformJsonModel.leftFinger0_1);
            applyModelToTransform(leftFinger0_2, transformJsonModel.leftFinger0_2);
            applyModelToTransform(leftFinger1, transformJsonModel.leftFinger1);
            applyModelToTransform(leftFinger1_1, transformJsonModel.leftFinger1_1);
            applyModelToTransform(leftFinger1_2, transformJsonModel.leftFinger1_2);
            applyModelToTransform(leftFinger2, transformJsonModel.leftFinger2);
            applyModelToTransform(leftFinger2_1, transformJsonModel.leftFinger2_1);
            applyModelToTransform(leftFinger2_2, transformJsonModel.leftFinger2_2);
            applyModelToTransform(leftFinger3, transformJsonModel.leftFinger3);
            applyModelToTransform(leftFinger3_1, transformJsonModel.leftFinger3_1);
            applyModelToTransform(leftFinger3_2, transformJsonModel.leftFinger3_2);
            applyModelToTransform(leftFinger4, transformJsonModel.leftFinger4);
            applyModelToTransform(leftFinger4_1, transformJsonModel.leftFinger4_1);
            applyModelToTransform(leftFinger4_2, transformJsonModel.leftFinger4_2);
            applyModelToTransform(leftThigh, transformJsonModel.leftThigh);
            applyModelToTransform(leftCalf, transformJsonModel.leftCalf);
            applyModelToTransform(leftFoot, transformJsonModel.leftFoot);

            applyModelToTransform(rightClavicle, transformJsonModel.rightClavicle);
            applyModelToTransform(rightUpperArm, transformJsonModel.rightUpperArm);
            applyModelToTransform(rightForeArm, transformJsonModel.rightForeArm);
            applyModelToTransform(rightHand, transformJsonModel.rightHand);
            applyModelToTransform(rightFinger0, transformJsonModel.rightFinger0);
            applyModelToTransform(rightFinger0_1, transformJsonModel.rightFinger0_1);
            applyModelToTransform(rightFinger0_2, transformJsonModel.rightFinger0_2);
            applyModelToTransform(rightFinger1, transformJsonModel.rightFinger1);
            applyModelToTransform(rightFinger1_1, transformJsonModel.rightFinger1_1);
            applyModelToTransform(rightFinger1_2, transformJsonModel.rightFinger1_2);
            applyModelToTransform(rightFinger2, transformJsonModel.rightFinger2);
            applyModelToTransform(rightFinger2_1, transformJsonModel.rightFinger2_1);
            applyModelToTransform(rightFinger2_2, transformJsonModel.rightFinger2_2);
            applyModelToTransform(rightFinger3, transformJsonModel.rightFinger3);
            applyModelToTransform(rightFinger3_1, transformJsonModel.rightFinger3_1);
            applyModelToTransform(rightFinger3_2, transformJsonModel.rightFinger3_2);
            applyModelToTransform(rightFinger4, transformJsonModel.rightFinger4);
            applyModelToTransform(rightFinger4_1, transformJsonModel.rightFinger4_1);
            applyModelToTransform(rightFinger4_2, transformJsonModel.rightFinger4_2);
            applyModelToTransform(rightThigh, transformJsonModel.rightThigh);
            applyModelToTransform(rightCalf, transformJsonModel.rightCalf);
            applyModelToTransform(rightFoot, transformJsonModel.rightFoot);
        }
    }
    
    Dictionary <string, Transform> BodyTransforms = new Dictionary <string, Transform>();
     
    public virtual void FindAvatarBone(GameObject avatarObj)
    {
        var transforms = avatarObj.GetComponentsInChildren<Transform>();

        foreach(KeyValuePair<string, Transform> bodyTransform in BodyTransforms) {
            Debug.Log($"{bodyTransform.Key}, {bodyTransform.Value}");
        } 

        string name = "Bip001 ";
        // this.avatarObj = avatarObj;
        
        BipRoot = getTransform(GameObject.Find("Bip001"));
        LForeTwist = getTransform(GameObject.Find($"{name}L ForeTwist"));
        RForeTwist = getTransform(GameObject.Find($"{name}R ForeTwist"));;

        pelvis = getTransform(GameObject.Find(name + "Pelvis")); // pelvis error

        // waist = getTransform(GameObject.Find(name + "Spine").GetComponent<Transform>();//腰部为spine
        chest = getTransform(GameObject.Find(name + "Spine1")); ;//胸部
        // neck = getTransform(GameObject.Find(name + "Neck").GetComponent<Transform>();
        head = getTransform(GameObject.Find(name + "Head"));
        //左侧
        //左臂上方节点,左锁骨位置
        leftClavicle = getTransform(GameObject.Find(name + "L Clavicle"));
        //左臂上方节点
        leftUpperArm = getTransform(GameObject.Find(name + "L UpperArm"));
        //左臂，前臂节点
        leftForeArm = getTransform(GameObject.Find(name + "L Forearm"));
        //左侧手臂节点
        leftHand = getTransform(GameObject.Find(name + "L Hand"));

        //左侧大拇指
        leftFinger0 = getTransform(GameObject.Find(name + "L Finger0"));
        leftFinger0_1 = getTransform(GameObject.Find(name + "L Finger01"));
        leftFinger0_2 = getTransform(GameObject.Find(name + "L Finger02"));
        //第二个手指
        leftFinger1 = getTransform(GameObject.Find(name + "L Finger1"));
        leftFinger1_1 = getTransform(GameObject.Find(name + "L Finger11"));
        leftFinger1_2 = getTransform(GameObject.Find(name + "L Finger12"));
        //第三个手指
        leftFinger2 = getTransform(GameObject.Find(name + "L Finger2"));
        leftFinger2_1 = getTransform(GameObject.Find(name + "L Finger21"));
        leftFinger2_2 = getTransform(GameObject.Find(name + "L Finger22"));
        //第四个和第五个手指
        leftFinger3 = getTransform(GameObject.Find(name + "L Finger3"));
        leftFinger3_1 = getTransform(GameObject.Find(name + "L Finger31"));
        leftFinger3_2 = getTransform(GameObject.Find(name + "L Finger32"));
        //第五个手指
        leftFinger4 = getTransform(GameObject.Find(name + "L Finger4"));
        leftFinger4_1 = getTransform(GameObject.Find(name + "L Finger41"));
        leftFinger4_2 = getTransform(GameObject.Find(name + "L Finger42"));
        //左侧大腿根节点
        leftThigh = getTransform(GameObject.Find(name + "L Thigh"));
        //左侧小腿节点
        leftCalf = getTransform(GameObject.Find(name + "L Calf"));
        //左脚脚踝节点
        leftFoot = getTransform(GameObject.Find(name + "L Foot"));


        //右侧
        //左臂上方节点,左锁骨位置
        rightClavicle = getTransform(GameObject.Find(name + "R Clavicle"));
        //右臂上方节点
        rightUpperArm = getTransform(GameObject.Find(name + "R UpperArm"));
        //右臂，前臂节点
        rightForeArm = getTransform(GameObject.Find(name + "R Forearm"));
        //右侧手臂节点
        rightHand = getTransform(GameObject.Find(name + "R Hand"));
        //右侧大拇指
        rightFinger0 = getTransform(GameObject.Find(name + "R Finger0"));
        rightFinger0_1 = getTransform(GameObject.Find(name + "R Finger01"));
        rightFinger0_2 = getTransform(GameObject.Find(name + "R Finger02"));
        //第二个手指
        rightFinger1 = getTransform(GameObject.Find(name + "R Finger1"));
        rightFinger1_1 = getTransform(GameObject.Find(name + "R Finger11"));
        rightFinger1_2 = getTransform(GameObject.Find(name + "R Finger12"));
        //第三个手指
        rightFinger2 = getTransform(GameObject.Find(name + "R Finger2"));
        rightFinger2_1 = getTransform(GameObject.Find(name + "R Finger21"));
        rightFinger2_2 = getTransform(GameObject.Find(name + "R Finger22"));
        //第四个和第五个手指
        rightFinger3 = getTransform(GameObject.Find(name + "R Finger3"));
        rightFinger3_1 = getTransform(GameObject.Find(name + "R Finger31"));
        rightFinger3_2 = getTransform(GameObject.Find(name + "R Finger32"));
        //第五个手指
        rightFinger4 = getTransform(GameObject.Find(name + "R Finger4"));
        rightFinger4_1 = getTransform(GameObject.Find(name + "R Finger41"));
        rightFinger4_2 = getTransform(GameObject.Find(name + "R Finger42"));
        //右侧大腿根节点
        rightThigh = getTransform(GameObject.Find(name + "R Thigh"));
        //右侧小腿节点
        rightCalf = getTransform(GameObject.Find(name + "R Calf"));
        //右脚脚踝节点
        rightFoot = getTransform(GameObject.Find(name + "R Foot"));
        
/*        
        //左脚前脚趾节点
        leftToe0 = getTransform(GameObject.Find(name + "L Toe0"));
        leftToe0_1 = getTransform(GameObject.Find(name + "L Toe01"));
        leftToe0_2 = getTransform(GameObject.Find(name + "L Toe02"));

        leftToe1 = getTransform(GameObject.Find(name + "L Toe1"));
        leftToe1_1 = getTransform(GameObject.Find(name + "L Toe11"));
        leftToe1_2 = getTransform(GameObject.Find(name + "L Toe12"));

        leftToe2 = getTransform(GameObject.Find(name + "L Toe2"));
        leftToe2_1 = getTransform(GameObject.Find(name + "L Toe21"));
        leftToe2_2 = getTransform(GameObject.Find(name + "L Toe22"));

        leftToe3 = getTransform(GameObject.Find(name + "L Toe3"));
        leftToe3_1 = getTransform(GameObject.Find(name + "L Toe31"));
        leftToe3_2 = getTransform(GameObject.Find(name + "L Toe31"));

        leftToe4 = getTransform(GameObject.Find(name + "L Toe4"));
        leftToe4_1 = getTransform(GameObject.Find(name + "L Toe41"));
        leftToe4_2 = getTransform(GameObject.Find(name + "L Toe42"));

        //右脚前脚趾节点
        rightToe0 = getTransform(GameObject.Find(name + "R Toe0"));
        rightToe0_1 = getTransform(GameObject.Find(name + "R Toe01"));
        rightToe0_2 = getTransform(GameObject.Find(name + "R Toe02"));

        rightToe1 = getTransform(GameObject.Find(name + "R Toe1"));
        rightToe1_1 = getTransform(GameObject.Find(name + "R Toe11"));
        rightToe1_2 = getTransform(GameObject.Find(name + "R Toe12"));

        rightToe2 = getTransform(GameObject.Find(name + "R Toe2"));
        rightToe2_1 = getTransform(GameObject.Find(name + "R Toe21"));
        rightToe2_2 = getTransform(GameObject.Find(name + "R Toe22"));

        rightToe3 = getTransform(GameObject.Find(name + "R Toe3"));
        rightToe3_1 = getTransform(GameObject.Find(name + "R Toe31"));
        rightToe3_2 = getTransform(GameObject.Find(name + "R Toe31"));

        rightToe4 = getTransform(GameObject.Find(name + "R Toe4"));
        rightToe4_1 = getTransform(GameObject.Find(name + "R Toe41"));
        rightToe4_2 = getTransform(GameObject.Find(name + "R Toe42"));
*/        
        
    }

    public virtual string toJson()
    {
        TransformJsonModel transformJsonModel = new TransformJsonModel();
        transformJsonModel.pelvis = new TransformJsonObject(pelvis);
        transformJsonModel.waist = new TransformJsonObject(waist);
        transformJsonModel.chest = new TransformJsonObject(chest);
        transformJsonModel.neck = new TransformJsonObject(neck);
        transformJsonModel.head = new TransformJsonObject(head);
        //左侧部分
        transformJsonModel.leftClavicle = new TransformJsonObject(leftClavicle);
        transformJsonModel.leftUpperArm = new TransformJsonObject(leftUpperArm);
        transformJsonModel.leftForeArm = new TransformJsonObject(leftForeArm);
        transformJsonModel.leftHand = new TransformJsonObject(leftHand);
        //左侧手指部分
        transformJsonModel.leftFinger0 = new TransformJsonObject(leftFinger0);
        transformJsonModel.leftFinger0_1 = new TransformJsonObject(leftFinger0_1);
        transformJsonModel.leftFinger0_2 = new TransformJsonObject(leftFinger0_2);
        transformJsonModel.leftFinger1 = new TransformJsonObject(leftFinger1);
        transformJsonModel.leftFinger1_1 = new TransformJsonObject(leftFinger1_1);
        transformJsonModel.leftFinger1_2 = new TransformJsonObject(leftFinger1_2);
        transformJsonModel.leftFinger2 = new TransformJsonObject(leftFinger2);
        transformJsonModel.leftFinger2_1 = new TransformJsonObject(leftFinger2_1);
        transformJsonModel.leftFinger2_2 = new TransformJsonObject(leftFinger2_2);
        transformJsonModel.leftFinger3 = new TransformJsonObject(leftFinger3);
        transformJsonModel.leftFinger3_1 = new TransformJsonObject(leftFinger3_1);
        transformJsonModel.leftFinger3_2 = new TransformJsonObject(leftFinger3_2);
        transformJsonModel.leftFinger4 = new TransformJsonObject(leftFinger4);
        transformJsonModel.leftFinger4_1 = new TransformJsonObject(leftFinger4_1);
        transformJsonModel.leftFinger4_2 = new TransformJsonObject(leftFinger4_2);
        transformJsonModel.leftThigh = new TransformJsonObject(leftThigh);
        transformJsonModel.leftCalf = new TransformJsonObject(leftCalf);
        transformJsonModel.leftFoot = new TransformJsonObject(leftFoot);

        //右侧部分
        transformJsonModel.rightClavicle = new TransformJsonObject(rightClavicle);
        transformJsonModel.rightUpperArm = new TransformJsonObject(rightUpperArm);
        transformJsonModel.rightForeArm = new TransformJsonObject(rightForeArm);
        transformJsonModel.rightHand = new TransformJsonObject(rightHand);
        //右侧手指部分
        transformJsonModel.rightFinger0 = new TransformJsonObject(rightFinger0);
        transformJsonModel.rightFinger0_1 = new TransformJsonObject(rightFinger0_1);
        transformJsonModel.rightFinger0_2 = new TransformJsonObject(rightFinger0_2);
        transformJsonModel.rightFinger1 = new TransformJsonObject(rightFinger1);
        transformJsonModel.rightFinger1_1 = new TransformJsonObject(rightFinger1_1);
        transformJsonModel.rightFinger1_2 = new TransformJsonObject(rightFinger1_2);
        transformJsonModel.rightFinger2 = new TransformJsonObject(rightFinger2);
        transformJsonModel.rightFinger2_1 = new TransformJsonObject(rightFinger2_1);
        transformJsonModel.rightFinger2_2 = new TransformJsonObject(rightFinger2_2);
        transformJsonModel.rightFinger3 = new TransformJsonObject(rightFinger3);
        transformJsonModel.rightFinger3_1 = new TransformJsonObject(rightFinger3_1);
        transformJsonModel.rightFinger3_2 = new TransformJsonObject(rightFinger3_2);
        transformJsonModel.rightFinger4 = new TransformJsonObject(rightFinger4);
        transformJsonModel.rightFinger4_1 = new TransformJsonObject(rightFinger4_1);
        transformJsonModel.rightFinger4_2 = new TransformJsonObject(rightFinger4_2);
        transformJsonModel.rightThigh = new TransformJsonObject(rightThigh);
        transformJsonModel.rightCalf = new TransformJsonObject(rightCalf);
        transformJsonModel.rightFoot = new TransformJsonObject(rightFoot);

/*
        transformJsonModel.leftToe0 = new TransformJsonObject(leftToe0);
        transformJsonModel.leftToe0_1 = new TransformJsonObject(leftToe0_1);
        transformJsonModel.leftToe0_2 = new TransformJsonObject(leftToe0_2);
        transformJsonModel.leftToe1 = new TransformJsonObject(leftToe1);
        transformJsonModel.leftToe1_1 = new TransformJsonObject(leftToe1_1);
        transformJsonModel.leftToe1_2 = new TransformJsonObject(leftToe1_2);
        transformJsonModel.leftToe2 = new TransformJsonObject(leftToe2);
        transformJsonModel.leftToe2_1 = new TransformJsonObject(leftToe2_1);
        transformJsonModel.leftToe2_2 = new TransformJsonObject(leftToe2_2);
        transformJsonModel.leftToe3 = new TransformJsonObject(leftToe3);
        transformJsonModel.leftToe3_1 = new TransformJsonObject(leftToe3_1);
        transformJsonModel.leftToe3_2 = new TransformJsonObject(leftToe3_2);
        transformJsonModel.leftToe4 = new TransformJsonObject(leftToe4);
        transformJsonModel.leftToe4_1 = new TransformJsonObject(leftToe4_1);
        transformJsonModel.leftToe4_2 = new TransformJsonObject(leftToe4_2);

        transformJsonModel.rightToe0 = new TransformJsonObject(rightToe0);
        transformJsonModel.rightToe0_1 = new TransformJsonObject(rightToe0_1);
        transformJsonModel.rightToe0_2 = new TransformJsonObject(rightToe0_2);
        transformJsonModel.rightToe1 = new TransformJsonObject(rightToe1);
        transformJsonModel.rightToe1_1 = new TransformJsonObject(rightToe1_1);
        transformJsonModel.rightToe1_2 = new TransformJsonObject(rightToe1_2);
        transformJsonModel.rightToe2 = new TransformJsonObject(rightToe2);
        transformJsonModel.rightToe2_1 = new TransformJsonObject(rightToe2_1);
        transformJsonModel.rightToe2_2 = new TransformJsonObject(rightToe2_2);
        transformJsonModel.rightToe3 = new TransformJsonObject(rightToe3);
        transformJsonModel.rightToe3_1 = new TransformJsonObject(rightToe3_1);
        transformJsonModel.rightToe3_2 = new TransformJsonObject(rightToe3_2);
        transformJsonModel.rightToe4 = new TransformJsonObject(rightToe4);
        transformJsonModel.rightToe4_1 = new TransformJsonObject(rightToe4_1);
        transformJsonModel.rightToe4_2 = new TransformJsonObject(rightToe4_2);*/
        return JsonUtility.ToJson(transformJsonModel, true);
    }
  

    private Transform getTransform(GameObject gb)
    {
        if (gb == null) return null;
        return gb.GetComponent<Transform>();
    }

    [HideInInspector] public Transform pelvis;
    [HideInInspector] public Transform BipRoot;
    //腰部
    [HideInInspector] public Transform waist;

    //胸部
    [HideInInspector] public Transform chest;

    //脖子
    [HideInInspector] public Transform neck;

    //头部
    [HideInInspector] public Transform head;

    ////////////////左侧信息/////////////////////
    //右臂上方节点,右锁骨位置
    [HideInInspector] public Transform leftClavicle;

    //左臂上方节点
    [HideInInspector] public Transform leftUpperArm;

    //左臂，前臂节点
    [HideInInspector] public Transform leftForeArm;

    //左侧手臂节点
    [HideInInspector] public Transform leftHand;

    //左侧大拇指
    [HideInInspector] public Transform leftFinger0;

    [HideInInspector] public Transform leftFinger0_1;

    [HideInInspector] public Transform leftFinger0_2;
    //第二个手指
    [HideInInspector] public Transform leftFinger1;

    [HideInInspector] public Transform leftFinger1_1;

    [HideInInspector] public Transform leftFinger1_2;

    //第三个手指
    [HideInInspector] public Transform leftFinger2;

    [HideInInspector] public Transform leftFinger2_1;

    [HideInInspector] public Transform leftFinger2_2;

    //第四个和第五个手指
    [HideInInspector] public Transform leftFinger3;

    [HideInInspector] public Transform leftFinger3_1;

    [HideInInspector] public Transform leftFinger3_2;

    //第五个手指
    [HideInInspector] public Transform leftFinger4;

    [HideInInspector] public Transform leftFinger4_1;

    [HideInInspector] public Transform leftFinger4_2;

    //左侧大腿根节点
    [HideInInspector] public Transform leftThigh;

    //左侧小腿节点
    [HideInInspector] public Transform leftCalf;

    //左脚脚踝节点
    [HideInInspector] public Transform leftFoot;

    ////////////////右侧信息/////////////////////
    //右臂上方节点,右锁骨位置
    [HideInInspector] public Transform rightClavicle;

    //右臂上方节点
    [HideInInspector] public Transform rightUpperArm;

    //右臂，前臂节点
    [HideInInspector] public Transform rightForeArm;

    //右侧手臂节点
    [HideInInspector] public Transform rightHand;

    //右侧大拇指
    [HideInInspector] public Transform rightFinger0;

    [HideInInspector] public Transform rightFinger0_1;

    [HideInInspector] public Transform rightFinger0_2;
    //第二个手指
    [HideInInspector] public Transform rightFinger1;

    [HideInInspector] public Transform rightFinger1_1;

    [HideInInspector] public Transform rightFinger1_2;

    //第三个手指
    [HideInInspector] public Transform rightFinger2;

    [HideInInspector] public Transform rightFinger2_1;

    [HideInInspector] public Transform rightFinger2_2;

    //第四个
    [HideInInspector] public Transform rightFinger3;

    [HideInInspector] public Transform rightFinger3_1;

    [HideInInspector] public Transform rightFinger3_2;

    //第五个手指
    [HideInInspector] public Transform rightFinger4;

    [HideInInspector] public Transform rightFinger4_1;

    [HideInInspector] public Transform rightFinger4_2;

    //右侧大腿根节点
    [HideInInspector] public Transform rightThigh;

    //右侧小腿节点
    [HideInInspector] public Transform rightCalf;

    //右脚脚踝节点
    [HideInInspector] public Transform rightFoot;

/*
    //左脚前脚趾节点
    [HideInInspector] public Transform leftToe0;
    [HideInInspector] public Transform leftToe0_1;
    [HideInInspector] public Transform leftToe0_2;
    [HideInInspector] public Transform leftToe1;
    [HideInInspector] public Transform leftToe1_1;
    [HideInInspector] public Transform leftToe1_2;

    [HideInInspector] public Transform leftToe2;
    [HideInInspector] public Transform leftToe2_1;
    [HideInInspector] public Transform leftToe2_2;

    [HideInInspector] public Transform leftToe3;
    [HideInInspector] public Transform leftToe3_1;
    [HideInInspector] public Transform leftToe3_2;
    [HideInInspector] public Transform leftToe4;
    [HideInInspector] public Transform leftToe4_1;
    [HideInInspector] public Transform leftToe4_2;

    //右脚前脚趾节点
    [HideInInspector] public Transform rightToe0;
    [HideInInspector] public Transform rightToe0_1;
    [HideInInspector] public Transform rightToe0_2;
    [HideInInspector] public Transform rightToe1;
    [HideInInspector] public Transform rightToe1_1;
    [HideInInspector] public Transform rightToe1_2;

    [HideInInspector] public Transform rightToe2;
    [HideInInspector] public Transform rightToe2_1;
    [HideInInspector] public Transform rightToe2_2;

    [HideInInspector] public Transform rightToe3;
    [HideInInspector] public Transform rightToe3_1;
    [HideInInspector] public Transform rightToe3_2;
    [HideInInspector] public Transform rightToe4;
    [HideInInspector] public Transform rightToe4_1;
    [HideInInspector] public Transform rightToe4_2;
*/

    //extra
    public Transform LForeTwist;
    public Transform RForeTwist;
}