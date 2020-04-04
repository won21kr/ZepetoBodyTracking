using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent (typeof (LineRenderer))]
public class SnowBoneRendererRuntime : MonoBehaviour
{
    // LineRenderer lineRenderer;
    public Color Color = Color.yellow;
    [Range(1.0f, 100.0f)]
	public float Size = 8.0f;

    public GameObject targetAvatar;
    public GameObject linePrefs;
    public GameObject axisPrefs;
    
    GameObject[] linePool;


    // Start is called before the first frame update
    void Start()
    {
        // lineRenderer = GetComponent<LineRenderer>();
        Transform[] tempTransforms = targetAvatar.GetComponentsInChildren<Transform>();
        int count = tempTransforms.Length;
        linePool = new GameObject[count];
        
        Debug.Log("->> line count : " + count);
        for (var i = 0; i < count; i++)
        {
            linePool[i] = Instantiate(linePrefs) as GameObject;
            LineRenderer lr = linePool[i].GetComponent<LineRenderer>();
          
            // add axis
            var axis = GameObject.Instantiate(axisPrefs, new Vector3(0, 0, 0), Quaternion.identity);
            axis.GetComponent<Transform>().SetParent(tempTransforms[i], false);  
            axis.gameObject.tag = "axis";
            tempTransforms[i].gameObject.gameObject.tag= "bone";
            
        }
            

    }

    // Update is called once per frame
    void Update()
    {
        Transform[] tempTransforms = targetAvatar.GetComponentsInChildren<Transform>();
        int count = tempTransforms.Length;

        Debug.Log("->> line count : " + count);
        int tmp = 0;
        for (var i = 0; i < count; i++)
        {
            if(tempTransforms[i].gameObject.gameObject.tag == "bone")
            {
                            // float _diff = 0.1f;
                LineRenderer lr = linePool[tmp++].GetComponent<LineRenderer>();
  
                lr.SetPosition(0, tempTransforms[i].position);
                lr.SetPosition(1, tempTransforms[i].parent.position);   
                
            }
            
        }
    }


    public void OnDrawGizmos() {
        
        // if(targetAvatar==null)
        //     return;

		// Transform[] tempTransforms = targetAvatar.GetComponentsInChildren<Transform>();

        // //VRMLookAtBoneApplyer - get eye target
		// foreach (Transform child in tempTransforms) 
		// {
		// 	Gizmos.color = this.Color;
		// 	float distance = Size / 1000.0f;

		// 	Gizmos.DrawSphere (child.position, distance);

		// 	Gizmos.DrawLine (child.position, child.parent.position);
		// }
	}
}
