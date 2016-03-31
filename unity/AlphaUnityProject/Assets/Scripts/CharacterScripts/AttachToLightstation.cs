using UnityEngine;
using System.Collections;

public class AttachToLightstation : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MiMi")
        {
            //SlowMiMiMovement(); 
            //TriggerMiMiAnimation();
            //PlaySound();
            //MakeChanelPossible(); 

            // BUGS FROM MARTIN PLAYTESTING // 
            /* 
               Camera bounding, collision,  
               Lightstations slow, 
               Speed is okay, might need testing some more, 
               enemies are annoying, 
               Signal which pad belongs to which character, 
               Zoom out based on player distance, 
               rescue of MiMi 
               Scrap monitors, focus on level design   
            */
        }
    }
}