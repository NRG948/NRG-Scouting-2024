using UnityEngine;

public class AutoSelectBox : MonoBehaviour
{
    private CanvasGroup group;

    private string animationState = "none";
    private float currentAnimationTick = 0f;
    public float FadeSpeed;

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
        if (animationState == "in")
        {
            if (currentAnimationTick < FadeSpeed)
            {
                currentAnimationTick += Time.deltaTime;
                group.alpha = currentAnimationTick / FadeSpeed;
            }
            else
            {
                currentAnimationTick = 0;
                group.alpha = 1f;
                animationState = "none";
                group.interactable = true;
            }
        }
        else if (animationState == "out")
        {
            if (currentAnimationTick < FadeSpeed)
            {
                currentAnimationTick += Time.deltaTime;
                group.alpha = 1 - (currentAnimationTick / FadeSpeed);
            }
            else
            {
                currentAnimationTick = 0;
                group.alpha = 0f;
                animationState = "none";
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Starts AlertSelectBox group fade-in animation.
    /// </summary>
    public void FadeIn()
    {
        HapticManager.LightFeedback();
        if (animationState == "none")
        {
            gameObject.SetActive(true);
            animationState = "in";
        }
    }

    /// <summary>
    /// Starts AlertSelectBox group fade-out animation.
    /// </summary>
    public void FadeOut()
    {
        HapticManager.HeavyFeedback();
        if (animationState == "none")
        {
            group.interactable = false;
            animationState = "out";
        }
    }
}
