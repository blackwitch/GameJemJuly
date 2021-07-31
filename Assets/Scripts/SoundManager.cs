using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Clip
{
    public string Name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public List<Clip> clips = new List<Clip>();

    public static SoundManager Instance { get; private set; } = null;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ChangeClip("¸ÞÀÎ");
    }

    public void ChangeClip(string name, bool loop = false)
    {
        Clip find = clips.Find((o) => { return o.Name == name; });
        if (find != null)
        {
            audioSource.Stop();
            audioSource.clip = find.clip;
            audioSource.loop = loop;
            audioSource.Play();
        }
    }
}
