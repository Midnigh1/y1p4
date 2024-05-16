using System;
using GXPEngine;

public class Player2 : Player
{
    Sprite sprite;
    public Player2(int pRadius, Vec2 pPosition) : base(pRadius, pPosition)
    {
        sprite = new Sprite("assets/Cow2.png", addCollider: false);
        AddChild(sprite);
        sprite.width = (int)(3f * pRadius);
        sprite.height = sprite.width;
        //sprite.SetOrigin(pRadius, pRadius);
        //sprite.SetXY(-pRadius, -pRadius);
        sprite.SetOrigin(sprite.width / 2, sprite.width / 2);
        sprite.SetXY(-(width / 5f), -(height / 5f));
    }

}
