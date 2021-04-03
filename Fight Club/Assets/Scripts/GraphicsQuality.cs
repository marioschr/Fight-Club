using TMPro;
using UnityEngine;

public class GraphicsQuality : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.value = PlayerPrefs.GetInt("GraphicsQuality", 4); // Παίρνουμε την επιλογή του χρήστη για την ποιότητα
    }

    public void SetGraphicsQuality(int quality) // Θέτουμε την επιλογή του χρήστη για την ποιότητα των γραφικών
    {
        QualitySettings.SetQualityLevel(quality, true);
        PlayerPrefs.SetInt("GraphicsQuality", quality);
    }
}
