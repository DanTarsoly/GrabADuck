using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBehavior : MonoBehaviour
{

    public AudioClip[] PickClips;
    public AudioClip[] ScoreClips;

    private AudioSource Audio;
    private System.Random Randomizer;

    void Awake()
    {
        Randomizer = new System.Random(GetHashCode() + System.DateTime.Now.Millisecond);
        Audio = GetComponent<AudioSource>();
    }

    void Start() { }

    public void PlayPickSound()
    {
        Audio.clip = PickClips[Randomizer.Next(0, PickClips.Length)];
        Audio.pitch = 1 + (float)Randomizer.NextDouble() * 0.6f - 0.3f;
        Audio.Play();
    }

    public void PlayScoreSound()
    {
        Audio.clip = ScoreClips[Randomizer.Next(0, PickClips.Length)];
        Audio.pitch = 1 + (float)Randomizer.NextDouble() * 0.6f - 0.3f;
        Audio.Play();
    }
}
