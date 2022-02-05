using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Behavior : MonoBehaviour
{
    //1
    void OnCollisionEnter(Collision collision)
    // Start is called before the first frame update
    {
        //2
        if(collision.gameObject.name == "Player")

    // Update is called once per frame
        {
            //3
            Destroy(this.transform.parent.gameObject);
            Debug.Log("Item collected!");
        } 
    }
}
