using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
     Quaternion currentRotation;
    Vector3 currentEulerAngles;
    public float speedTurn;
    float y=270;
 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
       
       
        if (Input.GetKey(KeyCode.L))
        {
            Debug.Log("L : Y était  "+y);
            y =  y-1f*speedTurn;
            if(y<0){
                 Debug.Log("Y vaut maintenant "+y);
                y=360;
            }
            Debug.Log(y);
            transform.rotation=Quaternion.Euler(0,y,0);
        }
         if (Input.GetKey(KeyCode.R))
        {
          
             y = 1f*speedTurn + y;
             Debug.Log("L : Y était  "+y);
              if(y>360){
                Debug.Log("Y vaut maintenant "+y);
                y=0;
            }
            Debug.Log(y);
             transform.rotation=Quaternion.Euler(0,y,0);
        }
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * 5f * Time.fixedDeltaTime * Input.GetAxis("Vertical"));
        transform.Translate(Vector3.right * 5f * Time.fixedDeltaTime * Input.GetAxis("Horizontal"));
    }
}
