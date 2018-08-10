using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class CChatClient : MonoBehaviour
{
    public string Address;
    public int Port;
    public Text nameText;
    public InputField nameInput;
    public Text chatText;
    public InputField chatInput;

    private TcpClient TheClient;
    private bool isConnect = false;

    private void Start()
    {
    }

    private void Update()
    {
        if (isConnect)
        {
            if (TheClient.Available > 0)
            {
                OnReceiveMessage();
            }
        }
    }

    public void Connent()
    {
        TheClient = new TcpClient();

        try
        {
            IPHostEntry host = Dns.GetHostEntry(Address);
            IPAddress newAddress = null;
            foreach (IPAddress h in host.AddressList)
            {
                if (h.AddressFamily == AddressFamily.InterNetwork)
                {
                    newAddress = h;
                }
            }
            TheClient.Connect(newAddress.ToString(), Port);
            chatText.text = null;
            chatText.text += string.Format("Connected to sever {0} Port : {1}\n", Address, Port);
            isConnect = true;
            Debug.Log(string.Format("Connected to sever {0} Port : {1}\n", Address, Port));
        }
        catch (Exception e)
        {
            chatText.text += string.Format("連線失敗 : 發生例外 {0}\n\n", e.ToString());
            chatText.text += "Please reconnect later...";
            Debug.LogError("Exception happend : " + e.ToString());
        }
    }

    public void SendName()
    {
        string sRequest = "LOGINNAME:" + nameInput.text;
        byte[] sRequestBuffer = System.Text.Encoding.ASCII.GetBytes(sRequest);

        TheClient.GetStream().Write(sRequestBuffer, 0, sRequestBuffer.Length);

        nameInput.interactable = false;
        Debug.Log("Send name" + nameInput.text);
    }

    public void SendBroadcast()
    {
        if (chatInput.text.Length > 0)
        {
            string message = chatInput.text;
            string sRequest = "BROADCAST:" + message;
            byte[] sRequestBuffer = System.Text.Encoding.ASCII.GetBytes(sRequest);

            TheClient.GetStream().Write(sRequestBuffer, 0, sRequestBuffer.Length);

            chatText.text += string.Format("{0} said : {1}\n", nameInput.text, message);
            chatInput.text = null;
            Debug.Log("Broadcast : " + message);
        }
    }

    public void OnReceiveMessage()
    {
        NetworkStream theClientStream = TheClient.GetStream();
        int numBytes = TheClient.Available;
        byte[] aBuffer = new byte[numBytes];
        int iByteRead = theClientStream.Read(aBuffer, 0, numBytes);

        string sRequest = System.Text.Encoding.ASCII.GetString(aBuffer).Substring(0, iByteRead);

        if (sRequest.StartsWith("MESSAGE:", StringComparison.OrdinalIgnoreCase))
        {
            string[] aTokens = sRequest.Split(':');
            string name = aTokens[1];
            string sMessage = aTokens[2];
            chatText.text += string.Format("{0} siad : {1}\n", name, sMessage);
            Debug.Log("Get message : " + sMessage);
        }
    }
}