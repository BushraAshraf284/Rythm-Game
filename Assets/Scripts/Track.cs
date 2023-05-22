using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Track : MonoBehaviour
{
    public SongData song;
    public AudioSource source;
    public Block ColorAPrefab;
    public Block ColorBPrefab;
    bool songOver;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.forward * (song.speed * GameManager.Instance.startTime);
        Invoke(nameof(StartSong), GameManager.Instance.startTime - song.startTime);
        MakeTrack();
        //StartCoroutine(MakeTrack());
        songOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.back * song.speed * Time.deltaTime;
    }

    void StartSong()
    {
        Debug.Log("Play Spmg");
        source.PlayOneShot(song.clip);
        Invoke("SongOver", song.clip.length);
    }

   /* IEnumerator MakeTrack()
    {
        float beatLength = 60.0f / (float)song.bpm;
        float beatDist = beatLength * song.speed;
        float distance = 0;
        while (!songOver)
        {
            for (int i = 0; i < 60; i++)
            {
                var prefab = Random.Range(0, 2) == 0? ColorAPrefab : ColorBPrefab;
                var direction = Random.Range(-1, 1) == 0 ? 1 : -1;
                var pos = transform.position + new Vector3(direction, 1, distance * beatDist);
                var Block = Instantiate(prefab, pos, Quaternion.identity, transform);
                distance++;
            }
            Debug.Log("Track made");
            yield return 60;
        }
       
    }*/

    public void MakeTrack()
    {
        float beatLength = 60.0f / (float)song.bpm;
        float beatDist = beatLength * song.speed;
        int max = 0;
        for (int i = (int)song.startTime; i < song.clip.length; i++)
        {
           
            //var prefab = Random.Range(0, 2) == 0 ? ColorAPrefab : ColorBPrefab;
            Block prefab;
            float direction = 0;
            Vector3 pos;
            max = i < 30 ? 2 : 3;
            int random = Random.Range(0, max);
            if ( random == 0)
            {
                prefab = ColorAPrefab;
                direction = Random.Range(-1.0f, 0.1f);
                pos = transform.position + new Vector3(direction, 1, i * beatDist);
                var Block = Instantiate(prefab, pos, Quaternion.identity, transform);

            }
            else if(random == 1)
            {
                prefab = ColorBPrefab;
                direction = Random.Range(0.0f, 1.0f);
                pos = transform.position + new Vector3(direction, 1, i * beatDist);
                var Block = Instantiate(prefab, pos, Quaternion.identity, transform);
            }
            else
            {
                pos = transform.position + new Vector3(-1, 1, i * beatDist);
                Instantiate(ColorAPrefab, pos, Quaternion.identity, transform);
                pos = transform.position + new Vector3(1, 1, i * beatDist);
                Instantiate(ColorBPrefab, pos, Quaternion.identity, transform);
            }
            //float direction = Random.Range(-1.0f, 1.0f);
           
        }
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
