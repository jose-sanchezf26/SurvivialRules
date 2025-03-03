using UnityEngine;

public class PausedIcon : MonoBehaviour
{
    public GameObject pausedIcon;
    public GameObject deathPanel;

    void Update()
    {
        if (Time.timeScale == 0f && !deathPanel.activeSelf)
            pausedIcon.SetActive(true);
        else
            pausedIcon.SetActive(false);       
    }
}
