using System;
using System.Security.Permissions;
using GXPEngine;

public class Player : Ball
{
	Sprite sprite;
	public Player (int pRadius, Vec2 pPosition) : base (pRadius, pPosition, pBounciness:0.92f)
	{
		sprite = new Sprite("assets/placeholderCow.png", addCollider:false);
		AddChild(sprite);
		sprite.SetOrigin(pRadius, pRadius);
	}
}
