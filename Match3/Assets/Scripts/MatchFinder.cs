using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MatchFinder : MonoBehaviour
{
    private Board board;

    public List<Gem> currentMatches = new List<Gem>();



    private void Awake()
    {
        board = FindObjectOfType<Board>();
    }


    public void FindAllMatches()
    {
        for (int x = 0; x < board.width; x++)
        {
            for (int y = 0; y < board.height; y++)
            {
                Gem currentGem = board.allGems[x, y];
                if (currentGem != null)
                {
                    if(x > 0 && x < board.width - 1)
                    {
                        //finding the matches to the left and right
                        Gem leftGem = board.allGems[x - 1, y];
                        Gem rightGem = board.allGems[x + 1, y];
                        if(leftGem != null && rightGem != null)
                        {
                            if (leftGem.type == currentGem.type && rightGem.type == currentGem.type)
                            {
                                currentGem.isMatched = true;
                                leftGem.isMatched = true;
                                rightGem.isMatched = true;

                                currentMatches.Add(currentGem);
                                currentMatches.Add(leftGem);
                                currentMatches.Add(rightGem);
                            }
                        }
                    }
                    if (y > 0 && y < board.height - 1)
                    {
                        //finding all the matches above and below
                        Gem aboveGem = board.allGems[x, y + 1];
                        Gem belowGem = board.allGems[x, y - 1];
                        if (aboveGem != null && belowGem != null)
                        {
                            if (aboveGem.type == currentGem.type && belowGem.type == currentGem.type)
                            {
                                currentGem.isMatched = true;
                                aboveGem.isMatched = true;
                                belowGem.isMatched = true;

                                currentMatches.Add(currentGem);
                                currentMatches.Add(aboveGem);
                                currentMatches.Add(belowGem);
                            }
                        }
                    }
                }
            }
        }

        if(currentMatches.Count > 0)
        {
            currentMatches = currentMatches.Distinct().ToList(); // make it a consistant list with no duplicates
        }

    }




}