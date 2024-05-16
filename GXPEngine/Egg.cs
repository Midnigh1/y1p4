using System;
using GXPEngine;

public class Egg : Ball
{
    Sprite sprite;
    bool destroyedByWalls;

    public Egg(int pRadius, Vec2 pPosition, Vec2 pVelocity = new Vec2(), float pBounciness = 0.4f, bool pMoving = true, byte pGreenness = 70, bool pDestroyedByWalls = true) : base(pRadius, pPosition, pVelocity: pVelocity, pBounciness: pBounciness, pMoving: pMoving, pRemovable: true)
    {
        //insert egg joke here
        destroyedByWalls = pDestroyedByWalls;
        acceleration = new Vec2(0, 0);
        
        sprite = new Sprite("assets/egg.png", addCollider: false);
        sprite.width = pRadius  * 4;
        sprite.height = pRadius * 4;
        AddChild(sprite);
        // sprite.SetOrigin(this.width/2, this.height/2);
    }

    public bool IsDestroyedByWalls()
    {
        return destroyedByWalls;
    }
}