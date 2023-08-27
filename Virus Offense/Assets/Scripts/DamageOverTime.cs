using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    private PlayerHealth playerHealth;

    [Header("D.O.T. Effects")]

    //damage to health
    [SerializeField] private float damageToHealthValue = 5f;
    //time to wait between ticks

    [SerializeField] private float waitForSecondValue = 0.75f;
    public List<int> dOTTickTimers = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // function to run the damage over time Coroutine, also checks to see if one is running and added more DOT to the cue.
    public void ApplyDOT(int ticks)
    {
        if(dOTTickTimers.Count <= 0)
        {
            dOTTickTimers.Add(ticks);
            StartCoroutine(DOT());
        }
        else
        {
            dOTTickTimers.Add(ticks);
        }
    }

    //The Damage over time enum, runs a while loop that counts down and deletes DOT stacks from the DOT Tick Timers list when they hit zero
    //also does damage to player
    IEnumerator DOT()
    {
        while(dOTTickTimers.Count > 0)
        {
            for(int i = 0; i < dOTTickTimers.Count; i++)
            {
                dOTTickTimers[i]--;
            }
            playerHealth.UpdateHealth(damageToHealthValue, false);
            //playerHealth.currentHealth -= damageToHealthValue;
            dOTTickTimers.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(waitForSecondValue);
        }
    }
}
