using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class EnemyController : MonoBehaviour {
   
    public GameObject playerObject;

    private float distance; //distance between enemy and player
    public float deAggroRange; //distance to lose aggro

 	public float currentHealth;
	public float maxHealth;
	public float speed;      //Speed of chase

	private bool isAggro = false; //not aggroed

	private Transform t;
	private Transform player;

	private Transform spawnPoint;

	private float spawnX;
	private float spawnY;
	private float spawnZ;





	private float shotInterval; //time between shots
	public float shotDelay; 

	public Rigidbody enemyBullet;
	public Transform enemyBulletSpawnPoint;

	public float enemyBulletVelocity;

	private bool enemyCanShoot = true;


    void Start ()
    {

    	//spawnX = this.transform.position.x;
    	//spawnY = this.transform.position.y;
    	//spawnZ = this.transform.position.z;

    	//spawnPoint.transform.position = new Vector3(spawnX, spawnY, spawnZ);

    	t = this.transform;


    	player = GameObject.FindGameObjectWithTag("Player").transform;

		currentHealth = maxHealth;


    }
   
    void Update () {

    	Distance(); // get distance between player and enemy

    	DeAggro(); //lose aggro on player if they are too far away



		if(isAggro == true)
		{
			ChasePlayer(); //move towards player
		}
    	

    }





    void ChasePlayer()
    {
        Vector3 localPosition = player.position - transform.position;
        localPosition = localPosition.normalized; // The normalized direction in LOCAL space
        transform.Translate(localPosition.x * Time.deltaTime * speed, localPosition.y * Time.deltaTime * speed, localPosition.z * Time.deltaTime * speed);
    }

    void returnHome()
    {
        Vector3 localPosition = spawnPoint.position - transform.position;
        localPosition = localPosition.normalized; // The normalized direction in LOCAL space
        transform.Translate(localPosition.x * Time.deltaTime * speed, localPosition.y * Time.deltaTime * speed, localPosition.z * Time.deltaTime * speed);	
    }


	public void Aggro()
	{
			isAggro = true;

			Vector3 dir = player.position - transform.position;    //Face player
    		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
    		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    		Vector3 eulerAngleOffset = new Vector3(0, 0, 90);  //Rotate to face player
       		transform.Rotate(eulerAngleOffset, Space.Self);


			transform.position -= transform.up * (speed * Time.deltaTime);
	

		
			if (shotInterval <= 0)
			{
				Rigidbody enemyBulletInstance = Instantiate(enemyBullet, enemyBulletSpawnPoint.position, Quaternion.Euler(new Vector3(0,0, transform.localEulerAngles.z))) as Rigidbody;

				enemyBulletInstance.GetComponent<Rigidbody>().AddForce(enemyBulletSpawnPoint.right * enemyBulletVelocity);

				enemyCanShoot = false;
				shotInterval = shotDelay;


			} else {

				shotInterval -= Time.deltaTime;
			}

    }


    public void DeAggro() // check to see if player is too far and lose aggro if true
    {
    	//Debug.Log(distance);
    	if (distance > deAggroRange)
    	{
    		isAggro = false;
    		returnHome();
    		Debug.Log("OUT OF AGGRO RANGE!");
    	}

    }


     private float Distance() // get distance between player and enemy
     {
         return distance = Vector3.Distance(t.position, player.position);
     }

}