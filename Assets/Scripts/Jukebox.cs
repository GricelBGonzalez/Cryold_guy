using UnityEngine;

public class Jukebox : MonoBehaviour
{
    public AudioSource mAudio;
    public AudioClip[] audioClips;
    public float minWaitTime;
    public float maxWaitTime;

    public float minPitch;
    public float maxPitch;

    private int _trackBeingPlayed = 0;
    float delay;
	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.N))
        {
            _trackBeingPlayed = _trackBeingPlayed == audioClips.Length - 1? 0: _trackBeingPlayed + 1;
            goto playMusic;
        }
        if (Input.GetKeyDown(KeyCode.M))
            goto randomMusic;

        if (mAudio.isPlaying)
            return;
        delay -= Time.deltaTime;
        if (delay > 0)
            return;

        randomMusic:
		_trackBeingPlayed = Random.Range(0, audioClips.Length);
        

        playMusic:
        RandomizeDelay();

        var randomPitch = Random.Range(minPitch, maxPitch);
        mAudio.pitch = randomPitch;
        mAudio.clip = audioClips[_trackBeingPlayed];
        mAudio.Play();
	}
    public void RandomizeDelay()
	{
        delay = Random.Range(minWaitTime, maxWaitTime);
    }
	private void Start()
	{
        RandomizeDelay();
	}
}
