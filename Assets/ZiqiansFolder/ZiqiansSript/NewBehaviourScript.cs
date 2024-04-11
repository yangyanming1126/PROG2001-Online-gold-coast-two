using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Opendoor : MonoBehaviour{ 

  public AudioClip OpenDoorSound;
  private AudioSource source;


  void Start(){ 
    source = GetComponent<AudioSource>();
    }

  //Update is called once per frame
  void Update(){ 
    if (PlayerController.Process == "MissionAception")
    {
        source.PlayOneShot(OpenDoorSound, 1F);
    }
  }
}
 