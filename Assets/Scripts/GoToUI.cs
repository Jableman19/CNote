using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoToUI : MonoBehaviour
{
    public GameObject destination;
    public GameObject source;
    // Start is called before the first frame update
    public void SwitchUI() {
        source.SetActive(false);
        destination.SetActive(true);

        handleUISwitch();
    }


    private void handleUISwitch()
    {
        GameObject ui = GameObject.Find("UI");
        GameObject currentSong = ui.transform.Find("currentSong").gameObject;
        if (source.name == "AR UI")
        {
            // switching to songbook
            
            currentSong.GetComponent<TextMeshProUGUI>().fontSize = 0;

        }
        else
        {
            // swtiching to AR UI
            currentSong.GetComponent<TextMeshProUGUI>().fontSize = 80;
        }
    }
}
