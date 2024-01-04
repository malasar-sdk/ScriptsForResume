using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckAuthorization : MonoBehaviour
{
    [SerializeField]
    private bool isLogin = true;

    [SerializeField]
    private string nameObjAuth = "AuthorizationManager";

    [SerializeField, Header("—сылки")]
    private GooglePlayLogin googlePlayLogin;

    [SerializeField]
    private TMP_Text GPStatus;

    private void Start()
    {
        if (googlePlayLogin == null)
        {
            isLogin = false;
        }

        GPStatus.text = "No login";
    }

    private void Update()
    {
        FindAuthManager();
    }

    public void CheckAthorizationGP()
    {
        if (googlePlayLogin != null)
        {
            if (googlePlayLogin.GetSuccsessBool() == true)
            {
                GPStatus.text = googlePlayLogin.GetPlayerNameGP();
            }
        }
    }

    private void FindAuthManager()
    {
        if (isLogin == false)
        {
            googlePlayLogin = GameObject.Find(nameObjAuth).GetComponent<GooglePlayLogin>();

            if (googlePlayLogin != null)
            {
                isLogin = true;
                CheckAthorizationGP();
            }
        }
    }
}
