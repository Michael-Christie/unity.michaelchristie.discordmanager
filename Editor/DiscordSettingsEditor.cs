using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MC.DiscordManager
{
    [CustomEditor(typeof(DiscordSettings))]
    public class DiscordSettingsEditor : Editor
    {
        private string password = "";

        private int steamApp = 0;

        //
        public override void OnInspectorGUI()
        {
            DiscordSettings _settingTarget = (DiscordSettings)target;

            GUIStyle _redText = new GUIStyle(EditorStyles.label);
            _redText.normal.textColor = Color.red;

            EditorGUILayout.LabelField("Discord Settings", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();

            password = EditorGUILayout.PasswordField("Discord App ID", _settingTarget.discordAppId == -1 ? "" : _settingTarget.discordAppId.ToString());

            if (!string.IsNullOrEmpty(password))
            {
                long _appID;
                if (long.TryParse(password, out _appID))
                {
                    _settingTarget.discordAppId = _appID;
                }
                else
                {
                    _settingTarget.discordAppId = -1;
                    GUILayout.Label("THIS IS NOT A VALID LONG TYPE", _redText);
                }
            }
            else
            {
                _settingTarget.discordAppId = -1;
                GUILayout.Label("THIS IS NOT A VALID LONG TYPE", _redText);
            }

            _settingTarget.initializeOnStart = EditorGUILayout.Toggle("Initalize On Start", _settingTarget.initializeOnStart);

            _settingTarget.serverInviteCode = EditorGUILayout.TextField("Discord Server Invite Code", _settingTarget.serverInviteCode);

            _settingTarget.discordLoadFlag = (Discord.CreateFlags)EditorGUILayout.EnumPopup("Discord Load Flag", _settingTarget.discordLoadFlag);
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Default - Will force close the game to load with discord open.");
            EditorGUILayout.LabelField("Not Required - Doesn't require discord to run.");
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Activity Images", EditorStyles.boldLabel);
            _settingTarget.largeImageKey = EditorGUILayout.TextField("Large Image Key", _settingTarget.largeImageKey);
            _settingTarget.largeImageText = EditorGUILayout.TextField("Large Image Text", _settingTarget.largeImageText);
            _settingTarget.smallImageKey = EditorGUILayout.TextField("Small Image Key", _settingTarget.smallImageKey);
            _settingTarget.smallImageText = EditorGUILayout.TextField("Small Image Text", _settingTarget.smallImageText);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Webhooks", EditorStyles.boldLabel);
            _settingTarget.useWebhooks = EditorGUILayout.Toggle("Use Webhooks?", _settingTarget.useWebhooks);

            if (_settingTarget.useWebhooks)
            {
                _settingTarget.defaultWebhookURL = EditorGUILayout.TextField("Default Webhook URL", _settingTarget.defaultWebhookURL);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Steam Intergration", EditorStyles.boldLabel);
            _settingTarget.hasSteamID = EditorGUILayout.Toggle("Have Steam Page?", _settingTarget.hasSteamID);

            if (_settingTarget.hasSteamID)
            {
                steamApp = EditorGUILayout.IntField("Steam App ID", (int)_settingTarget.steamAppID);

                uint _steamID;
                if (uint.TryParse(steamApp.ToString(), out _steamID))
                {
                    _settingTarget.steamAppID = _steamID;
                }
                else
                {
                    _settingTarget.steamAppID = 0;
                    GUILayout.Label("THIS IS NOT A VALID UINT TYPE", _redText);
                }
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Debug Settings", EditorStyles.boldLabel);

            _settingTarget.useDebugLogging = EditorGUILayout.Toggle("Debug Logging", _settingTarget.useDebugLogging);

            if (_settingTarget.useDebugLogging)
            {
                _settingTarget.minLoggingLevel = (Discord.LogLevel)EditorGUILayout.EnumPopup("Min Debug Level", _settingTarget.minLoggingLevel);

                EditorGUILayout.LabelField("Do Not Leave This On In A Full Build", _redText);
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(_settingTarget);
            }
        }
    }
}
