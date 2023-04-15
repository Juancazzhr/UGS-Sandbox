using CloudCode;

using System;
using System.Collections.Generic;

using Unity.Services.Authentication;
using Unity.Services.CloudCode;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

using UnityEngine;


public class CloudCodeInitializer : MonoBehaviour
{
    enum Environments
    {
        Prod,
        Dev
    }

    [SerializeField] string username;
    [SerializeField] Environments environments;

    const string _PROD_ENVIRONMENT = "production";
    const string _DEV_ENVIRONMENT = "development";

    async void Start()
    {
        var options = new InitializationOptions();
        options.SetEnvironmentName(environments == Environments.Prod ? _PROD_ENVIRONMENT : _DEV_ENVIRONMENT);

        var arguments = new Dictionary<string, object>
        {
            { "platform", $"{Application.platform:G}" },
            { "username", $"{(string.IsNullOrWhiteSpace(username) ? "Anonymous" : username)}" }
        };

        try
        {
            await UnityServices.InitializeAsync(options);
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            var response = await CloudCodeService.Instance.CallEndpointAsync<CloudCodeResponse>("hello-world", arguments);

            print(response.Message);
        }
        catch (Exception exception)
        {
            print(exception.Message);
        }
    }
}