using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Use this for initialization
    public GameObject PlayerReference;
    Vector3 CameraOffset;
	void Start () {
        CameraOffset = gameObject.transform.position - PlayerReference.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(CameraOffset + PlayerReference.transform.position != gameObject.transform.position)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, CameraOffset + PlayerReference.transform.position, Time.deltaTime * 6.0f);
        }
	}
}
