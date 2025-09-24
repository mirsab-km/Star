using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject HomePanel;
    public GameObject levelsPanel;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayButton()
    {
        HomePanel.SetActive(false);
        levelsPanel.SetActive(true);
    }
}
