using System;
using GXPEngine;

public class Axe : AnimationSprite
{
	Enemy axe1;
    Enemy axe2;
	public Axe (Vec2 pPosition) : base ("assets/rotatingaxe.png", 5, 5)
	{
        SetXY(pPosition.x, pPosition.y);
        SetCycle(0, 21);
        axe1 = new Enemy(10, pPosition);
        axe2 = new Enemy(10, pPosition);
        axe2.SetXY(pPosition.x, pPosition.y + 100);
        MyGame mygame = (MyGame)game;
        mygame.AddExistingMover(axe1);
        mygame.AddExistingMover(axe2);
    }
	public void Update()
	{
		Animate();
        axe1.SetXY(Mathf.Cos(((float)this.currentFrame / 21) * 2 * Mathf.PI), Mathf.Sin(((float)this.currentFrame / 21) * 2 * Mathf.PI));
        axe2.SetXY(Mathf.Cos(Mathf.PI + ((float)this.currentFrame / 21) * 2 * Mathf.PI), Mathf.Sin(Mathf.PI + ((float)this.currentFrame / 21) * 2 * Mathf.PI));
    }
}
