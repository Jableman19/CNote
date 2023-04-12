using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupPopulator : MonoBehaviour
{
    public string title;
    public string info;
    TextMeshProUGUI titleText;
    Button learnMoreButton;

    PopupManager popupManager;
    // Start is called before the first frame update
    void Start()
    {
        titleText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        titleText.text = title;

        learnMoreButton = gameObject.GetComponentInChildren<Button>();
        learnMoreButton.onClick.AddListener(OpenInfoPanel);

        popupManager = PopupManager.instance;
    }

    void OpenInfoPanel() {
        popupManager.gameObject.GetComponent<GoToUI>().SwitchUI();
        popupManager.PopulateInfoPanel(title, info);
    }
}
