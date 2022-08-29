using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    
    [SerializeField] private GameObject player;
    public GameObject Indicator;

    Renderer rd;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Renderer>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(rd.isVisible == false)
        {
            if(Indicator.activeSelf == false)
            {
                Indicator.SetActive(true);
            }
            
            RaycastHit hit;

            Vector3 dir = player.transform.position - transform.position;

            if(Physics.Raycast(transform.position, dir, out hit,10)){
                if(hit.collider != null){
                    Indicator.transform.position = hit.point;
                }
            }
        }else{
            if(Indicator.activeSelf == true)
            {
                Indicator.SetActive(false);
            }
        }
    }
}
