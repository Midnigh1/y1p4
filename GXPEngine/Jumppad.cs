using System;
using GXPEngine;

public class Bomb : Ball
{
    Sprite sprite;
	public Bomb (Vec2 pPosition, Vec2 pVelocity=new Vec2(), bool pMoving=false) : base (20, pPosition, pVelocity:pVelocity, pMoving:pMoving, greenness:100)
	{
        sprite = new Sprite("assets/placeholderJumppad.png", addCollider: false);
        AddChild(sprite);
        sprite.SetOrigin(20, 20);
    }

	public void Update()
	{
		
	}
}
