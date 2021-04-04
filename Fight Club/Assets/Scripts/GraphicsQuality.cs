using Michsky.UI.ModernUIPack;
using TMPro;
using UnityEngine;

public class GraphicsQuality : MonoBehaviour
{
    private HorizontalSelector dropdown;
    void Start()
    {
        dropdown = GetComponent<HorizontalSelector>();
    }

    public void SetGraphicsQuality(int quality) // Θέτουμε την επιλογή του χρήστη για την ποιότητα των γραφικών
    {
        QualitySettings.SetQualityLevel(quality, true);
        PlayerPrefs.SetInt("GraphicsQuality", quality);
    }

    public void SetGraphicsValue()
    {
        dropdown = GetComponent<HorizontalSelector>();
        dropdown.defaultIndex = PlayerPrefs.GetInt("GraphicsQuality", 4);// Παίρνουμε την επιλογή του χρήστη για την ποιότητα
    }
}
