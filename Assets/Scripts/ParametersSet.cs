using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ParametersSet : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TMP_Dropdown Handdropdown;
    public TMP_InputField inputFieldID;
    public TMP_InputField inputFieldRound;
    public Volume postProcess;
    private Vignette vg;

    // Start is called before the first frame update
    void Start()
    {
        postProcess.profile.TryGet(out vg);
        vg.intensity.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchExp()
    {
        int playerID = int.Parse(inputFieldID.text.ToString());
        PlayerPrefs.SetInt("IDPlayer",playerID);

        int handID = Handdropdown.value;
        PlayerPrefs.SetInt("handId", handID);
        if( handID == 0 )
        { PlayerPrefs.SetString("handSide", "Left");}
        else { PlayerPrefs.SetString("handSide", "Right");}
        
      GetComponent<MySceneManager>().SetHandVisual();
        //int nbRounds = int.Parse(inputFieldRound.text.ToString());
        //PlayerPrefs.SetInt("nbRounds", nbRounds);

        int handType = dropdown.value;
        PlayerPrefs.SetInt("handType", handType);
        if (handType == 0)
        { PlayerPrefs.SetString("condition", "Glove"); }
        else { PlayerPrefs.SetString("handSide", "Claw"); }

        SceneManager.LoadScene("InteractionScene", LoadSceneMode.Single);

    }

    
    public void SetHandType()
    {
      

    }
    public void SetID()
    {
     

    }
}
