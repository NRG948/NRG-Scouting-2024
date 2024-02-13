using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartingBox : MonoBehaviour
{
    public Image screen;
    public Image field;
    public Image cancel;
    public Image robot;
    public string animationState = "none";
    public float timer = 0;
    public float ANIMATION_TIME = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animationState == "in") {
            if (timer > 0) {
                timer -= Time.deltaTime;
                var tempColor = screen.color;
                tempColor.a = (1 - timer / ANIMATION_TIME) * 0.8f;
                screen.color = tempColor;

                tempColor = field.color;
                tempColor.a = 1 - timer / ANIMATION_TIME;
                field.color = tempColor;
                tempColor = cancel.color;
                tempColor.a = 1 - timer / ANIMATION_TIME;
                cancel.color = tempColor;
                tempColor = robot.color;
                tempColor.a = 1 - timer / ANIMATION_TIME;
                robot.color = tempColor;                
            } else {
                timer = 0;
                animationState = "none";
            }
        } else if (animationState == "out") {
            if (timer > 0) {
                timer -= Time.deltaTime;
                var tempColor = screen.color;
                tempColor.a = timer / ANIMATION_TIME * 0.8f;
                screen.color = tempColor;

                tempColor = field.color;
                tempColor.a = timer / ANIMATION_TIME;
                field.color = tempColor;
                tempColor = cancel.color;
                tempColor.a = timer / ANIMATION_TIME;
                cancel.color = tempColor;
                tempColor = robot.color;
                tempColor.a = timer / ANIMATION_TIME;
                robot.color = tempColor;
            } else {
                timer = 0;
                animationState = "none";
                gameObject.SetActive(false);
            }
        }
    }
    
    public void fadeIn() {
        if (animationState == "none") {
            animationState = "in";
            timer = ANIMATION_TIME;
            gameObject.SetActive(true);
        }
    }

    public void fadeOut() {
        if (animationState == "none") {
            animationState = "out";
            timer = ANIMATION_TIME;
        }
    }
}
