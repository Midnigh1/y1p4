using System;
using GXPEngine;

public class Reset : Ball
{
    Sprite sprite;
    float radius; 

    public Reset(int pRadius, Vec2 pPosition) : base(pRadius, pPosition, pMoving: false)
    {
        sprite = new Sprite("assets/Cow.png", addCollider: false);
        AddChild(sprite);
        sprite.width = (int)(2.2f * pRadius);
        sprite.height = sprite.width;
        sprite.SetOrigin(pRadius, pRadius);
        sprite.SetXY(-pRadius, -pRadius);
        radius = pRadius;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (Input.mouseX < x + radius) && (Input.mouseX > x - radius) && (Input.mouseY < y + radius) && (Input.mouseY > y - radius))
        {
            Console.WriteLine("bam");
        }
    }

}
