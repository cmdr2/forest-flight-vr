using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(MeshFilter))]
public class CombineMeshes : MonoBehaviour {
	/* dependencies */
	public Transform meshParent;
	public string meshName = "combined1";

	/* scratchpad */
	private MeshFilter[] meshFilters;
	private Vector3 p0, p1;
	private Quaternion r0, r1;
	private Vector3 s0, s1;

	void Start () {
		meshFilters = meshParent.GetComponentsInChildren<MeshFilter> (false);

		PushState ();

		var combine = new CombineInstance[meshFilters.Length];
		for (int i = 0; i < meshFilters.Length; i++) {
			combine [i].mesh = meshFilters [i].mesh;
			combine [i].transform = meshFilters [i].transform.localToWorldMatrix;
			meshFilters [i].gameObject.SetActive (false);
		}
		var mf = GetComponent<MeshFilter> ();
		mf.mesh = new Mesh ();
		mf.mesh.CombineMeshes (combine);

		PopState ();

		#if UNITY_EDITOR
		AssetDatabase.CreateAsset (mf.mesh, "Assets/" + meshName + ".asset");
		#endif
	}

	private void PushState() {
		p0 = meshParent.transform.position;
		r0 = meshParent.transform.rotation;
		s0 = meshParent.transform.localScale;
		meshParent.transform.position = Vector3.zero;
		meshParent.transform.rotation = Quaternion.identity;
		meshParent.transform.localScale = Vector3.one;

		p1 = transform.position; // fix for preserving transform
		r1 = transform.rotation;
		s1 = transform.localScale;
		transform.position = Vector3.zero; // fix for preserving transform
		transform.rotation = Quaternion.identity;
		transform.localScale = Vector3.one;
	}

	private void PopState() {
		meshParent.transform.position = p0;
		meshParent.transform.rotation = r0;
		meshParent.transform.localScale = s0;

		transform.position = p1;
		transform.rotation = r1;
		transform.localScale = s1;
	}
}
