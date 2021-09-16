using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width;
    public int height;
    public int borderSize;

    public float gemSpeed;

    public GameObject bgTilePrefab;

    public Gem[] gems; // an array of the Gems prefabs
    public Gem[,] allGems; // 2d array of Gems

    [HideInInspector]
    public MatchFinder matchFind;


    private void Awake()
    {
        matchFind = FindObjectOfType<MatchFinder>();
        allGems = new Gem[width, height]; //inialize the array with width and height
    }

    // Start is called before the first frame update
    void Start()
    {        

        Setup();// run the setup to set up the board
        SetupCamera(); // adjusts camera to board size
    }

    private void Update()
    {
        matchFind.FindAllMatches();
    }

    private void Setup()
    {
        //Sets up the board
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 pos = new Vector2(x, y);
                GameObject bgTile = Instantiate(bgTilePrefab, pos, Quaternion.identity); //create and place the BGtile
                bgTile.transform.parent = transform; // set the BGtile to be a child of the Board
                bgTile.name = "BG Tile - " + x + ", " + y; // name each tile as the coordinates

                int gemToUse = Random.Range(0, gems.Length); //making a random selection of the gems



                SpawnGem(new Vector2Int(x, y), gems[gemToUse]); //spawns the random gem at the new location
            }
        }

    }



    private void SpawnGem(Vector2Int pos, Gem gemToSpawn)
    {
        Gem gem = Instantiate(gemToSpawn, new Vector3(pos.x, pos.y, 0), Quaternion.identity); //instaniates the gem
        gem.transform.parent = this.transform;
        gem.name = "Gem - " + pos.x + ", " + pos.y; // names the gem with the x y
        allGems[pos.x, pos.y] = gem; // stores the gems coordinates

        gem.SetupGem(pos, this);
    }

    private bool MatchesAt(Vector2Int posToCheck, Gem gemToCheck)
    {
        if(posToCheck.x > 1)
        {
            //checks to the left
            if(allGems[posToCheck.x - 1, posToCheck.y].type == gemToCheck.type &&
               allGems[posToCheck.x - 2, posToCheck.y].type == gemToCheck.type)
            {
                return true;
            }
        }

        if (posToCheck.y > 1)
        {
            //checks to the above
            if (allGems[posToCheck.x, posToCheck.y - 1].type == gemToCheck.type &&
                allGems[posToCheck.x, posToCheck.y - 2].type == gemToCheck.type)
            {
                return true;
            }
        }

        return false;
    }



    private void SetupCamera()
    {
        Camera.main.transform.position = new Vector3((float)(width - 1) / 2f, (float)(height - 1) / 2f, -10f);

        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float verticalSize = (float)height / 2f + (float)borderSize;
        float horizontalSize = ((float)width / 2f + (float)borderSize) / aspectRatio;

        Camera.main.orthographicSize = (verticalSize > horizontalSize) ? verticalSize : horizontalSize;
    }


}
