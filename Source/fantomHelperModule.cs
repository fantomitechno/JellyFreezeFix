using System;
using Celeste.Mod.fantomHelper.Fixes;

namespace Celeste.Mod.fantomHelper;

public class fantomHelperModule : EverestModule
{
  public static fantomHelperModule Instance { get; private set; }

  public override Type SettingsType => typeof(fantomHelperModuleSettings);
  public static fantomHelperModuleSettings Settings => (fantomHelperModuleSettings)Instance._Settings;

  public fantomHelperModule()
  {
    Instance = this;
  }


  public override void Load()
  {
    On.Celeste.Actor.OnGround_int += InteractionWhenPlayerDead.FixJellyFrize;
  }


  public override void Unload()
  {
    On.Celeste.Actor.OnGround_int -= InteractionWhenPlayerDead.FixJellyFrize;
  }
}