using System;
using System.Security.Permissions;
using GXPEngine;

public class ShootingEnemy : Enemy
{
	int shotCooldown = 1000;
	int shotCooldownRemaining = 200;
    Vec2 shotDirection;
	int projectileSpeed = 3;
    int projectileRadius = 5;
    public ShootingEnemy(Vec2 pPosition, Vec2 pDirection) : base (20, pPosition, pMoving:false, pGreenness:0)
	{
        shotDirection = pDirection;
        position = pPosition;
        shotDirection = pDirection.Normalized();
    }

	public void Update()
	{
        if (shotCooldownRemaining <= 0)
        {
            Shoot();
        }
        else
        {
            shotCooldownRemaining -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        MyGame myGame = (MyGame)game;
        Player player = myGame.GetPlayer();
        if (player != null)
        {
            Vec2 toPlayer = player.position - position;
            float distToLine = shotDirection.Normal().Dot(toPlayer); // line means line of fire with this
            float velocityOnLine = shotDirection.Normal().Dot(player.velocity); // but it means velocity on the normal to line of fire with this
            float crossingTime = -distToLine / velocityOnLine;
            if(crossingTime > 0)
            {
                Vec2 crossPoint = player.position + player.velocity * crossingTime;

                float projectileDistToImpact = (position - crossPoint).Length() - (radius + projectileRadius + 1);
                float bulletTime = projectileDistToImpact / projectileSpeed - crossingTime; // bullet time is time until bullet gets to crosspoint with player (in frames)
                if (bulletTime < 3 && bulletTime > -3) // ususlly there is no exact matching frame so we shoot a bullet at just roughly matching time
                {
                    ((MyGame)parent).AddEnemy(projectileRadius, position + shotDirection * (radius + projectileRadius + 1), shotDirection * projectileSpeed, pDestroyedByWalls:true);
                    shotCooldownRemaining = shotCooldown;
                }
            }
        }
    }
}
