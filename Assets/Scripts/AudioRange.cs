using UnityEngine;

public class AudioRange : MonoBehaviour
{
    public Transform listener;     
    public float maxDistance = 20f; 
    public float minDistance = 1f;  

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        
        if (listener == null)
        {
            listener = Camera.main.transform;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, listener.position);

        if (distance >= maxDistance)
        {
            audioSource.volume = 0f;
        }
        else
        {  
            float volume = 1 - Mathf.InverseLerp(minDistance, maxDistance, distance);
            audioSource.volume = volume;
        }
    }
}
