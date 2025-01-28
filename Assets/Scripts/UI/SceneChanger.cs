using UnityEngine;
using UnityEngine.SceneManagement; // Nécessaire pour changer de scène

public class SceneChanger : MonoBehaviour
{
    // Méthode appelée lors du clic sur le bouton
    public void LoadScene(string TestQuentin)
    {
        SceneManager.LoadScene(TestQuentin); // Change la scène spécifiée par "sceneName"
    }
}