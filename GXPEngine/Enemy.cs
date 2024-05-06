using System;
using System.Security.Permissions;
using GXPEngine;

public class Enemy : Ball
{
	bool destroyedByWalls;
	public Enemy (int pRadius, Vec2 pPosition, Vec2 pVelocity=new Vec2(), float pBounciness=0.4f, bool pMoving=true, byte pGreenness=70, bool pDestroyedByWalls=false) : base (pRadius, pPosition, pVelocity:pVelocity, pBounciness:pBounciness, moving:pMoving, greenness:pGreenness)
	{
        destroyedByWalls = pDestroyedByWalls;
    }

	public bool IsDestroyedByWalls()
	{
		return destroyedByWalls;
	}

	public void Update()
	{
		
	}
}
