using System;
using GXPEngine;

public class Collectable : Ball
{
	Sprite sprite;
	public Collectable (Vec2 pPosition) : base (30, pPosition, pMoving:false)
	{
        sprite = new Sprite("assets/placeholderCow.png", addCollider: false);
        AddChild(sprite);
        sprite.SetOrigin(30, 30);
    }
}
