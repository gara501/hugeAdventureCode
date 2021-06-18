using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Rigidbody2D rb;
    private bool isOnPlatform = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isOnPlatform)
            {
                transform.Translate(new Vector3(0f, -0.1f, 0f));
            }
            
            Invoke("DropPlatform", 0.5f);
            Destroy(gameObject, 2f);
            isOnPlatform = true;
        }
        else
        {
            isOnPlatform = false;
            transform.Translate(new Vector3(0f, 0f, 0f));
        }
    }

    // Update is called once per frame
    void DropPlatform()
    {
        rb.isKinematic = false;
    }
}
