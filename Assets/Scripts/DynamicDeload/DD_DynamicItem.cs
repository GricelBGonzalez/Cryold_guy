using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class DD_DynamicItem : MonoBehaviour
{
	public bool startEnabled;
	public Color zoneColor;
	private Bounds extents;
	private void OnDrawGizmos()
	{
		if (Application.isPlaying)
			goto draw;
		Vector3 _minPos = Vector3.one * float.MaxValue;
		Vector3 _maxPos = Vector3.one * float.MinValue;
		var allChildren = transform.GetComponentsInChildren<Transform>();
		foreach (var item in allChildren)
		{
			Vector3 _minSize = item.position;
			Vector3 _maxSize = item.position;
			if(item.TryGetComponent<Renderer>(out var rend))
			{
				_minSize = rend.bounds.min;
				_maxSize = rend.bounds.max;
			}
			if (_minSize.x < _minPos.x)
				_minPos.x = _minSize.x;
			if (_minSize.y < _minPos.y)
				_minPos.y = _minSize.y;
			if (_minSize.z < _minPos.z)
				_minPos.z = _minSize.z;


			if (_maxSize.x > _maxPos.x)
				_maxPos.x = _maxSize.x;
			if (_maxSize.y > _maxPos.y)
				_maxPos.y = _maxSize.y;
			if (_maxSize.z > _maxPos.z)
				_maxPos.z = _maxSize.z;

		}

		extents.min = _minPos;
		extents.max = _maxPos;

	draw:
		zoneColor.a = 1f;
		Gizmos.color = zoneColor;

		Gizmos.DrawWireCube(extents.center, extents.size);
	}
}
