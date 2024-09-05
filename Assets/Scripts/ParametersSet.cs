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
    public TMP_InputField inputFieldID;
    public TMP_InputField inputFieldRound;

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
        int handID = int.Parse(inputFieldID.text.ToString());
        PlayerPrefs.SetInt("IDPlayer", handID);

        //int nbRounds = int.Parse(inputFieldRound.text.ToString());
        //PlayerPrefs.SetInt("nbRounds", nbRounds);

        int handType = dropdown.value;
        PlayerPrefs.SetInt("handType", handType);

        SceneManager.LoadScene("FirstSceneNEW", LoadSceneMode.Single);

    }
    public void SetHandType()
    {
      

    }
    public void SetID()
    {
     

    }
}
