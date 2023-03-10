using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace TNRD.Zeepkist.Utilities;

public sealed class ClickScriptDisabler : MonoBehaviour
{
    private static LEV_ClickScript clickScript;
    private static int disableRequests;

    private static readonly List<object> senders = new List<object>();
    
    [PublicAPI] public static bool IsEnabled => disableRequests == 0 || senders.Count > 0;

    [PublicAPI, Obsolete]
    public static void Enable()
    {
        disableRequests = Math.Max(0, disableRequests - 1);
        UpdateClickScript();
    }

    [PublicAPI, Obsolete]
    public static void Disable()
    {
        disableRequests++;
        UpdateClickScript();
    }

    public static void Enable(object sender)
    {
        if (senders.Contains(sender))
        {
            senders.Remove(sender);
        }
        
        UpdateClickScript();
    }

    public static void Disable(object sender)
    {
        if (senders.Contains(sender))
            return;

        senders.Add(sender);
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
