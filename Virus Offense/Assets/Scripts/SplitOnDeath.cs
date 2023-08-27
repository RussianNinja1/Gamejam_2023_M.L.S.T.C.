using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SplitOnDeath : MonoBehaviour
{
    [SerializeField] GameObject basicEnemy;

    private int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        enemyCount = Random.Range(3, 7);
    }

    public void SplitEnemy()
    {

        for (int n = 0; n < enemyCount; n++)
        {
            GameObject newEnemy = Instantiate(basicEnemy, transform.position, transform.rotation) as GameObject;
            newEnemy.transform.localScale = newEnemy.transform.localScale * 0.8f;
            newEnemy.GetComponent<EnemyMovement>().GainTarget(GetComponent<EnemyMovement>().ReturnTarget());
        }
    }
}
