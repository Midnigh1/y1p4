using System;
using GXPEngine;
using System.Drawing; // For color


/// <summary>
/// An example of a simple particle class - feel free to extend this with more properties!
/// </summary>
class Particle : Sprite {
	// Some example particle parameters (feel free to add more!):

	float vx, vy; // horizontal and vertical velocity
	Color startColor = Color.White;
	Color endColor = Color.White;
	float startScale = 1;
	float endScale = 1;

	// Life time variables:

	int totalLifeTimeMs;
	int currentLifeTimeMs = 0;

	public Particle(string filename, BlendMode blendMode, int lifeTimeMs) : base(filename, true, false) { // no colliders for particles!
		this.blendMode = blendMode;
		totalLifeTimeMs = lifeTimeMs;
		SetOrigin(width / 2, height / 2);
	}

	public Particle SetScale(float start, float end) {
		startScale = start;
		endScale = end;
		scale = start;
		return this; // allows for chaining
	}

	public Particle SetColor(Color start, Color end) {
		startColor = start;
		endColor = end;
		SetColor(start.R/255f, start.G/255f, start.B/255f);
		return this; // allows for chaining
	}

	public Particle SetVelocity(float velX, float velY) {
		vx = velX;
		vy = velY;
		return this; // allows for chaining
	}



	void Update() {
		// Keep track of this particle's life time:
		currentLifeTimeMs += Time.deltaTime;
		// Make a parameter that goes from 0 to 1 throughout the lifetime of the particle:
		float t = Mathf.Clamp(1f * currentLifeTimeMs / totalLifeTimeMs, 0, 1);

		// ---- Interpolate parameters, using linear interpolation (alternatively, you can use tweening curves here too!):

		// interpolate color:
		float currentR = startColor.R * (1 - t) + endColor.R * t;
		float currentG = startColor.G * (1 - t) + endColor.G * t;
		float currentB = startColor.B * (1 - t) + endColor.B * t;
		SetColor(currentR/255f, currentG/255f, currentB/255f);

		// interpolate scale:
		scale = startScale * (1 - t) + endScale * t;

		// Move:
		x += vx;
		y += vy;

		if (currentLifeTimeMs>=totalLifeTimeMs) {
			Destroy();
		}
	}

}

