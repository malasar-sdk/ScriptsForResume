using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
using UnityEngine;

public class AnonimAuthorization : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txtAnonim;

    public async void SignInAnonimus()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            txtAnonim.text = $"Player ID: {AuthenticationService.Instance.PlayerId}";
            Debug.Log("Sign in Sucsess!");
        }
        catch (AuthenticationException ex)
        {
            txtAnonim.text = "Sign in Anonim failed!";
            Debug.Log("Sign in Anonim failed!");
            Debug.LogException(ex);
        }
    }
}
