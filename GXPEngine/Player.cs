using System;
using GXPEngine;

public class Player : Ball
{
	Sprite sprite;
	public Player (int pRadius, Vec2 pPosition) : base (pRadius, pPosition, pBounciness:0.92f)
	{
		sprite = new Sprite("assets/Cow.png", addCollider:false);
		AddChild(sprite);
		sprite.width = (int)(2.2f * pRadius);
		sprite.height = sprite.width;
		sprite.SetOrigin(pRadius, pRadius);
        sprite.SetXY(-pRadius, -pRadius);
    }

}
