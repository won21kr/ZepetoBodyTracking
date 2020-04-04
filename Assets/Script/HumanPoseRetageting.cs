using System.Collections;
using UnityEngine;

public class HumanPoseRetageting : MonoBehaviour {
    public GameObject sourceGo;
    public GameObject targetGo;

    HumanPoseHandler poseHandler = null;
    HumanPoseHandler targetHandler = null;
    
    void Update () {
        updatePose();
    }

    public void InitiatePoseRetargeter() {
        // 제페토 로딩이 끝나면 retargeter를 초기화한다.
        StartCoroutine(Retargeter());
    }    

    IEnumerator Retargeter() {
        Animator source = sourceGo.GetComponentInChildren<Animator>();   
        Animator target = targetGo.GetComponentInChildren<Animator>();   

        // source.runtimeAnimatorController
        Debug.Log($">>>>> {source}, {target}");
        
        if(source!=null && target!=null) {
            poseHandler = new HumanPoseHandler (source.avatar, source.transform);
            targetHandler = new HumanPoseHandler (target.avatar, target.transform);
        }
        yield return null;
    }

    void updatePose() {

        if(poseHandler==null || targetHandler==null ) {             
            return;
        }            

        HumanPose pose = new HumanPose ();
        // sensetime as source
        poseHandler.GetHumanPose (ref pose);

        // bone의 인덱스가 다른경우 직접 매핑함
        // Adjust finger spread with bending
        pose.muscles[56] = pose.muscles[55]; // Left thumb adjustment
        pose.muscles[60] = pose.muscles[59]; // Left index adjustment
        pose.muscles[64] = pose.muscles[63]; // Left middle adjustment
        pose.muscles[68] = pose.muscles[67]; // Left ring adjustment
        pose.muscles[72] = pose.muscles[71]; // Left little adjustment
        pose.muscles[76] = pose.muscles[75]; // Right thumb adjustment
        pose.muscles[80] = pose.muscles[79]; // Right index adjustment
        pose.muscles[84] = pose.muscles[83]; // Right middle adjustment
        pose.muscles[88] = pose.muscles[87]; // Right ring adjustment
        pose.muscles[92] = pose.muscles[91]; // Right little adjustment

        // zepeto as target
        targetHandler.SetHumanPose (ref pose);
    }

    // public void LookUpMuscleIndex () {
    //     string[] muscleName = HumanTrait.MuscleName;
    //     int i = 0;
    //     Debug.Log (this.gameObject.name);
    //     while (i < HumanTrait.MuscleCount) {
    //         Debug.Log (i + ": " + muscleName[i] + " min: " + HumanTrait.GetMuscleDefaultMin (i) + " max: " + HumanTrait.GetMuscleDefaultMax (i));
    //         i++;
    //     }
    // }

    // public void LookUpBones () {

    //     Animator source = sourceGo.GetComponent<Animator>();   
    
    //     HumanBone[] humanBones = source.avatar.humanDescription.human;
    //     Debug.Log ("Avatar Name:" + source.avatar.name);
    //     for (int i = 0; i < humanBones.Length; i++) {
    //         humanBones[i].limit.useDefaultValues = false;
    //         Debug.Log (i + " BoneName:" + humanBones[i].boneName + "-->" + humanBones[i].humanName + " || (Center):" + humanBones[i].limit.center + " (Min):" + humanBones[i].limit.min + " (Max):" + humanBones[i].limit.max + " (AxisLenght):" + humanBones[i].limit.axisLength);
    //     }
    // }
}