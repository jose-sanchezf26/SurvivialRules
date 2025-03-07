using UnityEngine;

public class Manager : MonoBehaviour
{
    // Este método se llama desde JavaScript para asignar el nombre de usuario
    public void SetUsername(string user)
    {
        FlowManager.instance.loggedInUser = user;
    }


    // Esta función será llamada desde JavaScript para pasar el username a Unity
    public void ReceiveUsernameFromJS(string usernameFromJS)
    {
        // Ahora Unity recibe el nombre de usuario desde JavaScript
        SetUsername(usernameFromJS);
    }
}
