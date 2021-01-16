using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelsDropdown : MonoBehaviour
{
    Dropdown dropdown;
    List<Dropdown.OptionData> dropdownOptions = new List<Dropdown.OptionData>();
    int value;

    void Start()
    {
        dropdown = GetComponent<Dropdown>();

        dropdownOptions.Add(new Dropdown.OptionData("Level 1"));
        dropdownOptions.Add(new Dropdown.OptionData("Level 2"));

        dropdown.ClearOptions();
        dropdown.AddOptions(dropdownOptions);

        value = dropdown.value;
    }

    public void OnChange()
    {
        // Debug.Log("Dropdown new value = '" + dropdown.value + "'. Type = " + dropdown.value.GetType());
        value = dropdown.value;
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(value + 1);
    }
}
