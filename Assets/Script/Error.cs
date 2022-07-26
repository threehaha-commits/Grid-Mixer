using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class Error
{
    private static int TimeAwait = 1000;
    private static GameObject Backgounrd;
    private static TMP_Text Text;

    public Error(TMP_Text text)
    {
        Text = text;
        Backgounrd = Text.transform.parent.gameObject;
    }

    async public static void Put(string message)
    {
        if (Backgounrd.activeInHierarchy)
        {
            TimeAwait += 1000;
            return;
        }
        Backgounrd.SetActive(true);
        Text.text = message;
        await Task.Delay(TimeAwait);
        Backgounrd.SetActive(false);
        TimeAwait = 1000;
    }
}
