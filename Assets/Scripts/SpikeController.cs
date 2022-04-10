using System;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        DealPlayerDamage(col);
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        DealPlayerDamage(col);
    }

    private void DealPlayerDamage(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<PlayerHealthController>().DealDamage();
        }
    }
}