using UnityEngine;
using System.Collections;

public class Plunger : MonoBehaviour {

	const float MAX_DISTANCE = 1f;
	const float PULL_SPEED = 100f;
	const float RESET_SPEED = 100f;

	public GameObject bottom;
	public float maxForce = 2200;

	bool _resetting;
	bool _active;
	Vector3 _startPos;
	Vector3 _bottomStartPos;


	// Use this for initialization
	void Start () {
		_active = true;
		_startPos = gameObject.transform.position;
		_bottomStartPos = bottom.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!_resetting && _active
		    && Input.GetKey(KeyCode.Space) 
		    && Mathf.Abs(gameObject.transform.position.z - _startPos.z) < 5f)
		{
			_resetting = false;
			Vector3 moveAmount = new Vector3(0f, 0f, -PULL_SPEED * Time.deltaTime);
			gameObject.transform.Translate(moveAmount);
			bottom.transform.Translate(moveAmount);
		}

		if (_active && Input.GetKeyUp(KeyCode.Space))
		{
            //if (Mathf.Abs(transform.position.z) - Mathf.Abs(bottom.transform.position.z) < 5f)
            //{
            //    float distance = Mathf.Abs(gameObject.transform.position.z - _startPos.z);
            //    bottom.GetComponent<Rigidbody>().AddForce(0f, 0f, maxForce * distance);
            //}
            _resetting = true;
			//_active = false;
		}

		if (_resetting)
		{
			if (gameObject.transform.position.z < _startPos.z)
			{
				Vector3 moveAmount = new Vector3(0f, 0f, RESET_SPEED * Time.deltaTime);
				gameObject.transform.Translate(moveAmount);
                gameObject.transform.position = _startPos;

			}
            else
                _resetting = false;
        }
	}

	public void Reload() {
		bottom.GetComponent<Rigidbody>().velocity = Vector3.zero;
		bottom.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		bottom.transform.localRotation = Quaternion.identity;
		bottom.transform.position = _bottomStartPos;
		_active = true;
	}

	public bool active {
		set {_active = value;}
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log("coll");
		_active = true;
	}
}
