using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteApplier : MonoBehaviour
{

    public float intensity = 0.75f;
    public float duration = 0.5f;
    public Volume volume = null;

    //provider
    public LocomotionManager locomotionManager;
    private Vignette vignette = null;

    //Comfort setting
    public bool vignetteEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        //get vignette
        if(volume.profile.TryGet(out Vignette vignette)) {
            this.vignette = vignette;
        }
    }

    //Button trigger
    private bool rightBTrigger = false;

    // Update is called once per frame
    void Update()
    {
        if (AssetManager.GetInputManager().GetRightBDown()) {
            if (!rightBTrigger) {
                rightBTrigger = true;
                vignetteEnabled = !vignetteEnabled;
                Debug.Log(vignetteEnabled);
            }
        } else {
            rightBTrigger = false;
        }
    }

    public void FadeIn() {
        if(vignetteEnabled) StartCoroutine(Fade(0, intensity));
    }

    public void FadeOut() {
        if (vignetteEnabled) StartCoroutine(Fade(intensity, 0));
    }

    private IEnumerator Fade(float startValue, float endValue) {
        float timeElapsed = 0.0f;
        while (timeElapsed <= duration) {
            //Calculate blend
            float blend = timeElapsed / duration;
            timeElapsed += Time.deltaTime;

            //Apply intensity
            float intens = Mathf.Lerp(startValue, endValue, blend);
            ApplyValue(intens);

            yield return null;
        }
    }

    private void ApplyValue (float value) {
        this.vignette.intensity.Override(value);
    }
}
