using System;

namespace Celeste.Mod.JellyFishFreezeFix;

public class JellyFishFreezeFixModule : EverestModule
{
  public static JellyFishFreezeFixModule Instance { get; private set; }

  public override Type SettingsType => typeof(JellyFishFreezeFixModuleSettings);
  public static JellyFishFreezeFixModuleSettings Settings => (JellyFishFreezeFixModuleSettings)Instance._Settings;

  public JellyFishFreezeFixModule()
  {
    Instance = this;
  }

  private bool FixJellyFrize(On.Celeste.Actor.orig_OnGround_int orig, Actor self, int downCheck)
  {
    if (Settings.RemoveDeadPlayerCollision && self.GetType() == typeof(Player))
    {
      Player pl = (Player)self;
      if (pl.Dead) return false;
    }
    return orig(self, downCheck);
  }


  public override void Load()
  {
    On.Celeste.Actor.OnGround_int += FixJellyFrize;
  }


  public override void Unload()
  {
    On.Celeste.Actor.OnGround_int -= FixJellyFrize;
  }
}