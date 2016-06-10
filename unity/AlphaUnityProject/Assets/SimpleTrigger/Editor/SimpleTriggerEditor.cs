using UnityEngine;
using System.Collections;
using UnityEditor;

public class SimpleTriggerEditor : Editor {

	[MenuItem("Tools/Simple Trigger/Create Trigger")]
	static void Init()
	{
		GameObject spawnedTrigger = GameObject.CreatePrimitive (PrimitiveType.Cube);
		spawnedTrigger.name = "Untitled SimpleTrigger";
		spawnedTrigger.AddComponent<SimpleTrigger> ();
		spawnedTrigger.GetComponent<MeshRenderer> ().enabled = false;
		spawnedTrigger.GetComponent<BoxCollider> ().isTrigger = true;

		Selection.activeGameObject = spawnedTrigger.gameObject;
	}
}
