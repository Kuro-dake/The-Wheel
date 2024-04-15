using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Activatable : MonoBehaviour
{
    public bool active { get; protected set; }
    public bool visible;
    
    private void OnMouseDown()
    {
        DeactivateAll();
        active = true;
        
    }

    public static void DeactivateAll()
    {
        FindObjectsByType<Activatable>(FindObjectsSortMode.None).ToList().ForEach((o) => o.Deactivate());
    }

    protected virtual void Update()
    {
        float target = visible ? (active ? 1f : .2f) : 0f;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        Color c = sr.color;
        c.a = Mathf.MoveTowards(c.a, target, Time.deltaTime);
        
        sr.color = c;

    } 
    
    protected virtual void Deactivate()
    {
        active = false;
    }
}
