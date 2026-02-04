using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class persecutorEntity : CheckpointDependent
{
    public Vector3 playerBreadCrumbOffset;
	private Vector3 current = Vector3.zero;
	private Vector3 next = Vector3.zero;

	public DetectPlayer detector;
	public float delayWalking;
	float delayWalking_current;
	Transform startPosition;
	bool activeOnStart;
	public override void Checkpoint_Load()
	{
		transform.position = startPosition.position;
		transform.rotation = startPosition.rotation;
		delayWalking_current = delayWalking;
		gameObject.SetActive(activeOnStart);
	}

	public override void Checkpoint_Save()
	{
		if(!startPosition)
			startPosition = new GameObject("Persecutor startposition").transform;
		startPosition.position = transform.position;
		startPosition.rotation = transform.rotation;

		current = transform.position;
		next = transform.position + transform.forward * 5;
		delayWalking_current = delayWalking;
		activeOnStart = gameObject.activeSelf;
	}

	// Start is called before the first frame Update
	void Start()
    {
		Checkpoint_Save();
		gameObject.SetActive(false);
    }
	private void GoToNextCrumb()
	{
		current = next;
		next = Player.id.transform.position + playerBreadCrumbOffset;
		normalWalk -= 1f;
	}
	private float normalWalk;
	private void WalkToCrumb()
	{
		if (delayWalking_current > 0)
		{
			delayWalking_current -= Time.deltaTime;
			return;
		}
		transform.position = Vector3.Lerp(current, next, normalWalk);
		normalWalk += Time.deltaTime;
		transform.LookAt(Player.id.transform.position - Vector3.up * 1.5f, Vector3.up);
		//transform.Rotate(Vector3.up * -Quaternion.LookRotation(next, Vector3.up).y, Time.deltaTime * 90f);
		if (normalWalk >= 1)
			GoToNextCrumb();
	}

    // Update is called once per frame
    void Update()
    {
		WalkToCrumb();

		if (detector.DetectPlayerOnce())
		{
			DeathAnimationControllers.id.persecutor.SetActive(true);
			Player.id.Player_Lock();
		}
    }
}
