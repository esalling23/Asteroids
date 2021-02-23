using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    List<Sprite> sprites = new List<Sprite>();

    GameObject asteroidPrefab;

    Dictionary<Direction, Vector2> startingLocations = new Dictionary<Direction, Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        asteroidPrefab = Resources.Load("Prefabs/Asteroid") as GameObject;

        LoadSprites();

        // Spawn starting asteroids
        // startingLocations contains the direction & location of each
        startingLocations.Add(Direction.Up, new Vector2(0, ScreenUtils.ScreenBottom));
        startingLocations.Add(Direction.Down, new Vector2(0, ScreenUtils.ScreenTop));
        startingLocations.Add(Direction.Left, new Vector2(ScreenUtils.ScreenRight, 0));
        startingLocations.Add(Direction.Right, new Vector2(ScreenUtils.ScreenLeft, 0));

        // Loop over startingLocations & spawn each
        foreach (KeyValuePair<Direction, Vector2> location in startingLocations)
        {
            Spawn(location.Key, location.Value);
        }
    }

    private void Spawn(Direction direction, Vector2 location)
    {
        // Spawn new asteroid object
        GameObject newAsteroid = Instantiate(asteroidPrefab,
            location, Quaternion.identity);

        // Give it a random sprite
        int ran = Random.Range(0, sprites.Count - 1);
        newAsteroid.GetComponent<SpriteRenderer>().sprite = sprites[ran];

        // Initialize it w/ direction
        newAsteroid.GetComponent<Asteroid>().Initialize(direction);
    }

    private void LoadSprites()
    {
        sprites.Add(Resources.Load<Sprite>("Sprites/asteroid1"));
        sprites.Add(Resources.Load<Sprite>("Sprites/asteroid2"));
        sprites.Add(Resources.Load<Sprite>("Sprites/asteroid3"));
    }

    public void ReplenishAsteroid()
    {
        Direction ranDir = (Direction)Random.Range(0, 4);

        // An asteroid has been destroyed! Time to make another
        Spawn(ranDir, startingLocations[ranDir]);
    }
}
