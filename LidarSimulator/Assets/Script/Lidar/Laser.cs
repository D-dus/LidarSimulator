using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	private LineRenderer mLaser;
	[SerializeField]
	private GameObject mLidar;

	private RotateLidar mLidarScript;
	// Use this for initialization
	void Start () 
	{
		mLaser = GetComponent<LineRenderer> ();
		mLaser.SetPosition (1, new Vector3 (0, 0, 5000));
		mLidarScript = mLidar.GetComponent<RotateLidar> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		RaycastHit lHit;
		if (Physics.Raycast (transform.position, transform.forward, out lHit)) {
			if (lHit.collider)
			{
				mLaser.SetPosition (1, new Vector3 (0, 0, lHit.distance));
				mLidarScript.SetHitDistance (lHit.distance);
			}
		} else
			mLaser.SetPosition (1, new Vector3 (0, 0, 5000));
	}
}
