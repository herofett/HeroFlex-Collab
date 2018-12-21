using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float PlayerSpeed;
    Vector3 PlayerVelocity = Vector3.zero;
    Vector3 PlayerDirection = Vector3.forward;
    Vector3 PreviousInput = Vector3.zero;
    LayerMask IgnoreEntities;
    public Rigidbody PlayerBody;

    Ray MouseRay;
    RaycastHit Point;


        //AI STUFFF
    public float aggroRange; //Distance to aggro/chase
    public bool isAggro = false; //not aggroed

    public LayerMask Enemy;  //Layer to find targets on
    public Transform target;      //nearest target




	void Start () {
        IgnoreEntities = LayerMask.GetMask("Level");
        PlayerBody = GetComponent<Rigidbody>();
        PlayerSpeed = 4;

}
	
	void Update () {
        PlayerVelocity.x = Input.GetAxis("Horizontal") * PlayerSpeed;
        PlayerVelocity.z = Input.GetAxis("Vertical") * PlayerSpeed;
        PlayerBody.velocity = PlayerVelocity;
        MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(MouseRay, out Point, 100000.0f, IgnoreEntities))
        {
            PlayerDirection = new Vector3(Point.point.x - gameObject.transform.position.x, 0, Point.point.z - gameObject.transform.position.z);
        }
        gameObject.transform.rotation = Quaternion.LookRotation(PlayerDirection);

        AggroField(); // Check for enemies


    }



    void AggroField()
    {
        Collider[] nearEnemy = Physics.OverlapSphere(transform.position, aggroRange, Enemy); //Create aggro field
        foreach (Collider hit in nearEnemy)   //Find nearest enemy
        {
            target = nearEnemy[0].transform; //nearest target
            target.GetComponent<EnemyController>().Aggro();

            isAggro = true;                         //aggro
        }
        
        if (nearEnemy.Length == 0)
        {
            isAggro = false;
        }
    }
}
