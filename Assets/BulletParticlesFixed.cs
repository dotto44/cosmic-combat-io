using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticlesFixed : MonoBehaviour
{
    [SerializeField] ParticleSystem myParticleSystem;

    ParticleSystem.MainModule mainModule;
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
            myGradientColors[i] = new GradientColorKey(colors[i], (float)i/1);
            myGradientAlphas[i] = new GradientAlphaKey(alpha, 1.0f);
        }
        gradient.SetKeys(
           myGradientColors,
            myGradientAlphas
        );
       
        var main = myParticleSystem.main;
        main.startColor = gradient;
    }
    public void AssignGradient(Color color, short particles)
    {
        var main = myParticleSystem.main;

        main.startColor = color;
        myParticleSystem.Emit(particles);
     
    }
}
