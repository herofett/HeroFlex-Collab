using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public int BulletType;
    Vector3 BulletVelocity;
    RaycastHit BulletImpact;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ModifyVelocity();
        if(Physics.Raycast(gameObject.transform.position, BulletVelocity, out BulletImpact)) {
            if (BulletImpact.transform.gameObject.tag == "Enemy") {
                
            }
        }
	}

    void ModifyVelocity () {
        
    }
    
}
