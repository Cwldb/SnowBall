using System;
using UnityEngine;
using Work.KJY.Code.Core;

namespace Work.KJY.Code.Manager
{
    public class SettingManager : MonoSingleton<SettingManager>
    {
        [SerializeField] private GameObject settingsPanel;

        private void Start()
        {
            settingsPanel.SetActive(false);
        }

        public void ToggleSettings()
        {
            if (settingsPanel == null) return;
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
    }
}