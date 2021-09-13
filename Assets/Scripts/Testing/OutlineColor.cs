using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineColor : MonoBehaviour
{
    public Renderer rendCol;

    private void Start()
    {
        StartCoroutine(changeColor());
        //rendCol.material.SetColor("_OutlineColor", new Color32(244,3,12,255));
    }

    IEnumerator changeColor()
    {
        rendCol.material.SetColor("_OutlineColor", new Color32(0, 128, 0, 255)); //Green
        yield return new WaitForSeconds(0.2f);
        rendCol.material.SetColor("_OutlineColor", new Color32(191, 64, 191, 255)); //purple
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(changeColor());
    }
}
