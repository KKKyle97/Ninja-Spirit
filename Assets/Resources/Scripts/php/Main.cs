using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Web web;
    public UserInfo userInfo;
    public Avatar[] avatars;
    public HTTPResponse response;
    public static Main instance;
    public Badge[] badges;
    public Report report;
    public bool hasNewUnlock = false;
    public string username { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        report = new Report();
        web = GetComponent<Web>();
    }

    public void TestStart()
    {
        instance = this;
        report = new Report();
        web = GetComponent<Web>();
    }
}
