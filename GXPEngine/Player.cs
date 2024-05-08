using System;
using GXPEngine;

public class Player : Ball
{
	Sprite sprite;
	public Player (int pRadius, Vec2 pPosition) : base (pRadius, pPosition, pBounciness:0.92f)
	{
		sprite = new Sprite("assets/Cow.png", addCollider:false);
        sprite.width = pRadius * 2;
		sprite.height = pRadius * 2;
		AddChild(sprite);
		sprite.SetXY(-pRadius, -pRadius);
	}
}