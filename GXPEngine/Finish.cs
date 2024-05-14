using System;
using GXPEngine;

public class Finish : Ball
{
    Sprite sprite;
    public Finish(Vec2 pPosition) : base (30, pPosition, pMoving:false)
	{
        sprite = new Sprite("assets/bucket.png", addCollider: false);
        sprite.height = GetRadius() * 2;
        sprite.width = GetRadius() * 2;
        AddChild(sprite);
        sprite.SetOrigin(30, 30);
        
    }
}
