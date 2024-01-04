using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class GooglePlayAuthorization : MonoBehaviour
{
    public string GooglePlayToken, GooglePlayError;

    [SerializeField]
    private TMP_Text googlePlay;

    public async Task Authenticate()
    {
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(succsess =>
        {
            if (succsess == SignInStatus.Success)
            {
                Debug.Log("Login Google success!");
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    Debug.Log($"Auth cose is: {code}");
                    GooglePlayToken = code;

                    googlePlay.text = $"Login Google success! - {GooglePlayToken}";
                });
            }
            else
            {
                GooglePlayError = "Failed to retrieve GPS auth code";
                Debug.LogError("Login Unsuccessful!");
                googlePlay.text = "Login Unsuccessful!";
            }
        });

        await AuthenticateWithUnity();
    }

    private async Task AuthenticateWithUnity()
    {
        try
        {
            await AuthenticationService.Instance.SignInWithGoogleAsync(GooglePlayToken);
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
            throw;
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
            throw;
        }
    }
}
