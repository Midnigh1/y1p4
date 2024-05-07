using System;
using System.Security.Permissions;
using GXPEngine;

public class Enemy : Ball
{
	Sprite sprite;
	bool destroyedByWalls;
	public Enemy (int pRadius, Vec2 pPosition, Vec2 pVelocity=new Vec2(), float pBounciness=0.4f, bool pMoving=true, byte pGreenness=70, bool pDestroyedByWalls=false) : base (pRadius, pPosition, pVelocity:pVelocity, pBounciness:pBounciness, pMoving:pMoving, greenness:pGreenness)
	{
        destroyedByWalls = pDestroyedByWalls;
		acceleration = new Vec2(0, 0);

        sprite = new Sprite("assets/placeholderSpike.png", addCollider: false);
        AddChild(sprite);
        sprite.SetOrigin(30, 30);
    }

	public bool IsDestroyedByWalls()
	{
		return destroyedByWalls;
	}

	public void Update()
	{
		
	}
}
