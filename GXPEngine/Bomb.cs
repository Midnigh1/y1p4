using System;
using System.Security.Permissions;
using GXPEngine;

public class Bomb : Ball
{
	public int explosionForce = 800000;
	public Bomb (Vec2 pPosition, Vec2 pVelocity=new Vec2(), bool pMoving=true) : base (20, pPosition, pVelocity:pVelocity, moving:pMoving, greenness:100)
	{
		
	}

	public void Update()
	{
		
	}

	public void Explode()
	{
        MyGame myGame = (MyGame)game;
        for (int i = myGame.GetNumberOfMovers(); i >= 0; i--) // go over every ball and apply some force to it
        {
            Ball mover = myGame.GetMover(i);
            if (mover != null)
            {
                if (mover != this)
                {
                    Vec2 explosionDirection = position - mover.position;
                    mover.velocity -= explosionDirection.Normalized() * (explosionForce / mover.Mass / explosionDirection.Length()); // F = m*a and divide by distance to simulate power dropoff
                }
                else
                {
                    myGame.RemoveMover(i);
                }
            }
        }
    }
}
