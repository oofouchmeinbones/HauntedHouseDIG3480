using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorModification : MonoBehaviour
{
    public GameEnding gameEnding;
    public Transform player;
    bool m_IsPlayerInRange;
    bool usingShield = false;
    bool hasShield = true;
    float shieldTimer = 0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Shield()
    {
        if (hasShield == true && Input.GetKeyDown(KeyCode.Space))
        {
            usingShield = true;
            shieldTimer = 0f;
            Debug.Log("Using Shield!");
        }

        if (usingShield)
        {
            shieldTimer += Time.deltaTime;

            // After 5 seconds, turn shield off
            if (shieldTimer >= 5f)
            {
                usingShield = false;
                hasShield = false;     
                Debug.Log("Shield Ran Out!");
            }
        }
    }

        void Update()
        {
        Shield();
            if (m_IsPlayerInRange)
            {
                Vector3 direction = player.position - transform.position + Vector3.up;
                Ray ray = new Ray(transform.position, direction);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit))
                {
                    if (raycastHit.collider.transform == player && usingShield == false)
                    {
                        gameEnding.CaughtPlayer();
                    }
                    else if (raycastHit.collider.transform == player && usingShield == true)
                    {
                        Debug.Log("Shielded!");
                    }
                }
            }
        }
    }
