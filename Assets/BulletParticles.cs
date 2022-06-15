using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticles : MonoBehaviour
{


    [SerializeField] ParticleSystem.MainModule pS;
    Gradient gradient;
    GradientColorKey[] myGradientColors;
    GradientAlphaKey[] myGradientAlphas;
   
    public void AssignGradient(Color[] colors)
    {
        gradient = new Gradient();
        float alpha = 1.0f;
        myGradientColors = new GradientColorKey[colors.Length];
        myGradientAlphas = new GradientAlphaKey[colors.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            myGradientColors[i] = new GradientColorKey(colors[i], 1.0f);
            myGradientAlphas[i] = new GradientAlphaKey(alpha, 1.0f);
        }
        gradient.SetKeys(
           myGradientColors,
            myGradientAlphas
        );
        pS.startColor = gradient;
    }
   
}
