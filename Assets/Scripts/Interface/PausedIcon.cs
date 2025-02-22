using UnityEngine;

public class PausedIcon : MonoBehaviour
{
    public GameObject pausedIcon;

    void Update()
    {
        if (Time.timeScale == 0f)
            pausedIcon.SetActive(true);
        else
            pausedIcon.SetActive(false);       
    }
}
