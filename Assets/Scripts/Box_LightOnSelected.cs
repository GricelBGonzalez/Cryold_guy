using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_LightOnSelected : MonoBehaviour
{
	[SerializeField] private Light boxLight_Light;


	[SerializeField] private BoxLight_Values boxLight_highlited;
	[SerializeField] private BoxLight_Values boxLight_notLooked;
	[SerializeField] private BoxLight_Values boxLight_picked_up;


	public void BL_Set_Highlighted() => boxLight_highlited.BLVal_SetLight(boxLight_Light);
	public void BL_Set_NotLooked() => boxLight_notLooked.BLVal_SetLight(boxLight_Light);
	public void BL_Set_PickedUp() => boxLight_picked_up.BLVal_SetLight(boxLight_Light);


	[System.Serializable] internal class BoxLight_Values
	{
		[SerializeField] public float bLVal_intensity = 1f;
		[SerializeField] public Color bLVal_color = Color.white;

		public void BLVal_SetLight(Light light)
		{
			light.intensity = bLVal_intensity;
			light.color = bLVal_color;
		}
	}
}
