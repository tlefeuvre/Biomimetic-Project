using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ParametersSet : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchExp()
    {
        SceneManager.LoadScene("FirstScene", LoadSceneMode.Single);

    }
    public void SetHandType()
    {
        int handType = dropdown.value;
        Debug.Log("hand type: " + handType);
        PlayerPrefs.SetInt("handType", handType);

    }
    public void SetID()
    {
        int handID = int.Parse(inputField.text.ToString());
        Debug.Log("id: " + handID);

        PlayerPrefs.SetInt("IDPlayer", handID);

    }
}
