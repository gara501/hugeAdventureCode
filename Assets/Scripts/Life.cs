using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    private bool collide = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            // This validation is to avoid more than one collision
            if (!collide)
            {
                Player.obj.AddLive();
                AudioManager.obj.playCoin();
                FxManager.obj.showPop(transform.position);
                gameObject.SetActive(false);
                collide = true;
            }
        }
    }
    
}
