using System.Collections;
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

    public void Create(int damage)
    {
        gameObject.transform.SetParent(GameObject.Find("Canvas").gameObject.transform);
        gameObject.transform.localScale = new Vector3(10, 10, 1);

        DamageOutput damageOutput = gameObject.GetComponent<DamageOutput>();
        damageOutput.Setup(damage);
    }

    public void Setup(int damage)
    {
        textMesh.SetText(damage.ToString());
        textColor = textMesh.color;
        disappearTime = DISAPPEAR_TIME_MAX;
    }
}
