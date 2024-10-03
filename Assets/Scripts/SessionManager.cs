using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;
using Newtonsoft.Json;
using System.Text;
using System.Net.Mime;
using System;
using System.Threading.Tasks;
using UnityEngine.Events;
using System.IO;
using OVRSimpleJSON;
using TMPro;


public class CreateSessionResponse
{
    public Guid Id { get; set; }
}

public enum ERequestType { Session, Event, Metric}

public class SessionManager : MonoBehaviour
{
    [SerializeField] TMP_InputField m_partIdField;
    public string EndPoint = "http://localhost/";

    public string PartId;

    public Guid SessionId;

    private string m_backupFolderPath;

    public bool IsConnectedToSQL { get; private set; }

    public static SessionManager Instance { get; private set; }

    private MySceneManager m_sceneManager;
    private int m_requestsCount = 0;

    void Start()
    {
        Instance = this;
        PartId = PlayerPrefs.GetInt("IDPlayer").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!MySceneManager.Instance.HasSessionBegun && GameManager.inputText != null)
        //{
        //    if (MySceneManager.inputText != PartId) { PartId = GameManager.inputText; }
        //}
        // if (!m_sceneManager.HasSessionBegun && m_sceneManager.inputText != null)
        //{
        // if (m_sceneManager.inputText != PartId) { PartId = m_sceneManager.inputText; }
        //}

          if (m_partIdField.text != PartId) { m_partIdField.text = PartId; }
    }

    public UnityEvent SessionStarted;
    public UnityEvent SessionFailed;
    public UnityEvent RequestFailed;


    private async Task CreateSessionAsync()
    {
        /*creating folder and JSON*/
        m_backupFolderPath = Path.Combine(Application.persistentDataPath, "BiomimeticSessionBackups");
        if (!Directory.Exists(m_backupFolderPath))
        {
            Directory.CreateDirectory(m_backupFolderPath);
        }

        m_backupFolderPath = Path.Combine(m_backupFolderPath, PartId + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));
        if (!Directory.Exists(m_backupFolderPath))
        {
            Directory.CreateDirectory(m_backupFolderPath);
        }

        var body = new { partId = PartId, date = DateTimeOffset.Now , avatarIV = PlayerPrefs.GetInt("handType").ToString(), handedness = PlayerPrefs.GetInt("handId").ToString() };
        string json = JsonConvert.SerializeObject(body);

        try
        {
            using HttpClient client = new HttpClient();

            byte[] bytes = Encoding.UTF8.GetBytes(json);
            HttpContent content = new ByteArrayContent(bytes);
            content.Headers.ContentType = new(MediaTypeNames.Application.Json);

            Debug.Log("Sending Request...", this);

            HttpResponseMessage response = await client.PostAsync(EndPoint + "Sessions", content);

            Debug.Log($"Response : {(int)response.StatusCode}", this);

            string responseText = await response.Content.ReadAsStringAsync();
            var responseBody = JsonConvert.DeserializeObject<CreateSessionResponse>(responseText);
            SessionId = responseBody.Id;

            Debug.Log($"ALL GOOD - {SessionId}", this);
            SessionStarted?.Invoke();

            /*Reporting success*/
            WriteBackupJson(true, json, ERequestType.Session);
            IsConnectedToSQL = true;
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
            SessionFailed?.Invoke();

            /*Generating a session Id*/
            SessionId = System.Guid.NewGuid();

            /*Reporting failure*/
            WriteBackupJson(false, json, ERequestType.Session);

            IsConnectedToSQL= false;
        }
    }

    public void CreateSession() => CreateSessionAsync();


    public async Task RecordEventAsync(string type, string targetId, DateTimeOffset dto)
    {
        var body = new { type, objectId = targetId, date = dto};
        string json = JsonConvert.SerializeObject(body);

        /*If not connected, writing directly the backup*/
        if (!IsConnectedToSQL)
        {
            WriteBackupJson(false, json, ERequestType.Event);
            return;
        }

        try
        {
            using HttpClient client = new HttpClient();

            
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            HttpContent content = new ByteArrayContent(bytes);
            content.Headers.ContentType = new(MediaTypeNames.Application.Json);

            Debug.Log("Sending Request...", this);

            HttpResponseMessage response = await client.PostAsync(EndPoint + "Sessions/" + SessionId + "/Events", content);

            Debug.Log($"Response : {(int)response.StatusCode}", this);

            /*Reporting success*/
            WriteBackupJson(true, json, ERequestType.Event);
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
            RequestFailed?.Invoke();

            /*Reporting failure*/
            WriteBackupJson(false, json, ERequestType.Event);
        }
    }

    public async Task RecordEventAsync(string type, DateTimeOffset dto)
    {
        var body = new { type, date = dto };
        string json = JsonConvert.SerializeObject(body);

        /*If not connected, writing directly the backup*/
        if (!IsConnectedToSQL)
        {
            WriteBackupJson(false, json, ERequestType.Event);
            return;
        }

        try
        {
            using HttpClient client = new HttpClient();


            byte[] bytes = Encoding.UTF8.GetBytes(json);
            HttpContent content = new ByteArrayContent(bytes);
            content.Headers.ContentType = new(MediaTypeNames.Application.Json);

            Debug.Log("Sending Request...", this);

            HttpResponseMessage response = await client.PostAsync(EndPoint + "Sessions/" + SessionId + "/Events", content);

            Debug.Log($"Response : {(int)response.StatusCode}", this);

            /*Reporting success*/
            WriteBackupJson(true, json, ERequestType.Event);
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
            RequestFailed?.Invoke();

            /*Reporting failure*/
            WriteBackupJson(false, json, ERequestType.Event);
        }
    }

    public async Task RecordMetricAsync(string type, float value, string targetId, DateTimeOffset dto)
    {
        var body = new { type, value = value , objectId = targetId, date = dto };
        string json = JsonConvert.SerializeObject(body);

        /*If not connected, writing directly the backup*/
        if (!IsConnectedToSQL)
        {
            WriteBackupJson(false, json, ERequestType.Metric);
            return;
        }

        try
        {
            using HttpClient client = new HttpClient();

            
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            HttpContent content = new ByteArrayContent(bytes);
            content.Headers.ContentType = new(MediaTypeNames.Application.Json);

            Debug.Log("Sending Request...", this);

            HttpResponseMessage response = await client.PostAsync(EndPoint + "Sessions/" + SessionId + "/Metrics", content);

            Debug.Log($"Response : {(int)response.StatusCode}", this);

            /*Reporting success*/
            WriteBackupJson(true, json, ERequestType.Metric);
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
            RequestFailed?.Invoke();

            /*Reporting failure*/
            WriteBackupJson(false, json, ERequestType.Metric);
        }
    }

    public async Task RecordMetricAsync(string type, float value, DateTimeOffset dto)
    {
        var body = new { type, value = value, date = dto };
        string json = JsonConvert.SerializeObject(body);

        /*If not connected, writing directly the backup*/
        if (!IsConnectedToSQL)
        {
            WriteBackupJson(false, json, ERequestType.Metric);
            return;
        }

        try
        {
            using HttpClient client = new HttpClient();


            byte[] bytes = Encoding.UTF8.GetBytes(json);
            HttpContent content = new ByteArrayContent(bytes);
            content.Headers.ContentType = new(MediaTypeNames.Application.Json);

            Debug.Log("Sending Request...", this);

            HttpResponseMessage response = await client.PostAsync(EndPoint + "Sessions/" + SessionId + "/Metrics", content);

            Debug.Log($"Response : {(int)response.StatusCode}", this);

            /*Reporting success*/
            WriteBackupJson(true, json, ERequestType.Metric);
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
            RequestFailed?.Invoke();

            /*Reporting failure*/
            WriteBackupJson(false, json, ERequestType.Metric);
        }
    }


    private void WriteBackupJson(bool success, string json, ERequestType type)
    {
        m_requestsCount++;
        string status = "Failure";

        if(success) {status = "Success";}  

        /*writing json*/
        string filename = $"{status}_{SessionId}_{type.ToString()}_{DateTime.Now:yyyyMMdd_HHmmss}_{m_requestsCount}.json";
        string filepath = Path.Combine(m_backupFolderPath, filename);
        File.WriteAllText(filepath, json);
    }

    
}
