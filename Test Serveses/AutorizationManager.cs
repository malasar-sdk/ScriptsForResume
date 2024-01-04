using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.VisualScripting;
using UnityEngine;

public class AutorizationManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text googlePlay, anonim;

    [SerializeField]
    private GooglePlayAuthorization googlePlayAuthorization;

    [SerializeField]
    private AnonimAuthorization anonimAuthorization;

    [SerializeField]
    private SaverLoader saverLoader;

    private async void Start()
    {
        await UnityServices.InitializeAsync();

        googlePlay.text = "Google play!";
        anonim.text = "Anonim!";
    }

    public async void GooglePlayLogin()
    {
        await googlePlayAuthorization.Authenticate();

        googlePlay.text = "Google play logIn complete!";
    }

    public void AnonimLogin()
    {
        anonimAuthorization.SignInAnonimus();

        anonim.text = "Anonim logIn complete!";
    }
}
