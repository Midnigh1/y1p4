using System;
using GXPEngine;

public class Finish2 : Finish
{
    AnimationSprite sprite;
    public Finish2(Vec2 pPosition) : base(pPosition)
    {
        sprite = new AnimationSprite("assets/redBucket.png", 3, 3, addCollider: false);
        sprite.height = GetRadius() * 4;
        sprite.width = GetRadius() * 4;
        AddChild(sprite);
        sprite.SetOrigin(60, 60);
        sprite.SetCycle(8, 1);
    }

    public void playAnimation()
    {
        sprite.SetCycle(0, 9);
    }

    void Update()
    {
        sprite.Animate(0.4f);
    }
}
