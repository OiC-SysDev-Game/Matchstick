using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderUniqueCode : MonoBehaviour
{
	public string code { get; private set; }

	private void Awake()
	{
		code = GetHierarchyPath(this.transform);
		Debug.Log("CUC : " + code);
	}

	private string GetHierarchyPath(Transform self)
	{
		string path = self.gameObject.name;
		Transform parent = self.parent;
		while (parent != null)
		{
			path = parent.name + "/" + path;
			parent = parent.parent;
		}
		return path;
	}
}
