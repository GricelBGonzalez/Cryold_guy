using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(DD_DynamicItem))]
public class DD_Editor_DynamicItem : Editor
{
	DD_DynamicItem t;
	Vector2 otherAreasScroll;
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		t = (DD_DynamicItem)target;

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Randomize Color"))
			RandomizeColor();
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label(" ");
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Display");
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();

		if (GUILayout.Button("Show all"))
			ShowAll();
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Hide all but this"))
			HideAllButThis();
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Set all in place"))
			SetAllInPlace();
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label(" ");
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Add trigger");
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Add Trigger3D"))
			Add3D();
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Add Trigger2D"))
			Add2D();
		GUILayout.EndHorizontal();


		GUILayout.BeginHorizontal();
		GUILayout.Label(" ");
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Other dynamic items...");
		GUILayout.EndHorizontal();

		GUILayout.BeginScrollView(otherAreasScroll);
		CheckOthers();
		GUILayout.EndScrollView();
	}

	private void CheckOthers()
	{
		GUILayout.BeginVertical();
		var list = FindObjectsOfType<DD_DynamicItem>(true);
		foreach (var item in list)
		{
			if (item == t)
				continue;
			if (GUILayout.Button(item.name + ":   " + (item.gameObject.activeSelf? " " : "(Unloaded)")))
				Selection.activeGameObject = item.gameObject;
		}

		GUILayout.EndVertical();
	}

	private void RandomizeColor()
	{
		t.zoneColor = Random.ColorHSV();
		t.zoneColor.a = 1f;
	}

	private void SetAllInPlace()
	{
		var objs = FindObjectsOfType<DD_DynamicItem>(true);
		foreach (var item in objs)
			item.gameObject.SetActive(item.startEnabled);
	}
	private void HideAllButThis()
	{
		var objs = FindObjectsOfType<DD_DynamicItem>(true);
		foreach (var item in objs)
			item.gameObject.SetActive(item.startEnabled);
		t.gameObject.SetActive(true);
	}

	private void ShowAll()
	{
		var objs = FindObjectsOfType<DD_DynamicItem>(true);
		foreach (var item in objs)
			item.gameObject.SetActive(true);
	}

	DD_Trigger AddBase()
	{
		t.gameObject.SetActive(t.startEnabled);
		var name = "Dynamic Item Triggers";
		var parent = FindObjectOfType<DD_TriggerParent>(true);
		if (!parent)
			parent = (new GameObject(name)).AddComponent<DD_TriggerParent>();


		var child = (new GameObject("Item trigger: " + t.name)).AddComponent<DD_Trigger>();
		child.load = t;


		var getAllElse = FindObjectsOfType<DD_DynamicItem>(true);
		List<DD_DynamicItem> makeArrayOfDeloads = new();
		foreach (var item in getAllElse)
			if (item != t)
				makeArrayOfDeloads.Add(item);
		child.deload = makeArrayOfDeloads.ToArray();


		Selection.activeGameObject = child.gameObject;
		child.transform.parent = parent.transform;
		child.transform.position = t.transform.position;
		return child;
	}

	void Add3D()
	{
		var add = AddBase().gameObject.AddComponent<BoxCollider>();
		add.size = Vector3.one * 4f;
		add.isTrigger = true;
	}
	void Add2D()
	{
		var add = AddBase().gameObject.AddComponent<BoxCollider2D>();
		add.size = Vector3.one;
		add.isTrigger = true;
	}
}