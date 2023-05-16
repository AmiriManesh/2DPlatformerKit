using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
    [Header("Sound Bank")]
    [SerializeField] private AudioClip[] sound1;
    [SerializeField] private float sound1Volume = 1f;
    [SerializeField] private AudioClip[] sound2;
    [SerializeField] private float sound2Volume = 1f;
    [SerializeField] private AudioClip[] sound3;
    [SerializeField] private float sound3Volume = 1f;
    [SerializeField] private AudioClip[] sound4;
    [SerializeField] private float sound4Volume = 1f;
    [SerializeField] private AudioClip[] sound5;
    [SerializeField] private float sound5Volume = 1f;
    [SerializeField] private AudioClip[] sound6;
    [SerializeField] private float sound6Volume = 1f;
    [SerializeField] private AudioClip[] sound7;
    [SerializeField] private float sound7Volume = 1f;
    [SerializeField] private AudioClip[] sound8;
    [SerializeField] private float sound8Volume = 1f;
    [SerializeField] private AudioClip[] sound9;
    [SerializeField] private float sound9Volume = 1f;
    [SerializeField] private AudioClip[] sound10;
    [SerializeField] private float sound10Volume = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlaySound1()
    {
        NewPlayer.Instance.sfxAudiosource.PlayOneShot(sound1[Random.Range(0,sound1.Length)] , sound1Volume);
    }
    public void PlaySound2()
    {
        NewPlayer.Instance.sfxAudiosource.PlayOneShot(sound2[Random.Range(0,sound2.Length)] , sound2Volume);
    }
    public void PlaySound3()
    {
        NewPlayer.Instance.sfxAudiosource.PlayOneShot(sound3[Random.Range(0,sound3.Length)] , sound3Volume);
    }
    public void PlaySound4()
    {
        NewPlayer.Instance.sfxAudiosource.PlayOneShot(sound4[Random.Range(0,sound4.Length)] , sound4Volume);
    }
    public void PlaySound5()
    {
        NewPlayer.Instance.sfxAudiosource.PlayOneShot(sound5[Random.Range(0,sound5.Length)] , sound5Volume);
    }
    public void PlaySound6()
    {
        NewPlayer.Instance.sfxAudiosource.PlayOneShot(sound6[Random.Range(0,sound6.Length)] , sound6Volume);
    }
    public void PlaySound7()
    {
        NewPlayer.Instance.sfxAudiosource.PlayOneShot(sound7[Random.Range(0,sound7.Length)] , sound7Volume);
    }
    public void PlaySound8()
    {
        NewPlayer.Instance.sfxAudiosource.PlayOneShot(sound8[Random.Range(0,sound8.Length)] , sound8Volume);
    }
    public void PlaySound9()
    {
        NewPlayer.Instance.sfxAudiosource.PlayOneShot(sound9[Random.Range(0,sound9.Length)] , sound9Volume);
    }
    public void PlaySound10()
    {
        NewPlayer.Instance.sfxAudiosource.PlayOneShot(sound10[Random.Range(0,sound10.Length)] , sound10Volume);
    }

}
