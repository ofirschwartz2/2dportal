using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCreation : MonoBehaviour
{
    private int _lastPortalCreated;

    // Start is called before the first frame update
    void Start()
    {
        _lastPortalCreated = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int x = 5;//other.contacts[0].normal
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Ground"))
        {
            Vector2 portalNormal = collisionInfo.GetContact(0).normal;
            //לפתור את הבעייה שהפונקציה הזאת לא נקראת בכלל...
            //פה צריך ליצור את הפורטל שהוא לא האחרון שנוצר בזווית שהיא משיקה לפורטל נורמל

            Destroy(collisionInfo.gameObject);
        }
    }
}
