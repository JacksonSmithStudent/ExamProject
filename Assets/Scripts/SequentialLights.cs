using UnityEngine;
using System.Collections;

public class SequentialLights : MonoBehaviour
{
    public float stepDelay = 0.08f;
    public float intervalDelay = 15f;
    public float sequenceDuration = 10f;
    public float spawnRate = 1f;
    public float postSequenceDelay = 25f;

    public CubeSpawner cubeSpawner;
    public Light directionalLight;

    [Header("Audio")]
    public AudioSource mainMusic;
    public AudioSource sequenceAudio;

    Light[] lights;
    int sequenceCount = 0;

    Color originalAmbientColor;
    float originalDirectionalIntensity;

    void Start()
    {
        lights = GetComponentsInChildren<Light>();

        foreach (Light l in lights)
            l.enabled = false;

        originalAmbientColor = RenderSettings.ambientLight;

        if (directionalLight != null)
            originalDirectionalIntensity = directionalLight.intensity;

        StartCoroutine(MainLoop());
    }

    IEnumerator MainLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalDelay);

            // --- START SEQUENCE ---
            RenderSettings.ambientLight = Color.black;

            if (directionalLight != null)
                directionalLight.intensity = 0f;

            // Audio control
            if (mainMusic != null && mainMusic.isPlaying)
                mainMusic.Pause();

            if (sequenceAudio != null && !sequenceAudio.isPlaying)
                sequenceAudio.Play();

            sequenceCount++;
            yield return StartCoroutine(ActiveSequence(sequenceCount));

            if (cubeSpawner != null)
                cubeSpawner.SpawnCube();

            yield return new WaitForSeconds(postSequenceDelay);

            // --- END SEQUENCE ---
            RenderSettings.ambientLight = originalAmbientColor;

            if (directionalLight != null)
                directionalLight.intensity = originalDirectionalIntensity;

            // Audio reset
            if (sequenceAudio != null)
                sequenceAudio.Stop();

            if (mainMusic != null)
                mainMusic.UnPause();
        }
    }

    IEnumerator ActiveSequence(int sequenceID)
    {
        float endTime = Time.time + sequenceDuration;

        while (Time.time < endTime)
        {
            StartCoroutine(LightSweep(sequenceID));
            yield return new WaitForSeconds(spawnRate);
        }
    }

    IEnumerator LightSweep(int sequenceID)
    {
        for (int i = 0; i < lights.Length; i++)
        {
            if (i > 0)
                lights[i - 1].enabled = false;

            lights[i].enabled = true;
            yield return new WaitForSeconds(stepDelay);
        }

        if (lights.Length > 0)
            lights[lights.Length - 1].enabled = false;
    }
}
