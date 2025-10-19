using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerState = PlayerController.PlayerState;

public class AnimationController : MonoBehaviour
{
    Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
        _anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // Update animation based on player state
    public void UpdateAnimation(PlayerState playerState)
    {
        for (int i = 0; i <= (int)PlayerState.Moving; i++)
        {
            string stateName = ((PlayerState)i).ToString(); 
            if (playerState == (PlayerState)i)
            {
                _anim.SetBool(stateName, true);
            }
            else
            {
                _anim.SetBool(stateName, false);
            }
        }
    }
}
