using System;
using GXPEngine;

public class Bomb : Ball
{
    Sprite sprite;
	MyGame myGame;
	public Bomb (Vec2 pPosition, Vec2 pVelocity=new Vec2(), bool pMoving=false, bool pRemovable=false) : base (20, pPosition, pVelocity:pVelocity, pMoving:pMoving, pRemovable:pRemovable)
	{
        sprite = new Sprite("assets/jumppad.png", addCollider: false);
        AddChild(sprite);
        
		sprite.width = 50;
		sprite.height = 50;
		sprite.SetXY(sprite.x - 25, sprite.y + 25);
		sprite.rotation = 270;
		myGame = (MyGame)game;
		myGame.AddLine(new Vec2(pPosition.x - 10, pPosition.y + 10), new Vec2(pPosition.x + 10, pPosition.y + 10), true, false);
    }

	public void Update()
	{
		
	}
}
