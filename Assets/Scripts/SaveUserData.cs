using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

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
        
        var path = Path.Combine("C:\\Users\\etudiant\\Documents", "UsersData.csv");
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
        string headers =  "Genre" +";"+ "Hand Type" + ";"+  "Totale Time" + ";" 
            + " Round_1 Fruit" + ";" + " Round_2 Fruit" + ";" + " Round_3 Fruit" + ";" + " Round_4 Fruit" + ";" + " Round_5 Fruit" + ";" 
            + " Round_1 Time" + ";" + " Round_2 Time" + ";" + " Round_3 Time" + ";" + " Round_4 Time" + ";" + " Round_5 Time" + ";"; 

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
        string data = "" + ";" +Measures.Instance.handType + ";" + Measures.Instance.totalElapsedTime + ";" 
            + Measures.Instance.brokeOrder[0] + ";" + Measures.Instance.brokeOrder[1] + ";" + Measures.Instance.brokeOrder[2] + ";" + Measures.Instance.brokeOrder[3] + ";" + Measures.Instance.brokeOrder[4] + ";" 
            + Measures.Instance.roundElapsedTime[0] + ";" + Measures.Instance.roundElapsedTime[1] + ";" + Measures.Instance.roundElapsedTime[2] + ";" + Measures.Instance.roundElapsedTime[3] + ";" + Measures.Instance.roundElapsedTime[4] + ";";



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
