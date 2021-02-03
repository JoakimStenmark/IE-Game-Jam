using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    public bool isDirty;
    Color clean = Color.cyan;
    Color dirty = Color.gray;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetClean()
    {
        isDirty = false;
        spriteRenderer.color = clean;

    }

    public void SetDirty()
    {
        isDirty = true;
        spriteRenderer.color = dirty;
    }

}
