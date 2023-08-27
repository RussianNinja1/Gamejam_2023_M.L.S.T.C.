using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoisonArea : MonoBehaviour
{
    [SerializeField] private int NumOfDOT = 4; // amount of tick applied per trigger instance
    

    // simple trigger area for apply poison damage
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.GetComponent<DamageOverTime>() !=null)
        {
            other.GetComponent<DamageOverTime>().ApplyDOT(NumOfDOT);
        }
    }
}
