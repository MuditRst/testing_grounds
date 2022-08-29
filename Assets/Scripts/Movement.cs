using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    float speed = 2f;
    float dr = 0;

    

    public Vector3 dis;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            dr = Input.GetAxisRaw("Horizontal")*90;
            
        }

        if(Input.GetAxisRaw("Vertical") < 0)
        {
            dr = Input.GetAxisRaw("Vertical") * 180;
            
        }else{
            dr = 0;
        }
       // StartCoroutine(DelayFall());
        Mathf.Clamp(dr, -90, 90);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, dr, 0), 360 * Time.deltaTime);
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * Time.deltaTime * speed;
    }


    IEnumerator DelayFall(){
            yield return new WaitForSeconds(5f);
            player.GetComponent<Rigidbody>().useGravity = true;
    }
}
