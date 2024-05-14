using System;
using GXPEngine;

public class Enemy : Ball
{
	bool destroyedByWalls;
	Sprite sprite;
	public Enemy (int pRadius, Vec2 pPosition, Vec2 pVelocity=new Vec2(), float pBounciness=0.4f, bool pMoving=false, bool pRemovable=false, bool pDestroyedByWalls=false, string spriteString = "assets/placeholderSpike.png") : base (pRadius, pPosition, pVelocity:pVelocity, pBounciness:pBounciness, pMoving:pMoving, pRemovable: pRemovable)
	{
        destroyedByWalls = pDestroyedByWalls;
		acceleration = new Vec2(0, 0);

        sprite = new Sprite(spriteString, addCollider: false);
		sprite.width = 75;
		sprite.height = 75;
        AddChild(sprite);
        sprite.SetOrigin(sprite.width/2, sprite.height/2);
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
		
	}
}
