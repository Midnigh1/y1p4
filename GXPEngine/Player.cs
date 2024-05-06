using System;
using System.Security.Permissions;
using GXPEngine;

public class Player : Ball
{
	Barrel gun;
	public Player (int pRadius, Vec2 pPosition) : base (pRadius, pPosition, pBounciness:0.98f)
	{
		gun = new Barrel();
		AddChild(gun);
	}

	public void DeleteGun()
	{
		gun.LateDestroy();
	}

	public int[] GetRemainingUses() // remeining uses of items
	{
		return gun.GetRemainingUses();
    }

    public void SetRemainingUses(int[] array)
    {
        gun.SetRemainingUses(array);
    }
}
