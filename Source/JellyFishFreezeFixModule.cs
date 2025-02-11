using System;

namespace Celeste.Mod.JellyFishFreezeFix;

public class JellyFishFreezeFixModule : EverestModule {
    public static JellyFishFreezeFixModule Instance { get; private set; }

    public override Type SettingsType => typeof(JellyFishFreezeFixModuleSettings);
    public static JellyFishFreezeFixModuleSettings Settings => (JellyFishFreezeFixModuleSettings) Instance._Settings;

    public override Type SessionType => typeof(JellyFishFreezeFixModuleSession);
    public static JellyFishFreezeFixModuleSession Session => (JellyFishFreezeFixModuleSession) Instance._Session;

    public override Type SaveDataType => typeof(JellyFishFreezeFixModuleSaveData);
    public static JellyFishFreezeFixModuleSaveData SaveData => (JellyFishFreezeFixModuleSaveData) Instance._SaveData;

    public JellyFishFreezeFixModule() {
        Instance = this;
#if DEBUG
        // debug builds use verbose logging
        Logger.SetLogLevel(nameof(JellyFishFreezeFixModule), LogLevel.Verbose);
#else
        // release builds use info logging to reduce spam in log files
        Logger.SetLogLevel(nameof(JellyFishFreezeFixModule), LogLevel.Info);
#endif
    }

    public override void Load() {
        // TODO: apply any hooks that should always be active
    }

    public override void Unload() {
        // TODO: unapply any hooks applied in Load()
    }
}