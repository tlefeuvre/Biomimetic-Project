using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SaveUserData : MonoBehaviour
{
    StringBuilder newuserdata = new System.Text.StringBuilder();

    private static SaveUserData instance = null;
    public static SaveUserData Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }


    public void WriteNewUserData() // (float v, int t) // variables mises en exemple
    {

        

        var path = Path.Combine(Application.dataPath, "Participant_" + PlayerPrefs.GetInt("IDPlayer")+"_Condition_" + PlayerPrefs.GetInt("handType") +"_"+ System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm") + ".csv");
        Debug.Log(path);
        FindUsersDataFile(path);
        
        NewUserData(); // (v, t); // variables mises en exemple

        SaveNewUserData(path, newuserdata.ToString());
    }


    // verifie si le fichier existe, sinon lance la fonction CreateUsersDataFile()

    void FindUsersDataFile(string path)
    {
        if (!File.Exists(path))
        {

            CreateUsersDataFile();
        }
        else
        {
            ReadUsersDataFile(path);
        }
    }


    // cree les headers du nouveau fichier et les ajoute au StringBuildder newuserdata

    void CreateUsersDataFile()
    {
        /* "Velocite" + ";" + "Temps" + ";" // variables mises en exemple*/
        string headers = "ID" + ";" + "Condition" + ";" + "Totale Time" + ";" +"Number of hits"+";"+"Number of open";
           

        /*for(int i = 0; i < FruitsSpawner.Instance.numbOfRounds; i++)
            headers = headers + "Round_" + i.ToString() + " Fruit ;";

        for (int i = 0; i < FruitsSpawner.Instance.numbOfRounds; i++)
            headers = headers + "Round_" + i.ToString() + " Time ;";*/

        newuserdata.AppendLine(headers);
    }


    // si pas encore recuperees, recupere les anciennes donnees du fichier et les enregistre dans le StreamBuilder newuserdata

    void ReadUsersDataFile(string path)
    {
        if (newuserdata.ToString() == "")
        {
            string data = File.ReadAllText(path, Encoding.UTF8);

            newuserdata.Append(data);
        }
        
    }

    // ajoute les données des variables au StringBuilder newuserdata

    void NewUserData() // (float v, int t) // variables mises en exemple
    {
        string data = PlayerPrefs.GetInt("IDPlayer")  + ";" + (int)GetComponent<MySceneManager>().GetHandType() + ";" + Measures.Instance.totalElapsedTime + ";"+PlayerPrefs.GetInt("NumberHits")+";"+ PlayerPrefs.GetInt("NumberOpened");
          
        /*for (int i = 0; i < FruitsSpawner.Instance.numbOfRounds; i++)
            data = data + Measures.Instance.brokeOrder[i] + ";";

        for (int i = 0; i < FruitsSpawner.Instance.numbOfRounds; i++)
            data = data + Measures.Instance.roundElapsedTime[i] + ";";*/
        newuserdata.AppendLine(data);
    }


    // ecrit les données du StringBuilder newuserdata dans le fichier UsersData.csv 

    void SaveNewUserData(string path, string data)
    {
        using (var writer = new StreamWriter(path))
        {
            writer.Write(data, Encoding.UTF8);
            writer.Flush();
            writer.Close();
        }

    }

    private void Start()
    {
       // WriteNewUserData();
    }
}
