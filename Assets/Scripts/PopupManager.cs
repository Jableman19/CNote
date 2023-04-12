using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public static PopupManager instance;
    public GameObject infoPanel;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void PopulateInfoPanel(string title, string info) {
        infoPanel.SetActive(true);
        infoPanel.transform.Find("Title").gameObject.GetComponent<TextMeshProUGUI>().text = title;
        infoPanel.transform.Find("InfoPanel").gameObject.GetComponentInChildren<TextMeshProUGUI>().text = info;
    }
}
