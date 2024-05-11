using System;
using GXPEngine;

public class Finish : Ball
{
    Sprite sprite;
    public Finish(Vec2 pPosition) : base (30, pPosition, pMoving:false, greenness:250)
	{
        sprite = new Sprite("assets/bucket.png", addCollider: false);
        AddChild(sprite);
        sprite.SetOrigin(30, 30);
    }

	public void Update()
	{
		
	}
}
