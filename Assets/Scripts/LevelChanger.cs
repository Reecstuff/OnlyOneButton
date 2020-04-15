using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Set the LevelUI
/// </summary>
public class LevelChanger : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textField;

    public int currentLevel = 0;

    string levelFormat = "'Level '00";


    public void setLevel(int level)
    {
        currentLevel = currentLevel + level > 0 ? currentLevel + level : currentLevel;

        textField.text = currentLevel.ToString(levelFormat);
    }

    public void setLevelDirect(int level)
    {
        currentLevel = level;
        textField.text = currentLevel.ToString(levelFormat);
    }

}
