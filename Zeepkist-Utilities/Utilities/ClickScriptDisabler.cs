using System;
using JetBrains.Annotations;
using UnityEngine;

namespace TNRD.Zeepkist.Utilities;

public sealed class ClickScriptDisabler : MonoBehaviour
{
    private static LEV_ClickScript clickScript;
    private static int disableRequests;

    [PublicAPI] public static bool IsEnabled => disableRequests == 0;

    [PublicAPI]
    public static void Enable()
    {
        disableRequests = Math.Max(0, disableRequests - 1);
        UpdateClickScript();
    }

    [PublicAPI]
    public static void Disable()
    {
        disableRequests++;
        UpdateClickScript();
    }

    private static void FetchLatestInstance()
    {
        if (clickScript == null)
            clickScript = FindObjectOfType<LEV_ClickScript>();
    }

    private static void UpdateClickScript()
    {
        FetchLatestInstance();

        if (clickScript != null)
        {
            clickScript.enabled = IsEnabled;
        }
        else
        {
            Debug.LogWarning("Trying to update ClickScript's state but we don't have an instance!");
        }
    }
}
