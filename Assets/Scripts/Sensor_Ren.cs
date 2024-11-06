using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor_Ren : MonoBehaviour
{
    private int m_ColCount = 0;
    private float m_DisableTimer;

    private void OnEnable()
    {
        m_ColCount = 0;
    }

    public bool State()
    {
        if(m_DisableTimer > 0)
        {
            return false;
        }
        return m_ColCount > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_ColCount++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_ColCount--;
    }

    void Update()
    {
        m_DisableTimer -= Time.deltaTime;
    }

    public void Disable(float duration)
    {
        m_DisableTimer = duration;
    }
}