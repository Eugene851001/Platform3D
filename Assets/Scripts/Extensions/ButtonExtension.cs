using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;

namespace Assets.Scripts.Extensions
{
    public static class ButtonExtension
    {
        public static string GetText(this Button button) =>
            button.GetComponentInChildren<TextMeshProUGUI>().text;

        public static void SetText(this Button button, string value) =>
            button.GetComponentInChildren<TextMeshProUGUI>().text = value;
    }
}
