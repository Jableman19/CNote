using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToUI : MonoBehaviour
{
    public GameObject destination;
    public GameObject source;
    // Start is called before the first frame update
    public void SwitchUI() {
        source.SetActive(false);
        destination.SetActive(true);
    }
}
