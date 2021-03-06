﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//animates the damage output
public class DamageOutput : MonoBehaviour
{
    private const float DISAPPEAR_TIME_MAX = 1f;

    private TextMeshPro textMesh;
    private Color textColor;
    private float disappearTime;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>(); 
    }

    private void Update()
    {
        float moveYSpeed = 20f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        if(disappearTime > DISAPPEAR_TIME_MAX * .5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale += Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTime -= Time.deltaTime;
        if(disappearTime < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Create(string damage, Color color)
    {
        DamageOutput damageOutput = gameObject.GetComponent<DamageOutput>();
        damageOutput.Setup(damage, color);
    }

    public void Setup(string damage, Color color)
    {
        textMesh.SetText(damage);
        textMesh.color = color;
        disappearTime = DISAPPEAR_TIME_MAX;
    }
}
