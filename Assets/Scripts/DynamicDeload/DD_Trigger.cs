using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DD_Trigger :  MonoBehaviour
{
	public DD_DynamicItem load;
	public DD_DynamicItem[] deload;

	private void ActivateIf_ThisGameobjectIsController(GameObject other)
	{
		if (!other.GetComponent<DD_DynamicEntityContoller>())
			return;
		try
		{
			load.gameObject.SetActive(true);
		}
		catch
		{

		}
		try
		{
			foreach (var i in deload)
				i.gameObject.SetActive(false);

		}
		catch
		{

		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		ActivateIf_ThisGameobjectIsController(collision.gameObject);	
	}
	private void OnTriggerEnter(Collider other)
	{
		ActivateIf_ThisGameobjectIsController(other.gameObject);
	}
	private void OnDrawGizmos()
	{
		if (!load)
			return;

		Gizmos.color = load.zoneColor;
		Gizmos.DrawLine(transform.position, load.transform.position);
	}
}
