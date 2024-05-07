using System;
using System.Drawing;
using GXPEngine;

public class Ball : EasyDraw
{
	// These four public static fields are changed from MyGame, based on key input (see Console):
	public static bool drawDebugLine = false;
	public static bool wordy = false;
	public float bounciness = 0.6f;
	// For ease of testing / changing, we assume every ball has the same acceleration (gravity):
	public Vec2 acceleration = new Vec2 (0, 1);


	public Vec2 velocity;
	public Vec2 position;

	public readonly int radius;
	public readonly bool moving;

	// Mass = density * volume.
	// In 2D, we assume volume = area (=all objects are assumed to have the same "depth")
	public float Mass {
		get {
			return radius * radius * _density;
		}
	}

	public bool IsMoving()
	{
		return moving;
	}

	private Vec2 _oldPosition;
	private Arrow _velocityIndicator;

	private float _density = 1;
	public Ball (int pRadius, Vec2 pPosition, Vec2 pVelocity=new Vec2(), bool pMoving=true, float pBounciness=0.6f, byte greenness=150) : base (pRadius*2 + 1, pRadius*2 + 1)
	{
		radius = pRadius;
		position = pPosition;
		velocity = pVelocity;
		moving = pMoving;
		bounciness = pBounciness;

        position = pPosition;
		UpdateScreenPosition ();
		SetOrigin (radius, radius);

		Draw (150, greenness, 0);

		_velocityIndicator = new Arrow(position, new Vec2(0,0), 10);
		// AddChild(_velocityIndicator);

		if(!moving)
		{
			acceleration = new Vec2(0, 0);
		}
	}

	void Draw(byte red, byte green, byte blue) {
		Fill (red, green, blue);
		Stroke (red, green, blue);
		Ellipse (radius, radius, 2*radius, 2*radius);
	}

	void UpdateScreenPosition() {
		x = position.x;
		y = position.y;
	}

	public void Step () {

		velocity += acceleration; // euler goes whooosh

		bool repeat = true;
		while (repeat) // we are not counting collisions at the start of the frame to avoid phasing through objects when things are not moving a lot
		{
			_oldPosition = position;
			position += velocity;

			CollisionInfo firstCollision = FindEarliestCollision();
			if (firstCollision != null)
			{
				ResolveCollision(firstCollision);
				if (firstCollision.timeOfImpact < 0.00001)
				{
                    repeat = true;
                }
				else
				{
					repeat = false;
				}
			}
            else
            {
                repeat = false;
            }
        }

		UpdateScreenPosition();
		ShowDebugInfo();
	}

    CollisionInfo CheckAllBalls()
	{
        MyGame myGame = (MyGame)game;
        for (int i = 0; i < myGame.GetNumberOfMovers(); i++)
        {
            Ball mover = myGame.GetMover(i);
            if (mover != this)
            {
				CollisionInfo col = CheckOneBall(mover);
				if (col != null)
				{
					return col;
				}
            }
        }
		return null;
    }

	CollisionInfo CheckOneBall(Ball mover) // this is just quadratic formula from slides
	{
        Vec2 relativePosition = _oldPosition - mover.position;
        if (this.velocity.Length() != 0)
        {
            float a = (float)Math.Pow(this.velocity.Length(), 2);
            float b = 2 * relativePosition.Dot(this.velocity);
            float c = (float)(Math.Pow(relativePosition.Length(), 2) - Math.Pow((this.radius + mover.radius), 2));
            if (c < 0 && b < 0)
            {
                return new CollisionInfo((this._oldPosition - mover.position).Normalized(), mover, 0);
            }
            if (b * b - 4 * a * c >= 0)
            {
                float timeOfImpact = (float)((-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a));
                if (timeOfImpact > 0 && timeOfImpact <= 1)
                {
                    return new CollisionInfo(((this._oldPosition + this.velocity * timeOfImpact) - mover.position).Normalized(), mover, timeOfImpact);
                }
            }
        }
		return null;
    }

	CollisionInfo CheckAllLines()
	{
        MyGame myGame = (MyGame)game;
        for (int i = 0; i < myGame.GetNumberOfLines(); i++)
        {
            LineSegment line = myGame.GetLine(i);
            CollisionInfo col = CheckOneLine(line);
			if(col != null)
			{
				return col;
			}
        }
        return null;
    }

	CollisionInfo CheckOneLine(LineSegment line)
	{
        Vec2 startLine = line.start;
        Vec2 endLine = line.end;

        Vec2 oldDifferenceVector = _oldPosition - startLine;
        float a = oldDifferenceVector.Dot((endLine - startLine).Normal()) - radius; // names a and b are from slides week 4/5; the slides didn't bother naming them properly, so neither will i

        float b = (_oldPosition - position).Dot((endLine - startLine).Normal());

        float ballDistance = (position - startLine).Dot((endLine - startLine).Normal());

        float timeOfImpact = 2; // if neither of the two conditions below are true, the collision is not happening anyway
        if (a >= 0)
        {
            timeOfImpact = a / b;
        }
        else if (a >= -radius)
        {
            timeOfImpact = 0;
        }

        if (b > 0 && ballDistance <= radius && timeOfImpact <= 1) // this is stuff from slides again
        {

            Vec2 addDist = velocity * timeOfImpact;
            Vec2 pointOfImpact = _oldPosition + addDist;

            float distAlongLine = (pointOfImpact - startLine).Dot((endLine - startLine).Normalized());



            if (distAlongLine > 0 && distAlongLine < (startLine - endLine).Length())
            {
                if (a > -radius && a < 0)
                {
                    return new CollisionInfo((endLine - startLine).Normal(), line, 0);
                }
                return new CollisionInfo((endLine - startLine).Normal(), line, timeOfImpact);
            }
        }

        // line caps

        Ball lineCap = new Ball(0, startLine, pMoving:false);
        CollisionInfo capCol = CheckOneBall(lineCap);
		return capCol; // dont need to check for null since it's the last return anyway
    }

	CollisionInfo FindEarliestCollision() {
		MyGame myGame = (MyGame)game;
		// Check other balls:
		CollisionInfo ballCollision = CheckAllBalls();
		if (ballCollision != null) 
		{
			return ballCollision;
		}

        // check lines
        CollisionInfo lineCollision = CheckAllLines();
		return lineCollision;
	}

	void ResolveCollision(CollisionInfo col) {

		if(col.timeOfImpact == 0) // making sure we don't fall through stuff 
		{
			position = _oldPosition;
		}
		else
		{
            position -= velocity * (1 - col.timeOfImpact);
        }

        UpdateScreenPosition();

        if (col.other is Ball)
        {
            Ball otherBall = (Ball)col.other;
			
			if (otherBall.IsMoving())
			{
                otherBall.velocity.Reflect((otherBall.bounciness + bounciness) / 2, col.normal);
                velocity.Reflect((otherBall.bounciness + bounciness) / 2, col.normal);
                // ditch the newton, i think i fucked something up

                /*if (col.timeOfImpact == 0)
				{
					// no newtons laws here so Step doesn't get stuck in an infinite loop
					otherBall.velocity.Reflect((otherBall.bounciness + bounciness) / 2, col.normal);
					velocity.Reflect((otherBall.bounciness + bounciness) / 2, col.normal);
				}
				else // newton is happy here (his laws work)
				{
					float cor = (otherBall.bounciness + bounciness) / 2; // cor is coefficient of reflection
					Vec2 massCenterVelocity = (this.velocity * this.Mass + otherBall.velocity * otherBall.Mass) / (this.Mass + otherBall.Mass);
					velocity = massCenterVelocity - cor * (this.velocity - massCenterVelocity);;
					otherBall.velocity = massCenterVelocity - cor * (otherBall.velocity - massCenterVelocity);
				}*/
            }
            else
			{
                velocity.Reflect((otherBall.bounciness + bounciness) / 2, col.normal);
            }
            if ((otherBall is Enemy && this is Player) || (this is Enemy && otherBall is Player))
            {
                MyGame myGame = (MyGame)game;
                myGame.RemovePlayer();
                myGame.Pause();

                myGame.gameOver.Text("Game Over", game.width / 2, game.height / 2);
            }
			// bombs are not family friendly so we will make them into jump pads instead
			// else if (this is Bomb || otherBall is Bomb)
			// {
            //     if (this is Bomb)
            //     {
			// 		((Bomb)this).Explode();
            //     }
			// 	else
			// 	{
			// 		((Bomb)otherBall).Explode();
			// 	}
            // }
			else if (otherBall is Bomb) 
			{
				this.velocity += new Vec2(0, -30);
			}
            else if (otherBall is Finish && this is Player)
            {
                MyGame myGame = (MyGame)game;
                myGame.RemovePlayer();
                myGame.Pause();

                myGame.gameOver.Text("You won", game.width / 2, game.height / 2);
            }
        }
		else
		{
            velocity.Reflect(bounciness, col.normal);
            if (this is Bomb)
            {
                ((Bomb)this).Explode();
            }
			else if(this is Enemy && ((Enemy)this).IsDestroyedByWalls())
			{
				((MyGame)game).RemoveMover(this);
			}
        }
    }


	void BoundaryWrapAround() {
		if (position.x < 0) {
			position.x += game.width;
		}
		if (position.x > game.width) {			
			position.x -= game.width;
		}
		if (position.y < 0) {
			position.y += game.height;
		}
		if (position.y > game.height) {
			position.y -= game.height;
		}
	}

	void ShowDebugInfo() {
		if (drawDebugLine) {
			((MyGame)game).DrawLine (_oldPosition, position);
		}
		_velocityIndicator.startPoint = position;
		_velocityIndicator.vector = velocity;
	}
}

