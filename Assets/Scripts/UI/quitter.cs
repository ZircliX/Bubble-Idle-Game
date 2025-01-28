using UnityEngine;

public class QuitButton : MonoBehaviour
{
    
    public void QuitGame()
    {
       
        Debug.Log("So long");
        Application.Quit();
    }
}