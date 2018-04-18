using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    //find target 
    //lerp to target
    //destroy yourself after a certain time
    //explode
    public GameObject Target;
    public GameObject Explosion;
    public float speef=0.1f;
	void Start ()
    {
        
    }
	
	
	void Update ()
    {
        if (Target != null)
        {
            if (Vector3.Distance(this.transform.position, Target.transform.position) > 5)
            {

                transform.position = Vector3.MoveTowards(this.transform.position, Target.transform.position, speef*Time.deltaTime);
                //speef += 0.1f;
            }
            if(Vector3.Distance(this.transform.position,Target.transform.position)<=0.5f)
            {
                Instantiate(Explosion,this.transform.position,Quaternion.identity);
                Destroy(this.gameObject);
            }

        }


    }
}
