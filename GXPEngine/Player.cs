using System;
using GXPEngine;

public class Player : Ball
{
	Sprite sprite;
	public Player (int pRadius, Vec2 pPosition) : base (pRadius, pPosition, pBounciness:0.92f)
	{
		sprite = new Sprite("assets/Cow.png", addCollider:false);
		AddChild(sprite);
		sprite.width = (int)(3f * pRadius);
		sprite.height = sprite.width;
        //sprite.SetOrigin(pRadius, pRadius);
        //sprite.SetXY(-pRadius, -pRadius);
        sprite.SetOrigin(sprite.width/2, sprite.width/2);
        sprite.SetXY(-(width / 5f), -(height / 5f));

		if (this is Player2)
		{
			sprite.alpha = 0;
		}
    }

}
