using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{
    public Ghost ghost;
    public float timer;
    public float timeValue;


    void Awake(){
        ghost.isRecord = false;
        ghost.isReplay = false;
        if(ghost.isRecord){
            ghost.ClearData();
            timer=0;
            timeValue=0;
        }
    }

    public void Record(){
        ghost.ClearData();
        ghost.isRecord = true;
        ghost.isReplay = false;
    }
    void Update(){
        timer += Time.unscaledDeltaTime;
        timeValue += Time.unscaledDeltaTime;

        if(ghost.isRecord && timer >= 1/ghost.RecordFrequency){
            ghost.timeStamp.Add(timeValue);
            ghost.position.Add(this.transform.position);
            ghost.rotation.Add(this.transform.rotation);
            timer = 0;
        }
    }
}
