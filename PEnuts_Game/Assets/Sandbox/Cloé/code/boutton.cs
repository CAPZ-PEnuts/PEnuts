﻿using System.Collections;
using System.Threading; 
using System.Collections.Generic;
using UnityEngine;

public class boutton : MonoBehaviour
{
    public signalhandlorder signal;


    void OnTriggerEnter(Collider other)

    {
        if (other.gameObject.tag == "Player")
        {
            signal.buttonpush(this);
            FindObjectOfType<AudioManager>().Play("signalclique");
        }


    }
}
