using System;
using System.Collections.Generic;
using GXPEngine;

public class Enemy : Ball
{
	bool destroyedByWalls;
	Sprite sprite;
	public Enemy (int pRadius, Vec2 pPosition, Vec2 pVelocity=new Vec2(), float pBounciness=0.4f, bool pMoving=false, bool pRemovable=false, bool pDestroyedByWalls=false, string spriteString = "assets/fork.png") : base (pRadius, pPosition, pVelocity:pVelocity, pBounciness:pBounciness, pMoving:pMoving, pRemovable: pRemovable)
	{
        destroyedByWalls = pDestroyedByWalls;
		acceleration = new Vec2(0, 0);

        sprite = new Sprite(spriteString, addCollider: false);
		//sprite.width = 75;
		//sprite.height = 75;
        AddChild(sprite);
		sprite.SetOrigin(sprite.width/2, 4*sprite.height / 5);
		sprite.rotation = 180;
    }

	public bool IsDestroyedByWalls()
	{
		return destroyedByWalls;
	}

	public void SetSprite(string filename)
	{
		sprite = new Sprite(filename, addCollider: false);
    }

    public void HideSprite()
    {
        sprite.visible = false;
    }

    public void Update()
	{
		List<Ball> balls = GetAllBallOverlaps();
		//Console.WriteLine(balls.Count);
		foreach (Ball ball in balls)
		{
			if(ball is Player)
			{
                MyGame myGame = (MyGame)game;
                myGame.RemovePlayer();
                myGame.Pause();
                loseSound.Play();

                myGame.gameOver.Text("Game Over\nPress R to restart the level", game.width / 2, game.height / 2);
            }
        }
    }
}
