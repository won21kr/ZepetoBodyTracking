using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
public class HierarchyArisa : MonoBehaviour
{
    [SerializeField] Transform[] points = null;

    private float fps = 23.97f;
    [SerializeField] float currentFrameIndex = 1;

    private float continuousThreshold = 0.5f;
    private float continuousMove = 0;

    List<List<body>> skeletonFrames = new List<List<body>>();
    List<List<worldPos>> worldPosFrames = new List<List<worldPos>>();
    string[] worldPosFolderPaths;
    
    public LineRenderer skeletonline;
    List<float[]> posArr;

    public Vector3 worldOffset;
    public Vector3 ground;
    Vector3 groundDist;
    Vector3 cacheVec;
    private void Start()
    {
        var videoFolderNames = Directory.GetDirectories($"./Data/pe3d_py_v1"); //일반적으로 5비디오.
        if (videoFolderNames.Length == 0)
            Debug.LogError("검색된 폴더가 없습니다.");

        var worldPosSets = Directory.GetDirectories($"Data/Position");
        var myPosSetPath = worldPosSets[0];
        worldPosFolderPaths = Directory.GetDirectories($"{myPosSetPath}");

        var fileNames = Directory.GetFiles(videoFolderNames[0]);
        var worldPosFiles = Directory.GetFiles(worldPosFolderPaths[0]);



        skeletonFrames.Clear();
        for (int i = 0; i < fileNames.Length; i++)
        {
            if (fileNames[i].Contains(".DS_Store")) continue;
            if (fileNames[i].Contains(".meta")) continue;
            if (fileNames[i].Contains(".jpg")) continue;
            if (fileNames[i].Contains(".mp4")) continue;
            if (fileNames[i].Contains(".txt")) continue;
            string text = File.ReadAllText(fileNames[i]);
            skeletonFrames.Add(JsonConvert.DeserializeObject<List<body>>(text));
        }
        worldPosFrames.Clear();
        for (int i = 0; i < worldPosFiles.Length; i++)
        {
            if (worldPosFiles[i].Contains(".DS_Store")) continue;
            if (worldPosFiles[i].Contains(".meta")) continue;
            if (worldPosFiles[i].Contains(".jpg")) continue;
            if (worldPosFiles[i].Contains(".mp4")) continue;
            if (worldPosFiles[i].Contains(".txt")) continue;
            string text = File.ReadAllText(worldPosFiles[i]);
            worldPosFrames.Add(JsonConvert.DeserializeObject<List<worldPos>>(text));
        }
        currentFrameIndex = 1;
        SetFrame();
    }
    private void Update()
    {
        CheckKeyboardInput();
    }
    void CheckKeyboardInput()
    {
        #region frame 전환
        // A D 를 눌렀을 때 프레임 전환
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            continuousMove = 0;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentFrameIndex = currentFrameIndex - 1;
            SetFrame();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentFrameIndex = currentFrameIndex + 1;
            SetFrame();
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //왼쪽
        {
            continuousMove += Time.deltaTime;
            if (continuousMove > continuousThreshold)
            {
                var cached = currentFrameIndex;
                currentFrameIndex = currentFrameIndex - Time.deltaTime * fps;
                if ((int)cached > (int)currentFrameIndex)
                    SetFrame();
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) //왼쪽
        {
            continuousMove += Time.deltaTime;
            if (continuousMove > continuousThreshold)
            {
                var cached = currentFrameIndex;
                currentFrameIndex = currentFrameIndex + Time.deltaTime * fps;
                if ((int)cached < (int)currentFrameIndex)
                    SetFrame();
            }
        }
        #endregion
    }
    public void SetFrame()
    {
        if (currentFrameIndex > skeletonFrames.Count)
            currentFrameIndex = skeletonFrames.Count;
        if (currentFrameIndex < 1)
            currentFrameIndex = 1;

        if (skeletonFrames.Count == 0) return;
        if (skeletonFrames[(int)currentFrameIndex].Count == 0) return;
        var posArr = skeletonFrames[(int)currentFrameIndex][0].kpt3d_body;
        if (posArr == null) return;
        if (worldPosFrames.Count == 0) return;
        if (worldPosFrames[(int)currentFrameIndex].Count == 0) return;
        var pos = worldPosFrames[(int)currentFrameIndex][0].world_pos;
        //var firstVec = new Vector3(pos[0][0] , 0, 0);
        //ResetWorldPos(pos); //test 목적으로 world position 적용 안함
        SetPoints(posArr);
        //SetWorldPos(pos);
    }
    public void ResetWorldPos(List<float[]> pos)
    {
        var firstVec = new Vector3(pos[0][0] , 0, 0);
        transform.localPosition = firstVec + worldOffset;
    }
    public void SetWorldPos(List<float[]> pos)
    {
        //set position
        var firstVec = new Vector3(pos[0][0] , 0, 0);
        transform.localPosition = firstVec + worldOffset + ground - groundDist;
        cacheVec = firstVec;
    }
    public void SetPoints(List<float[]> posArr)
    {
        if (posArr == null)
            return;
        
        
        
        WithOnlyZLength(posArr);
    }
    void WithOnlyZLength(List<float[]> posArr)
    {
        Vector3 alpha;
        Vector3 beta;
        Vector3 gamma;
        Vector3 delta;
        Vector3 cross;
        Vector3 cross2;
        Vector3 cross3;
        Vector3 target;
        float dot;
        Quaternion previousFrameRot;
        Quaternion afterLookAtRot;

        //Vector 생성. 좌표계 차이로 -z 적용
        Vector3[] posVec = new Vector3[posArr.Count];
        for (int i = 0; i < posVec.Length; i++)
            posVec[i] = new Vector3(posArr[i][0], posArr[i][1], -posArr[i][2]);

        //테스트용으로 직접 값 할당
        var pelvis = Vector3.Lerp(posVec[8], posVec[11], 0.5f);
        Vector3 modelCenter = pelvis + transform.position;
        //골반 기준 상대벡터 생성. 14개
        Vector3[] relPosVec = new Vector3[posArr.Count];
        for (int i = 0; i < relPosVec.Length; i++)
        {
            relPosVec[i] = posVec[i] - pelvis;
        }
        
        points[0].position = modelCenter;

        //임시로 skeleton 표시
        Vector3[] skelArr = {
                new Vector3(0,0,0),
                relPosVec[8],
                relPosVec[9],
                relPosVec[10],
                relPosVec[9],
                relPosVec[8],
                relPosVec[11],
                relPosVec[12],
                relPosVec[13],
                relPosVec[12],
                relPosVec[11],
                new Vector3(0,0,0),
                relPosVec[1],
                relPosVec[2],
                relPosVec[3],
                relPosVec[4],
                relPosVec[3],
                relPosVec[2],
                relPosVec[1],
                relPosVec[0],
                relPosVec[1],
                relPosVec[5],
                relPosVec[6],
                relPosVec[7],
            };
        skeletonline.positionCount = skelArr.Length;
        for (int i = 0; i < skelArr.Length; i++)
        {
            skeletonline.SetPosition(i, points[0].position+new Vector3(0.5f,0,0) - pelvis + skelArr[i]);
        }


        //pelvis 회전 계산
        alpha = relPosVec[8] - Vector3.zero;
        beta = relPosVec[1] - Vector3.zero;
        cross = Vector3.Cross(alpha, beta).normalized;
        points[0].LookAt(points[0].position - ((relPosVec[8] + relPosVec[11]) / 2) + relPosVec[1]);
        afterLookAtRot = points[0].localRotation;
        dot = Mathf.Clamp(Vector3.Dot(points[0].right, cross), -1, 1);
        points[0].Rotate(new Vector3(0, 0, 1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        dot = Mathf.Clamp(Vector3.Dot(points[0].right, cross), -1, 1);
        if (dot < 0.999f)
        {
            points[0].localRotation = afterLookAtRot;
            dot = Mathf.Clamp(Vector3.Dot(points[0].right, cross), -1, 1);
            points[0].Rotate(new Vector3(0, 0, -1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        }

        //leftThigh 회전 계산
        alpha = relPosVec[12] - relPosVec[11];
        beta = relPosVec[13] - relPosVec[12];
        gamma = relPosVec[11] - ((relPosVec[8] + relPosVec[11]) / 2);
        cross = Vector3.Cross(alpha, beta).normalized;
        target = Vector3.Cross(alpha, cross).normalized;
        cross2 = Vector3.Cross(target, gamma).normalized;
        if (Vector3.Angle(alpha, cross2) > 90) // 이상한 각도가 나올 시 회전시키지 않는다.
        {
            goto LeftLegEnd;
        }
        points[1].LookAt(points[1].position - relPosVec[11] + relPosVec[12] );
        afterLookAtRot = points[1].localRotation;
        dot = Mathf.Clamp(Vector3.Dot(-points[1].up, target), -1, 1);
        points[1].Rotate(new Vector3(0, 0, 1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        dot = Mathf.Clamp(Vector3.Dot(-points[1].up, target), -1, 1);
        if (dot < 0.999f)
        {
            points[1].localRotation = afterLookAtRot;
            dot = Mathf.Clamp(Vector3.Dot(-points[1].up, target), -1, 1);
            points[1].Rotate(new Vector3(0, 0, -1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        }

        //leftCalc 회전 계산
        target = Vector3.Cross(beta, cross).normalized;
        points[2].LookAt(points[2].position - relPosVec[12] + relPosVec[13]);
        afterLookAtRot = points[2].localRotation;
        dot = Mathf.Clamp(Vector3.Dot(-points[2].up, target), -1, 1);
        points[2].Rotate(new Vector3(0, 0, 1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        dot = Mathf.Clamp(Vector3.Dot(-points[2].up, target), -1, 1);
        if (dot < 0.999f)
        {
            points[2].localRotation = afterLookAtRot;
            dot = Mathf.Clamp(Vector3.Dot(-points[2].up, target), -1, 1);
            points[2].Rotate(new Vector3(0, 0, -1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        }
        LeftLegEnd:

        //rightThigh 회전 계산
        alpha = relPosVec[9] - relPosVec[8];
        beta = relPosVec[10] - relPosVec[9];
        gamma = relPosVec[8] - ((relPosVec[8] + relPosVec[11]) / 2);
        cross = Vector3.Cross(alpha, beta).normalized;
        target = Vector3.Cross(alpha, cross).normalized;
        cross2 = Vector3.Cross(target, gamma).normalized;
        if (Vector3.Angle(alpha, cross2) < 90) // 이상한 각도가 나올 시 회전시키지 않는다.
        {
            goto RightLegEnd;
        }
        points[4].LookAt(points[4].position - relPosVec[8] + relPosVec[9]);
        afterLookAtRot = points[4].localRotation;
        dot = Mathf.Clamp(Vector3.Dot(points[4].up, target), -1, 1);
        points[4].Rotate(new Vector3(0, 0, 1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        dot = Mathf.Clamp(Vector3.Dot(points[4].up, target), -1, 1);
        if (dot < 0.999f)
        {
            points[4].localRotation = afterLookAtRot;
            dot = Mathf.Clamp(Vector3.Dot(points[4].up, target), -1, 1);
            points[4].Rotate(new Vector3(0, 0, -1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        }

        //rightCalc 회전 계산 
        alpha = relPosVec[9] - relPosVec[8];
        beta = relPosVec[10] - relPosVec[9];
        cross = Vector3.Cross(alpha, beta).normalized;
        target = Vector3.Cross(beta, cross).normalized;
        points[5].LookAt(points[5].position - relPosVec[9] + relPosVec[10]);
        afterLookAtRot = points[5].localRotation;
        dot = Mathf.Clamp(Vector3.Dot(points[5].up, target), -1, 1);
        points[5].Rotate(new Vector3(0, 0, 1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        dot = Mathf.Clamp(Vector3.Dot(points[5].up, target), -1, 1);
        if (dot < 0.999f)
        {
            points[5].localRotation = afterLookAtRot;
            dot = Mathf.Clamp(Vector3.Dot(points[5].up, target), -1, 1);
            points[5].Rotate(new Vector3(0, 0, -1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        }
        RightLegEnd:

        //spine 회전 계산 (pelvis와 같다)
        alpha = relPosVec[8] - Vector3.zero;
        beta = relPosVec[1] - Vector3.zero;
        cross = Vector3.Cross(alpha, beta).normalized;
        points[7].LookAt(points[7].position - ((relPosVec[8]+relPosVec[11])/2) + relPosVec[1]);
        afterLookAtRot = points[7].localRotation;
        dot = Mathf.Clamp(Vector3.Dot(points[7].right, cross), -1, 1);
        points[7].Rotate(new Vector3(0, 0, 1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        dot = Mathf.Clamp(Vector3.Dot(points[7].right, cross), -1, 1);
        if (dot < 0.999f)
        {
            points[7].localRotation = afterLookAtRot;
            dot = Mathf.Clamp(Vector3.Dot(points[7].right, cross), -1, 1);
            points[7].Rotate(new Vector3(0, 0, -1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        }

        //spine1 회전 계산
        alpha = relPosVec[2] - relPosVec[5];
        beta = relPosVec[1] - Vector3.zero;
        cross = Vector3.Cross(alpha, beta).normalized;
        points[8].LookAt(points[8].position - ((relPosVec[8] + relPosVec[11]) / 2) + relPosVec[1]);
        afterLookAtRot = points[8].localRotation;
        dot = Mathf.Clamp(Vector3.Dot(points[8].right, cross), -1, 1);
        points[8].Rotate(new Vector3(0, 0, 1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        dot = Mathf.Clamp(Vector3.Dot(points[8].right, cross), -1, 1);
        if (dot < 0.999f)
        {
            points[8].localRotation = afterLookAtRot;
            dot = Mathf.Clamp(Vector3.Dot(points[8].right, cross), -1, 1);
            points[8].Rotate(new Vector3(0, 0, -1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        }

        //spine2 회전 계산
        alpha = relPosVec[2] - relPosVec[5];
        beta = relPosVec[1] - Vector3.zero;
        cross = Vector3.Cross(alpha, beta).normalized;
        points[9].LookAt(points[9].position - ((relPosVec[8] + relPosVec[11]) / 2) + relPosVec[1]);
        afterLookAtRot = points[9].localRotation;
        dot = Mathf.Clamp(Vector3.Dot(points[9].right, cross), -1, 1);
        points[9].Rotate(new Vector3(0, 0, 1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        dot = Mathf.Clamp(Vector3.Dot(points[9].right, cross), -1, 1);
        if (dot < 0.999f)
        {
            points[9].localRotation = afterLookAtRot;
            dot = Mathf.Clamp(Vector3.Dot(points[9].right, cross), -1, 1);
            points[9].Rotate(new Vector3(0, 0, -1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        }

        //leftClavicle 회전 계산
        alpha = relPosVec[5] - relPosVec[1];
        beta = (relPosVec[8]+relPosVec[11])/2 - relPosVec[1];
        cross = Vector3.Cross(alpha, beta).normalized;
        target = Vector3.Cross(alpha, cross).normalized;
        points[10].LookAt(points[10].position - relPosVec[1] + relPosVec[5]);
        afterLookAtRot = points[10].localRotation;
        dot = Mathf.Clamp(Vector3.Dot(points[10].up, target), -1, 1);
        points[10].Rotate(new Vector3(0, 0, 1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        dot = Mathf.Clamp(Vector3.Dot(points[10].up, target), -1, 1);
        if (dot < 0.999f)
        {
            points[10].localRotation = afterLookAtRot;
            dot = Mathf.Clamp(Vector3.Dot(points[10].up, target), -1, 1);
            points[10].Rotate(new Vector3(0, 0, -1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        }

        //leftUpperArm 회전 계산
        alpha = relPosVec[6] - relPosVec[5];
        beta = relPosVec[7] - relPosVec[6];
        gamma = relPosVec[1] - ((relPosVec[8] + relPosVec[11]) / 2);
        delta = relPosVec[5] - relPosVec[1];
        cross = Vector3.Cross(alpha, beta).normalized;
        target = Vector3.Cross(alpha, cross).normalized;
        cross2 = Vector3.Cross(target, gamma).normalized;
        cross3 = Vector3.Cross(cross2, delta).normalized;

        previousFrameRot = points[11].localRotation;
        points[11].LookAt(points[11].position - relPosVec[5] + relPosVec[6]);
        if (Vector3.Angle(delta, cross2) < 90 && Vector3.Angle(cross3, gamma)>90) // 이상한 각도가 나올 시 회전시키지 않는다.
        {
            //print("left error");
            //goto LeftArm;
        }
        
        afterLookAtRot = points[11].localRotation;
        dot = Mathf.Clamp(Vector3.Dot(points[11].up, target), -1, 1);
        points[11].Rotate(new Vector3(0, 0, 1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        dot = Mathf.Clamp(Vector3.Dot(points[11].up, target), -1, 1);
        if (dot < 0.999f)
        {

            points[11].localRotation = afterLookAtRot;
            dot = Mathf.Clamp(Vector3.Dot(points[11].up, target), -1, 1);
            points[11].Rotate(new Vector3(0, 0, -1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        }
        LeftArm:

        //leftForeArm 회전 계산. 아직 손의 방향을 모르기 때문에 z축 계산은 아직 하지 않았 아직 손의 방향을 모르기 때문에 z축 계산은 아직 하지 않았음
        alpha = relPosVec[6] - relPosVec[5];
        beta = relPosVec[7] - relPosVec[6];
        cross = Vector3.Cross(alpha, beta).normalized;
        target = Vector3.Cross(beta, cross).normalized;
        cross2 = Vector3.Cross(target, alpha).normalized;
        points[12].LookAt(points[12].position - relPosVec[6] + relPosVec[7]);

        //points[12].Rotate(new Vector3(0, 0, 1), -afterLookAtRot.z);
        //dot = Mathf.Clamp(Vector3.Dot(points[12].up, target), -1, 1);
        //points[12].Rotate(new Vector3(0, 0, 1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        //dot = Mathf.Clamp(Vector3.Dot(points[12].up, target), -1, 1);
        //if (dot < 0.999f)
        //{
        //    points[12].localRotation = afterLookAtRot;
        //    dot = Mathf.Clamp(Vector3.Dot(points[12].up, target), -1, 1);
        //    points[12].Rotate(new Vector3(0, 0, -1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        //}

        //neck 회전 계산
        alpha = relPosVec[2] - relPosVec[5];
        beta = relPosVec[0] - relPosVec[1];
        cross = Vector3.Cross(alpha, beta).normalized;
        points[14].LookAt(points[14].position - relPosVec[1] + relPosVec[0]);
        afterLookAtRot = points[14].localRotation;
        dot = Mathf.Clamp(Vector3.Dot(points[14].right, cross), -1, 1);
        points[14].Rotate(new Vector3(0, 0, 1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        dot = Mathf.Clamp(Vector3.Dot(points[14].right, cross), -1, 1);
        if (dot < 0.999f)
        {
            points[14].localRotation = afterLookAtRot;
            dot = Mathf.Clamp(Vector3.Dot(points[14].right, cross), -1, 1);
            points[14].Rotate(new Vector3(0, 0, -1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        }

        //rightClavicle 회전 계산
        alpha = relPosVec[2] - relPosVec[1];
        beta = (relPosVec[8] + relPosVec[11]) / 2 - relPosVec[1];
        cross = Vector3.Cross(alpha, beta).normalized;
        target = Vector3.Cross(alpha, cross).normalized;
        points[16].LookAt(points[16].position - relPosVec[1] + relPosVec[2]);
        afterLookAtRot = points[16].localRotation;
        dot = Mathf.Clamp(Vector3.Dot(-points[16].up, target), -1, 1);
        points[16].Rotate(new Vector3(0, 0, 1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        dot = Mathf.Clamp(Vector3.Dot(-points[16].up, target), -1, 1);
        if (dot < 0.999f)
        {
            points[16].localRotation = afterLookAtRot;
            dot = Mathf.Clamp(Vector3.Dot(-points[16].up, target), -1, 1);
            points[16].Rotate(new Vector3(0, 0, -1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        }

        //rightUpperArm 회전 계산
        alpha = relPosVec[3] - relPosVec[2];
        beta = relPosVec[4] - relPosVec[3];
        gamma = relPosVec[1] - ((relPosVec[8] + relPosVec[11]) / 2);
        delta = relPosVec[2] - relPosVec[1];
        cross = Vector3.Cross(alpha, beta).normalized;
        target = Vector3.Cross(alpha, cross).normalized;
        cross2 = Vector3.Cross(target, gamma).normalized;
        cross3 = Vector3.Cross(cross2, delta).normalized;
        previousFrameRot = points[17].localRotation;
        points[17].LookAt(points[17].position - relPosVec[2] + relPosVec[3]);
        if (Vector3.Angle(delta, cross2) > 90 && Vector3.Angle(cross3, gamma) > 90) // 이상한 각도가 나올 시 회전시키지 않는다.
        {
            //print("right error");
            //goto RightArm;
        }
        
        afterLookAtRot = points[17].localRotation;
        dot = Mathf.Clamp(Vector3.Dot(points[17].up, target), -1, 1);
        points[17].Rotate(new Vector3(0, 0, 1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        dot = Mathf.Clamp(Vector3.Dot(points[17].up, target), -1, 1);
        if (dot < 0.999f)
        {
            points[17].localRotation = afterLookAtRot;
            dot = Mathf.Clamp(Vector3.Dot(points[17].up, target), -1, 1);
            points[17].Rotate(new Vector3(0, 0, -1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        }
        RightArm:
        //Vector3[] pointsArr = {
        //        new Vector3(0,0,0),
        //        alpha,
        //        alpha+beta,
        //        alpha,
        //        alpha+target,
        //        alpha,
        //        new Vector3(0,0,0),
        //        target,
        //        new Vector3(0,0,0),
        //        cross3,
        //        new Vector3(0,0,0),
        //        gamma.normalized,
        //        new Vector3(0,0,0),
        //        delta.normalized,
        //        new Vector3(0,0,0),
        //        cross2,
        //        new Vector3(0,0,0),
        //    };
        //line.positionCount = pointsArr.Length;
        //for (int i = 0; i < pointsArr.Length; i++)
        //{
        //    line.SetPosition(i, points[17].position - pelvis + pointsArr[i]);
        //}
        //rightForeArm 회전 계산. 아직 손의 방향을 모르기 때문에 z축 계산은 아직 하지 않았음
        alpha = relPosVec[3] - relPosVec[2];
        beta = relPosVec[4] - relPosVec[3];
        cross = Vector3.Cross(alpha, beta).normalized;
        target = Vector3.Cross(beta, cross).normalized;
        points[18].LookAt(points[18].position - relPosVec[3] + relPosVec[4]);
        //afterLookAtRot = points[18].localRotation;
        //dot = Mathf.Clamp(Vector3.Dot(points[18].up, target), -1, 1);
        //points[18].Rotate(new Vector3(0, 0, 1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        //dot = Mathf.Clamp(Vector3.Dot(points[18].up, target), -1, 1);
        //if (dot < 0.999f)
        //{
        //    points[18].localRotation = afterLookAtRot;
        //    dot = Mathf.Clamp(Vector3.Dot(points[18].up, target), -1, 1);
        //    points[18].Rotate(new Vector3(0, 0, -1), -(Mathf.Acos(dot) * Mathf.Rad2Deg));
        //}

        points[0].position = points[0].position - pelvis; // 골반을 0,0,0으로 고정
        groundDist = new Vector3(0, Mathf.Min(points[3].position.y, points[6].position.y), 0);
    }
}
