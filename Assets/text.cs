using UnityEngine;

public class Test : MonoBehaviour
{
    public AudioSource audioSource;

    bool PlayerAtExit = false;
    bool HasAudioplayed;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == player)
        {
            PlayerAtExit = true;
        }
    }

    void Update()
    {
        if (PlayerAtExit)
        {
            if (!HasAudioplayed)
            {
                audioSource.Play();
                HasAudioplayed = true;
            }
        }
    }
}