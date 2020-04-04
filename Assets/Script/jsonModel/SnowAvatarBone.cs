using System.Collections.Generic;
using UnityEngine;

public class SnowAvatarBone
{
    public GameObject avatarObj;    /// target
    Dictionary <string, Transform> avatarTransforms = new Dictionary <string, Transform>();

    public virtual void applyJsonModelToTransform(TransformJsonObject transformJsonObject, Transform target) {
        target.localRotation = transformJsonObject.getRotation();    
    }

    public virtual void updateAvatarBone(GameObject avatarObj) {
        var transforms = avatarObj.GetComponentsInChildren<Transform>();        
        foreach(Transform joint in transforms) {
            avatarTransforms.Add(joint.name, joint);
        }
    }

    public virtual bool isAvatarInitialized() {
        return (avatarTransforms.Count>0)?true:false;
    }
    
    public virtual void updateFromTransformModel(TransformJsonModel transformJsonModel, bool includeHead = false)
    {
        // if(avatarObj==null){
        //     Debug.LogError("SnowAvatarBone : Target avatar bone is not initailized!");
        //     return;
        // }
            
        // if(!isAvatarInitialized()) {
        //     updateAvatarBone(avatarObj);
        // }

        if (transformJsonModel != null)
        {                     
            string name = "Bip001 "; 
            Dictionary <string, TransformJsonObject> jsonModels = new Dictionary <string, TransformJsonObject>() {
                [$"{name}Pelvis"] = transformJsonModel.pelvis,
                // ["waist"] = transformJsonModel.waist,
                [$"{name}Spine1"] = transformJsonModel.chest,
                // [$"{name}Neck"] = transformJsonModel.neck,
                [$"{name}Head"] = transformJsonModel.head,
                [$"{name}L Clavicle"] = transformJsonModel.leftClavicle,
                [$"{name}L UpperArm"] = transformJsonModel.leftUpperArm,
                [$"{name}L Forearm"] = transformJsonModel.leftForeArm,
                [$"{name}L Hand"] = transformJsonModel.leftHand,
                [$"{name}L Finger0"] = transformJsonModel.leftFinger0,
                [$"{name}L Finger01"] = transformJsonModel.leftFinger0_1,
                [$"{name}L Finger02"] = transformJsonModel.leftFinger0_2,
                [$"{name}L Finger1"] = transformJsonModel.leftFinger1,
                [$"{name}L Finger11"] = transformJsonModel.leftFinger1_1,
                [$"{name}L Finger12"] = transformJsonModel.leftFinger1_2,
                [$"{name}L Finger2"] = transformJsonModel.leftFinger2,
                [$"{name}L Finger21"] = transformJsonModel.leftFinger2_1,
                [$"{name}L Finger22"] = transformJsonModel.leftFinger2_2,
                [$"{name}L Finger3"] = transformJsonModel.leftFinger3,
                [$"{name}L Finger31"] = transformJsonModel.leftFinger3_1,
                [$"{name}L Finger32"] = transformJsonModel.leftFinger3_2,
                [$"{name}L Finger4"] = transformJsonModel.leftFinger4,
                [$"{name}L Finger41"] = transformJsonModel.leftFinger4_1,
                [$"{name}L Finger42"] = transformJsonModel.leftFinger4_2,
                [$"{name}L Thigh"] = transformJsonModel.leftThigh,
                [$"{name}L Calf"] = transformJsonModel.leftCalf,
                [$"{name}L Foot"] = transformJsonModel.leftFoot,
                [$"{name}R Clavicle"] = transformJsonModel.rightClavicle,
                [$"{name}R UpperArm"] = transformJsonModel.rightUpperArm,
                [$"{name}R Forearm"] = transformJsonModel.rightForeArm,
                [$"{name}R Hand"] = transformJsonModel.rightHand,
                [$"{name}R Finger0"] = transformJsonModel.rightFinger0,
                [$"{name}R Finger01"] = transformJsonModel.rightFinger0_1,
                [$"{name}R Finger02"] = transformJsonModel.rightFinger0_2,
                [$"{name}R Finger1"] = transformJsonModel.rightFinger1,
                [$"{name}R Finger11"] = transformJsonModel.rightFinger1_1,
                [$"{name}R Finger12"] = transformJsonModel.rightFinger1_2,
                [$"{name}R Finger2"] = transformJsonModel.rightFinger2,
                [$"{name}R Finger21"] = transformJsonModel.rightFinger2_1,
                [$"{name}R Finger22"] = transformJsonModel.rightFinger2_2,
                [$"{name}R Finger3"] = transformJsonModel.rightFinger3,
                [$"{name}R Finger31"] = transformJsonModel.rightFinger3_1,
                [$"{name}R Finger32"] = transformJsonModel.rightFinger3_2,
                [$"{name}R Finger4"] = transformJsonModel.rightFinger4,
                [$"{name}R Finger41"] = transformJsonModel.rightFinger4_1,
                [$"{name}R Finger42"] = transformJsonModel.rightFinger4_2,
                [$"{name}R Thigh"] = transformJsonModel.rightThigh,
                [$"{name}R Calf"] = transformJsonModel.rightCalf,
                [$"{name}R Foot"] = transformJsonModel.rightFoot 
            };

            
            foreach(KeyValuePair<string, TransformJsonObject> jsonModel in jsonModels) {
                // Debug.Log($"{jsonModel.Key}, {jsonModel.Value}");
                var target = avatarTransforms[jsonModel.Key];
                if(target!=null) {
                    // transfrom, transformObject
                    applyJsonModelToTransform(jsonModel.Value, target);
                }
            }   

        }
    }
    
 
    public virtual string toJson()
    {
        TransformJsonModel transformJsonModel = new TransformJsonModel();
        string name = "Bip001 "; 
 
        transformJsonModel.pelvis = new TransformJsonObject(avatarTransforms[$"{name}Pelvis"]);
        // transformJsonModel.waist = new TransformJsonObject(avatarTransforms[$"{name}waist"]);
        transformJsonModel.chest = new TransformJsonObject(avatarTransforms[$"{name}Spine1"]);
        transformJsonModel.neck = new TransformJsonObject(avatarTransforms[$"{name}Neck"]);
        transformJsonModel.head = new TransformJsonObject(avatarTransforms[$"{name}Head"]);
        transformJsonModel.leftClavicle = new TransformJsonObject(avatarTransforms[$"{name}L Clavicle"]);
/*        
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
*/
        return JsonUtility.ToJson(transformJsonModel, true);
    }

}