using System.Collections.Generic;
using UnityEngine;

public class NectarIndicator : MonoBehaviour
{

    // Generators
    [SerializeField] NectarGenerator nectarGenerator;

    public float alpha = 0f;


    //Indicator children
    public List<SpriteRenderer> rendererArray = new List<SpriteRenderer>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nectarGenerator.nectarGenerated.AddListener(fadeIndicator);

    }


    public void fadeIndicator(int nectar) 
    {

        alpha = (float)nectar / (float)nectarGenerator.GetMaxNectar();
        rendererArray[0].color = new Color(1f, 1f, 1f, 1-alpha);
        rendererArray[1].color = new Color(1f, 1f, 1f, alpha);
    }

}
