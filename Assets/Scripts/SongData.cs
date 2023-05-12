using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Song Data", menuName = "Song Data")]
public class SongData : ScriptableObject
{
    public AudioClip clip;
    public int bpm;
    public float startTime;
    public float speed;
    
}
