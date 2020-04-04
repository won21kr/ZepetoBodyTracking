using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SnowBoneGizmosRenderer : MonoBehaviour
{
    // Start is called before the first frame update
	public Color jointColor = Color.yellow;
	public Color boneColor = Color.green;
	[Range(1.0f, 100.0f)]
	public float Size = 8.0f;
	public Transform rootBone;

	public void OnDrawGizmos() {
		// var skmr = this.GetComponentInChildren<SkinnedMeshRenderer> ();
		// Transform[] allJoints = skmr.bones;

		Transform[] allJoints = rootBone.GetComponentsInChildren<Transform>();
		foreach (var item in allJoints) {
			Gizmos.color = this.jointColor;
			float distance = Size / 1000.0f;

			Gizmos.DrawSphere (item.position, distance);

			Gizmos.color = this.boneColor;
			Gizmos.DrawLine (item.position, item.parent.position);
		}
	}
}
