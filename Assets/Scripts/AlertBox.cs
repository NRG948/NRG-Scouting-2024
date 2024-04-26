using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlertBox : MonoBehaviour
{
    private bool selectNo;
    private bool selectYes;

    private Transform alertBoxTransform;
    public GameObject YesButton;
    public GameObject NoButton;

    // Start is called before the first frame update
    void Start()
    {
        alertBoxTransform = transform.GetChild(0);
        alertBoxTransform.gameObject.SetActive(false);
    }


    /// <summary>
    /// Shows alert box.
    /// </summary>
    /// <param name="messageKey"></param> Concatenated message key split by |.
    public void ShowBoxByMessageKey(string messageKey)
    {
        var temp = messageKey.Split('|');
        string message = temp[0];
        string key = temp[1];

        StartCoroutine(ShowDecisionBox(message, key));
    }

    /// <summary>
    /// Shows optionless alert box.
    /// </summary>
    /// <param name="message"></param> The message to be displayed.
    /// <param name="doesKick"></param> If the alert box transfers to the main menu.
    /// <param name="isImportant"></param> If the alert box sends strong haptic feedback and a delay.
    /// <returns></returns>
    public IEnumerator ShowNotificationBox(string message, bool doesKick=false, bool isImportant=false)
    {
        HapticManager.LightFeedback();

        selectYes = false;
        YesButton.GetComponent<AlertBoxButton>().on = false;
        alertBoxTransform.gameObject.SetActive(true);
        alertBoxTransform.GetChild(1).GetComponent<TMP_Text>().text = message;
        alertBoxTransform.GetChild(3).gameObject.SetActive(false);

        while (!selectYes)
        {
            selectYes = YesButton.GetComponent<AlertBoxButton>().on;
            if (isImportant) { HapticManager.HeavyFeedback(); yield return new WaitForSeconds(0.3f); }
            yield return null;
        }

        HapticManager.HeavyFeedback();

        alertBoxTransform.gameObject.SetActive(false);
        if (doesKick) { SceneManager.LoadScene(0); }
    }

    /// <summary>
    /// Shows alert box with confirmation.
    /// </summary>
    /// <param name="message"></param> The message to be displayed.
    /// <param name="key"></param> The key referring to the action executed upon positive confirmation.
    /// <param name="isImportant"></param> If the alert box sends strong haptic feedback and a delay.
    /// <returns></returns>
    private IEnumerator ShowDecisionBox(string message, string key, bool isImportant=false)
    {
        HapticManager.LightFeedback();

        selectNo = false;
        selectYes = false;
        YesButton.GetComponent<AlertBoxButton>().on = false; NoButton.GetComponent<AlertBoxButton>().on = false;
        alertBoxTransform.gameObject.SetActive(true);
        alertBoxTransform.GetChild(3).gameObject.SetActive(true);
        alertBoxTransform.GetChild(1).GetComponent<TMP_Text>().text = message;

        while (!(selectNo || selectYes))
        {
            selectYes = YesButton.GetComponent<AlertBoxButton>().on;
            selectNo = NoButton.GetComponent<AlertBoxButton>().on;
            if (isImportant) { HapticManager.HeavyFeedback(); yield return new WaitForSeconds(0.3f); }
            yield return null;

        };
        HapticManager.HeavyFeedback();
        alertBoxTransform.gameObject.SetActive(false);
        if (selectYes)
        {
            CallAlertAction(key);
        }
    }

    public void SetTrue() { selectNo = selectYes = true; }

    public void SetFalse() { selectNo = true; }

    /// <summary>
    /// Calls the alert action.
    /// </summary>
    /// <param name="key"></param> The key referring to the action executed.
    private void CallAlertAction(string key)
    {    
        switch (key)
        {
            case "settingsSave":
                GameObject.Find("Scripts").GetComponent<SaveSystem>().SaveData(); break;
            case "settingsDelete":
                GameObject.Find("Scripts").GetComponent<SaveSystem>().DeletData(); break;
            case "objSave":
                GameObject.Find("DataManager").GetComponent<DataManager>().SaveRobotScout(); break;
            case "subjSave":
                GameObject.Find("DataManager").GetComponent<DataManager>().SaveSubjectiveRobotScout(); break;
            case "pitSave":
                GameObject.Find("DataManager").GetComponent<DataManager>().SavePitScout(); break;
            case "menu":
                GameObject.Find("HomeButton").GetComponent<HomeButton>().homeButton(); break;
            case "ldvDelete":
                GameObject.Find("LocalDataViewer").GetComponent<LocalDataViewer>().confirmDelete(); break;
            case "exit":
                Application.Quit(); break;
            case "ldvDeleteAllConfirm":
                StartCoroutine(ShowDecisionBox("Please understand that you are DELETING ALL SAVED DATA FOR THIS EVENT. Your scouting team will not be held responsible for your actions.", "ldvDeleteAll",true));break;
            case "ldvDeleteAll":
                GameObject.Find("LocalDataViewer").GetComponent<LocalDataViewer>().deleteFullEvent();break;
            case "downloadMatches":
                GameObject.Find("MatchDownloader").GetComponent<MatchDownloader>().downloadMatches();break;
        }
    }
}
