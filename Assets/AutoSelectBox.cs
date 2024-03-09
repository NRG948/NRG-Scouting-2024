using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSelectBox : MonoBehaviour
{
    public CanvasGroup group;
    public string animationState = "none";
    public float fadeSpeed;
    public float animationTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();

        group.alpha = 0f;
        group.interactable = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (animationState == "in") {
            if (animationTime < fadeSpeed) {
                animationTime += Time.deltaTime;
                group.alpha = animationTime / fadeSpeed;
            } else {
                animationTime = 0;
                group.alpha = 1f;
                animationState = "none";
                group.interactable = true;
            }
        } else if (animationState == "out") {
            if (animationTime < fadeSpeed) {
                animationTime += Time.deltaTime;
                group.alpha = 1 - (animationTime / fadeSpeed);
            } else {
                animationTime = 0;
                group.alpha = 0f;
                animationState = "none";
                gameObject.SetActive(false);
            }
        }
    }

    public void FadeIn() {
        if (animationState == "none") {
            gameObject.SetActive(true);
            animationState = "in";
        }
    }

    public void FadeOut() {
        if (animationState == "none") {
            group.interactable = false;
            animationState = "out";
        }
    }
}
