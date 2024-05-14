using System;
using GXPEngine;

public class Enemy : Ball
{
	Sprite sprite;
	bool destroyedByWalls;
	public Enemy (int pRadius, Vec2 pPosition, Vec2 pVelocity=new Vec2(), float pBounciness=0.4f, bool pMoving=false, bool pRemovable=false, bool pDestroyedByWalls=false) : base (pRadius, pPosition, pVelocity:pVelocity, pBounciness:pBounciness, pMoving:pMoving, pRemovable: pRemovable)
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
