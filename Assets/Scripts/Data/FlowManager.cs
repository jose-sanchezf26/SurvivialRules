using UnityEngine;

public class FlowManager : MonoBehaviour
{
    public static FlowManager instance;
    public string groupID = "defaultGroup";
    public string loggedInUser = "defaultUser";
    public SelectedSet selectedSet;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

public class SelectedSet
{
    public string name = "DefaultSet";
}
