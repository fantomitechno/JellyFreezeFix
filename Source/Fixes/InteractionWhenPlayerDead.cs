namespace Celeste.Mod.fantomHelper.Fixes;

public class InteractionWhenPlayerDead
{

  public static bool FixJellyFrize(On.Celeste.Actor.orig_OnGround_int orig, Actor self, int downCheck)
  {
    if (fantomHelperModule.Settings.RemoveDeadPlayerCollision && self.GetType() == typeof(Player))
    {
      Player pl = (Player)self;
      if (pl.Dead) return false;
    }
    return orig(self, downCheck);
  }
}