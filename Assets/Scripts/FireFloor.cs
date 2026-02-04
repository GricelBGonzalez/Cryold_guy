using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFloor : MonoBehaviour
{
	public DetectPlayer detector;

	private void Update()
	{
		if(detector.playerDetected)
		{
			detector.DetectPlayerOnce().Player_Lock();
			DeathAnimationControllers.id.fire.SetActive(true);
		}
	}

}
