
using UnityEngine;

[System.Serializable]
public class TransformJsonModel
{
    public TransformJsonObject pelvis;
    public TransformJsonObject waist;
    public TransformJsonObject chest;
    //脖子
    public TransformJsonObject neck;

    //头部
    public TransformJsonObject head;

    ////////////////左侧信息/////////////////////
    //左臂上方节点,左锁骨位置
    public TransformJsonObject leftClavicle;

    //左臂上方节点
    public TransformJsonObject leftUpperArm;

    //左臂，前臂节点
    public TransformJsonObject leftForeArm;

    //左侧手臂节点
    public TransformJsonObject leftHand;

    //左侧大拇指
    public TransformJsonObject leftFinger0;

    public TransformJsonObject leftFinger0_1;

    public TransformJsonObject leftFinger0_2;
    //第二个手指
    public TransformJsonObject leftFinger1;

    public TransformJsonObject leftFinger1_1;

    public TransformJsonObject leftFinger1_2;

    //第三个手指
    public TransformJsonObject leftFinger2;

    public TransformJsonObject leftFinger2_1;

    public TransformJsonObject leftFinger2_2;

    //第四个和第五个手指
    public TransformJsonObject leftFinger3;

    public TransformJsonObject leftFinger3_1;

    public TransformJsonObject leftFinger3_2;

    //第五个手指
    public TransformJsonObject leftFinger4;

    public TransformJsonObject leftFinger4_1;

    public TransformJsonObject leftFinger4_2;


    //左侧大腿根节点
    public TransformJsonObject leftThigh;

    //左侧小腿节点
    public TransformJsonObject leftCalf;

    //左脚脚踝节点
    public TransformJsonObject leftFoot;

    //左脚前脚趾节点
    public TransformJsonObject leftToe0;
    public TransformJsonObject leftToe0_1;
    public TransformJsonObject leftToe0_2;
    public TransformJsonObject leftToe1;
    public TransformJsonObject leftToe1_1;
    public TransformJsonObject leftToe1_2;

    public TransformJsonObject leftToe2;
    public TransformJsonObject leftToe2_1;
    public TransformJsonObject leftToe2_2;

    public TransformJsonObject leftToe3;
    public TransformJsonObject leftToe3_1;
    public TransformJsonObject leftToe3_2;
    public TransformJsonObject leftToe4;
    public TransformJsonObject leftToe4_1;
    public TransformJsonObject leftToe4_2;

    ////////////////右侧信息/////////////////////

    //右臂上方节点,右锁骨位置
    public TransformJsonObject rightClavicle;

    //右臂上方节点
    public TransformJsonObject rightUpperArm;

    //右臂，前臂节点
    public TransformJsonObject rightForeArm;

    //右侧手臂节点
    public TransformJsonObject rightHand;

    //右侧大拇指
    public TransformJsonObject rightFinger0;

    public TransformJsonObject rightFinger0_1;

    public TransformJsonObject rightFinger0_2;
    //第二个手指
    public TransformJsonObject rightFinger1;

    public TransformJsonObject rightFinger1_1;

    public TransformJsonObject rightFinger1_2;

    //第三个手指
    public TransformJsonObject rightFinger2;

    public TransformJsonObject rightFinger2_1;

    public TransformJsonObject rightFinger2_2;

    //第四个
    public TransformJsonObject rightFinger3;

    public TransformJsonObject rightFinger3_1;

    public TransformJsonObject rightFinger3_2;

    //第五个手指
    public TransformJsonObject rightFinger4;

    public TransformJsonObject rightFinger4_1;

    public TransformJsonObject rightFinger4_2;

    //右侧大腿根节点
    public TransformJsonObject rightThigh;

    //右侧小腿节点
    public TransformJsonObject rightCalf;

    //右脚脚踝节点
    public TransformJsonObject rightFoot;

    //右脚前脚趾节点

    public TransformJsonObject rightToe0;
    public TransformJsonObject rightToe0_1;
    public TransformJsonObject rightToe0_2;
    public TransformJsonObject rightToe1;
    public TransformJsonObject rightToe1_1;
    public TransformJsonObject rightToe1_2;

    public TransformJsonObject rightToe2;
    public TransformJsonObject rightToe2_1;
    public TransformJsonObject rightToe2_2;

    public TransformJsonObject rightToe3;
    public TransformJsonObject rightToe3_1;
    public TransformJsonObject rightToe3_2;
    public TransformJsonObject rightToe4;
    public TransformJsonObject rightToe4_1;
    public TransformJsonObject rightToe4_2;
}

[System.Serializable]
public class TransformJsonObject
{
    public float[] localPosition = new float[3];
    public float[] localRotation = new float[4];//w,x,y,z
    public float[] localAngle = new float[3];
    public float[] localScale = new float[3];

    public Vector3 getPositon()
    {
        return arrayToVector3(localPosition);
    }

    public Vector3 getScale()
    {
        return arrayToVector3(localScale);
    }

    public Quaternion getRotation()
    {
        if (localRotation != null)
            return arrayToQuaternion(localRotation);

        else
            return Quaternion.Euler(localAngle[0], localAngle[1], localAngle[2]);

    }

    public TransformJsonObject(Transform transform)
    {
        if (transform == null)
            return;
        
        // this.localPosition = vector3ToArray(transform.localPosition);
        this.localRotation = quaternionToArray(transform.localRotation);
        // this.localAngle = quaternionToOAngle(transform.localEulerAngles);
        // this.localScale = vector3ToArray(transform.localScale);
    }

    public static float[] quaternionToOAngle(Vector3 localRotation)
    {
        return new float[3] { localRotation.x, localRotation.y, localRotation.z };
    }

    public static float[] quaternionToArray(Quaternion quaternion)
    {
        return new float[4] { quaternion.w, quaternion.x, quaternion.y, quaternion.z };
    }

    public static Quaternion arrayToQuaternion(float[] array)
    {
        return new Quaternion(array[1], array[2], array[3], array[0]);
    }

    public static float[] vector3ToArray(Vector3 vector)
    {
        return new float[3] { vector.x, vector.y, vector.z };
    }


    public static Vector3 arrayToVector3(float[] array)
    {
        return new Vector3(array[0], array[1], array[2]);
    }
}