using UnityEngine;

public class DataPersonalRen
{
    public static float money;
    public static float currentHealthRen;
    public static float maxHealthRen = 100;

    // Method to save current health to PlayerPrefs
    public static void SaveHealth()
    {
        PlayerPrefs.SetFloat("currentHealthRen", currentHealthRen);
        PlayerPrefs.Save();
    }

    // Method to load health from PlayerPrefs
    public static void LoadHealth()
    {
        // Load health from PlayerPrefs if it exists, otherwise use maxHealthRen
        currentHealthRen = PlayerPrefs.HasKey("currentHealthRen") ?
                           PlayerPrefs.GetFloat("currentHealthRen") : maxHealthRen;
    }
}
