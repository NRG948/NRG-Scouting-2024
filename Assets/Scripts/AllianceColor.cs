using UnityEngine;
using UnityEngine.UI;

public class AllianceColor : MonoBehaviour
{
    private DataManager data;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        data = gameObject.GetComponent<DataManager>();
        image = gameObject.GetComponent<Image>();
    }

    /// <summary>
    /// Updates sprite color based on alliance.
    /// </summary>
    public void UpdateColor()
    {
        image.color = (data.match.AllianceColor == "red") ? new(222, 75, 62, 255) : new(76, 159, 230, 255);
    }
}
