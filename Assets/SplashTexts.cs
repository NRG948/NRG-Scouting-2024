using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SplashTexts : MonoBehaviour
{
    public TMP_Text txt;

    public string[] splashTexts = {
        "Watt was that?",
        "Shocking!",
        "Have fun, darnit!",
        "Red Alliance!",
        "Blue Alliance!",
        "RIP Dodo",
        "No comms?",
        "MARGINS >:(",
        "Electric!",
        "Ayo pay attention",
        "Shift + Alt + F your pit",
        "Who are we? MC^2",
        "kg m^2/s^2",
        "Not by the wires!",
        "Have you turned it off and on again?",
        "The only thing I can pull are my commits",
        "Sushi Wannabe",
        "[SPLASH TEXT NOT S&P APPROVED]",
        "Red and Gold!",
        "Dark mode when",
        "FIRE IN THE HOLE",
        "Sugar free",
        "If there are joysticks, are there sadsticks?",
        "Gulati approved",
        "In-Game Purchases Included",
        "Drop that like and leave a comment below",
        "It's not a bug, it's a feature!",
        "Give us a better name",
        "W IT",
        "C#? It looks rather dull to me",
        "Programmer? I'm glad you support proper English",
        "And that's how I lost my license",
        "Zero factor authentication",
        "Accept cookies? \\(•ω•`)o",
        "error 1002: ; Expected",
        "Did we cook?",
        "Now with 95% less consistent variable names!",
        "No, the grey arrows aren't buttons",
        "Can you hear the music?",
        "Also try Terraria!",
        $"{PlayerPrefs.GetString("ScouterName","Anonymous")} is you!"
    };

    void Start()
    {
        txt.text = splashTexts[Random.Range(0, splashTexts.Length)];
    }
}
