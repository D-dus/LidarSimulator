using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Text;

public class RotateLidar : MonoBehaviour {
	public float mSpeed;
	public float mHitDistance;
	public float mAnglePosition;
	public Text mDebugText;
	private string mDataToSend;
	private byte[] mBufferToSend;
	private System.Object LockThis;
	//private Rigidbody mLidarRigidbody;
	// Use this for initialization
	void Start () 
	{
		//mLidarRigidbody = GetComponent<Rigidbody> ();
		LockThis = new System.Object ();
	}
	
	// Update is called once per frame

	void Update()
	{
		transform.Rotate (Vector3.up * Time.deltaTime*mSpeed);
		//mLidarRigidbody.angularVelocity = Vector3.up * mSpeed;
		mAnglePosition = transform.eulerAngles.y;
		
		lock (LockThis) 
		{
			mDataToSend= mAnglePosition + ", " + mHitDistance;
			mDebugText.text = mDataToSend;
			mBufferToSend=ASCIIEncoding.ASCII.GetBytes (mDataToSend);
		}

	}
	public void SetHitDistance(float iHitDistance)
	{
		mHitDistance = iHitDistance;
	}
	public byte[] GetBufferToSend()
	{
		return mBufferToSend;
	}
}
