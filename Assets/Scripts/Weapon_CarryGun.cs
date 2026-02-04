using UnityEngine;

public class Weapon_CarryGun : WeaponMechanic
{
	public static Weapon_CarryGun id { get; private set; }
	public override void ExtraRemoveCall()
	{
		Box_Drop();
	}

	public override void Use_Down()
	{
	}
	bool isPressingClick;
	public override void Use_Held()
	{
		if (isPressingClick) return;

		if (!boxHeld && boxAvailable && boxAvailable != boxHeld)
		{
			Box_Grab();
			return;
		}
		else if (boxHeld) { 
			Box_Drop();
			isPressingClick = true;
		}
	}
	public Transform pivot;
	void Box_Grab()
	{

		if (!Player.id.mController.isGrounded)
		{
			return;
		}
		boxHeld = boxAvailable;
		boxAvailable = null;
		boxHeld.mLight.BL_Set_PickedUp();
		boxHeld.SetKinematic(true);
		boxHeld.mRig.velocity = (Player.id.transform.position - boxHeld.transform.position) * 6f;
		isPressingClick = true;
	}
	public void Box_Drop()
	{
		if (boxHeld)
		{
			boxHeld.SetKinematic(false);

			boxHeld.mLight.BL_Set_NotLooked();
		}
		boxHeld = null;
		boxAvailable = null;
	}
	private void Start()
	{
		id = this;
	}
	private void Update()
	{
		if (boxHeld)
		{
			boxHeld.mRig.velocity *= 0.9f;
			boxHeld.mRig.velocity += (pivot.position - boxHeld.transform.position + Vector3.down);
		}

		if (Input.GetMouseButtonUp(0)) isPressingClick = false;
	}
	public override void Use_RightDown()
	{
		Box_Drop();
	}
	Box boxHeld;
	public Box boxAvailable;
	private void OnTriggerEnter(Collider other)
	{
		if (!boxHeld)
			if (other.TryGetComponent<Box>(out var getToTryCarry))
				try
				{
					if (boxAvailable)
						if(boxAvailable != getToTryCarry)
							boxAvailable.mLight.BL_Set_NotLooked();
					boxAvailable = getToTryCarry;
					boxAvailable.mLight.BL_Set_Highlighted();
				}
				catch { }

	}
	private void OnTriggerExit(Collider other)
	{
		try
		{
			if (other.gameObject == boxAvailable.gameObject)
				boxAvailable.mLight.BL_Set_NotLooked();
		}
		catch { }
	}
}