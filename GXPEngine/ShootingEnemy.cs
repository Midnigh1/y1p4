using System;
using GXPEngine;

public class ShootingEnemy : Enemy
{
    int shotCooldown = 1000;
    int shotCooldownRemaining = 200;
    Vec2 shotDirection;
    int projectileSpeed = 10;
    int projectileRadius = 5;
    MyGame myGame;
    Egg[] eggs;
    Sound eggSound;
    
    public ShootingEnemy(Vec2 pPosition, Vec2 pDirection) : base(20, pPosition, pMoving: false, spriteString: "assets/chicken.png")
    {
        shotDirection = pDirection;
        position = pPosition;
        shotDirection = pDirection.Normalized();
        myGame = (MyGame)game;
        SetOrigin(width/2, height/2);
        eggSound = new Sound("assets/gun.mp3");
        
    }

    public new void Update()
    {
        eggs = myGame.FindObjectsOfType<Egg>();
        if (shotCooldownRemaining <= 0 && !myGame._paused && eggs.Length < 10)
        {
            Shoot();
        }
        else
        {
            shotCooldownRemaining -= Time.deltaTime;
        }

    }

/*    private void Shoot()
    {
        MyGame myGame = (MyGame)game;
        Player player = myGame.GetPlayer();
        if (player != null)
        {
            Vec2 toPlayer = player.position - position;
            float distToLine = shotDirection.Normal().Dot(toPlayer); // line means line of fire with this
            float velocityOnLine = shotDirection.Normal().Dot(player.velocity); // but it means velocity on the normal to line of fire with this
            float crossingTime = -distToLine / velocityOnLine;
            if (crossingTime > 0)
            {
                Vec2 crossPoint = player.position + player.velocity * crossingTime;

                float projectileDistToImpact = (position - crossPoint).Length() - (GetRadius() + projectileRadius + 1);
                float bulletTime = projectileDistToImpact / projectileSpeed - crossingTime; // bullet time is time until bullet gets to crosspoint with player (in frames)
                if (bulletTime < 3 && bulletTime > -3) // ususlly there is no exact matching frame so we shoot a bullet at just roughly matching time
                {
                    ((MyGame)parent).AddEnemy(projectileRadius, position + shotDirection * (GetRadius() + projectileRadius + 1), shotDirection * projectileSpeed, pDestroyedByWalls: true, moving:true);
                    shotCooldownRemaining = shotCooldown;
                }
            }
        }
    }*/

    private void Shoot()
    {
        MyGame myGame = (MyGame)game;
        Player player = myGame.GetPlayer();

        if (player != null)
        {
            Vec2 toPlayer = player.position - position;
            float distToPlayer = toPlayer.Length();

            // Check if the player is within shooting range
            if (distToPlayer < 1000)  // Adjust the shooting range as needed
            {
                // Calculate the time it takes for the projectile to reach the player
                float timeToHit = distToPlayer / projectileSpeed;

                // Predict the player's position when the projectile reaches
                Vec2 predictedPlayerPosition = (toPlayer + (player.velocity * timeToHit) + player.acceleration * timeToHit);

                // Calculate the direction to shoot towards the predicted position
                Vec2 shootDirection = (predictedPlayerPosition).Normalized();


                rotation = shootDirection.GetAngleDegrees();

                // Spawn a projectile in the shootDirection

                myGame.AddEgg(projectileRadius, position + shootDirection * (GetRadius() + projectileRadius + 1), shootDirection * projectileSpeed, pDestroyedByWalls: true, moving: true);
                eggSound.Play();

                // Reset the cooldown timer
                shotCooldownRemaining = shotCooldown;
            }
        }
    }

}
