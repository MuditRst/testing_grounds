using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ghost : ScriptableObject
{
    public bool isRecord=false;
    public bool isReplay=false;
    public float RecordFrequency;

    public List<float> timeStamp;
    public List<Vector3> position;
    public List<Quaternion> rotation;

    public void ClearData()
    {
        timeStamp.Clear();
        position.Clear();
        rotation.Clear();
    }
}
