using System;
using GXPEngine;

public class Bomb : Ball
{
    Sprite sprite;
	MyGame myGame;
	public Bomb (Vec2 pPosition, Vec2 pVelocity=new Vec2(), bool pMoving=false, bool pRemovable=false) : base (20, pPosition, pVelocity:pVelocity, pMoving:pMoving, pRemovable:pRemovable)
	{
        sprite = new Sprite("assets/placeholderJumppad.png", addCollider: false);
        AddChild(sprite);
        sprite.SetOrigin(20, 20);
		myGame = (MyGame)game;
		myGame.AddLine(new Vec2(pPosition.x - 10, pPosition.y + 10), new Vec2(pPosition.x + 10, pPosition.y + 10), false, true);
    }

	public void Update()
	{
		
	}
}
