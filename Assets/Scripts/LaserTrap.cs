using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : TrapActivator
{
    public DetectPlayer detector;
    public AudioSource laserActivationSound;
    public bool startActivated;
    private bool activated = true;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] Material mat_Activated;
    [SerializeField] Material mat_Deactivated;

    [SerializeField] GameObject setActive;
	public override void SwitchPress()
	{
        activated= !activated;

        var matSel = activated ? mat_Activated : mat_Deactivated;
        Material[] mat = { matSel };
        setActive.SetActive(activated);
		mesh.SetMaterials(new List<Material>(mat));
		if (activated && laserActivationSound.gameObject.activeSelf)
            laserActivationSound.Play();
	}
	private void Start()
	{
        activated= !startActivated;
        laserActivationSound.gameObject.SetActive(false);
        SwitchPress();
		laserActivationSound.gameObject.SetActive(true);

	}

	// Update is called once per frame
	void Update()
    {
        if (detector.DetectPlayerOnce())
        {
            Player.id.Player_Lock();
            DeathAnimationControllers.id.laser.SetActive(true);
        }
    }
}
