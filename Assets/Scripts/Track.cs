using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public SongData song;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.forward * (song.speed * GameManager.Instance.startTime);
        Invoke(nameof(StartSong), GameManager.Instance.startTime - song.startTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.back * song.speed * Time.deltaTime;
    }

    void StartSong()
    {
        source.PlayOneShot(song.clip);
        Invoke("SongOver", song.clip.length);
    }

    private void OnDrawGizmos()
    {
        for(int i =0; i< 100; i++)
        {
            float beatLength = 60.0f / (float)song.bpm; // beat per second
            float beatDist = beatLength * song.speed; // distance between each beat
            Gizmos.DrawLine(transform.position + new Vector3(-1, 0, i * beatDist), transform.position + new Vector3(1, 0, i * beatDist));
        }
    }
}
