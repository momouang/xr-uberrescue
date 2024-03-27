using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UnderwaterDepth : MonoBehaviour
{
    [Header("Depth Parameters")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private int depth = 0;

    [Header("Post Processing Volume")]
    [SerializeField] private Volume postProcessingVolume;

    [Header("Post Processing Profiles")]
    [SerializeField] private VolumeProfile surfacePostProcessing;
    [SerializeField] private VolumeProfile underwaterPostProcessing;

    public ParticleSystem bubbleParticle;
    public bool isPlaying;
    public bool isTouched;

    private void Update()
    {
        if(mainCamera.position.y < depth)
        {
            EnableEffects(true);
        }
        else
        {
            EnableEffects(false);
        }


        //Bubble Effect when diving
        if(!isPlaying && isTouched)
        {
            isPlaying = true;
            PlayParticle();
        }
        else
        {
            return;
        }

        if(!isTouched)
        {
            isPlaying = false;
        }
    }

    private void EnableEffects(bool active)
    {
        if(active)
        {
            RenderSettings.fog = true;
            //postProcessingVolume.profile = underwaterPostProcessing;
            isTouched = true;
        }
        else
        {
            RenderSettings.fog = false;
            //postProcessingVolume.profile = surfacePostProcessing;
            isTouched = false;
            isPlaying = false;
        }
    }

    private void PlayParticle()
    {
        bubbleParticle.Play();
    }
}
