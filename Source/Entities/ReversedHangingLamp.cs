using System;
using System.Collections.Generic;
using System.Linq;
using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.fantomHelper.Entities;

[CustomEntity("fantomHelper/ReversedHangingLamp")]
public class ReversedHangingLamp : Entity
{
  public ReversedHangingLamp(Vector2 position, int length)
  {
    this.Position = position - Vector2.UnitX * 4f;
    this.Length = Math.Max(16, length);
    base.Depth = 2000;
    MTexture mtexture = GFX.Game["objects/hanginglamp"];
    Image image;
    for (int i = 0; i < this.Length - 8; i += 8)
    {
      base.Add(image = new Image(mtexture.GetSubtexture(0, 8, 8, 8, null)));
      image.Origin.X = 5f;
      image.Origin.Y = i + 8f;
      this.images.Add(image);
    }
    base.Add(image = new Image(mtexture.GetSubtexture(0, 0, 8, 8, null)));
    image.Origin.X = 4f;
    image.FlipY = true;
    base.Add(image = new Image(mtexture.GetSubtexture(0, 16, 8, 8, null)));
    image.Origin.X = 5f;
    image.Origin.Y = this.Length - 8f;
    image.FlipY = true;
    this.images.Add(image);
    base.Add(this.bloom = new BloomPoint(Vector2.UnitY * (float)(this.Length - 4), 1f, 48f));
    base.Add(this.light = new VertexLight(Vector2.UnitY * (float)(this.Length - 4), Color.White, 1f, 24, 48));
    base.Add(this.sfx = new SoundSource());
    base.Collider = new Hitbox(8f, (float)this.Length, -4f, 8f - this.Length);
  }

  public ReversedHangingLamp(EntityData e, Vector2 position) : this(e.Position, Math.Max(16, e.Height))
  {
  }


  public override void Update()
  {
    base.Update();
    this.soundDelay -= Engine.DeltaTime;
    Player entity = base.Scene.Tracker.GetEntity<Player>();
    if (entity != null && base.Collider.Collide(entity))
    {
      this.speed = -entity.Speed.X * 0.005f * ((-base.Y - this.Length + entity.Y) / (float)this.Length);
      if (Math.Abs(this.speed) < 0.1f)
      {
        this.speed = 0f;
      }
      else if (this.soundDelay <= 0f)
      {
        this.sfx.Play("event:/game/02_old_site/lantern_hit", null, 0f);
        this.soundDelay = 0.25f;
      }
    }
    float num = (Math.Sign(this.rotation) == Math.Sign(this.speed)) ? 8f : 6f;
    if (Math.Abs(this.rotation) < 0.5f)
    {
      num *= 0.5f;
    }
    if (Math.Abs(this.rotation) < 0.25f)
    {
      num *= 0.5f;
    }
    float value = this.rotation;
    this.speed += (float)(-(float)Math.Sign(this.rotation)) * num * Engine.DeltaTime;
    this.rotation += this.speed * Engine.DeltaTime;
    this.rotation = Calc.Clamp(this.rotation, -0.4f, 0.4f);
    if (Math.Abs(this.rotation) < 0.02f && Math.Abs(this.speed) < 0.2f)
    {
      this.rotation = (this.speed = 0f);
    }
    else if (Math.Sign(this.rotation) != Math.Sign(value) && this.soundDelay <= 0f && Math.Abs(this.speed) > 0.5f)
    {
      this.sfx.Play("event:/game/02_old_site/lantern_hit", null, 0f);
      this.soundDelay = 0.25f;
    }
    foreach (Image image in this.images)
    {
      image.Rotation = this.rotation;
    }
    Vector2 position = Calc.AngleToVector(this.rotation + 1.5707964f, (float)this.Length - 4f);
    this.bloom.Position = (this.light.Position = position);
    this.sfx.Position = position;
  }

  public override void Render()
  {
    foreach (Component component in base.Components)
    {
      Image image = component as Image;
      if (image != null)
      {
        image.DrawOutline(1);
      }
    }
    base.Render();
  }

  public readonly int Length;

  private List<Image> images = new List<Image>();

  private BloomPoint bloom;

  private VertexLight light;

  private float speed;

  private float rotation;

  private float soundDelay;

  private SoundSource sfx;
}
