using System;
using GXPEngine;

public class Finish2 : Finish
{
    Sprite sprite;
    public Finish2(Vec2 pPosition) : base(pPosition)
    {
        sprite = new Sprite("assets/bucket2.png", addCollider: false);
        sprite.height = GetRadius() * 2;
        sprite.width = GetRadius() * 2;
        AddChild(sprite);
        sprite.SetOrigin(30, 30);

    }
}
