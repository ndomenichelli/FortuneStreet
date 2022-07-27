using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToken : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        stateManager = GameObject.FindObjectOfType<StateManager>();
        //playerManager = transform.GetComponentInChildren<PlayerManager>();

        spacesDisplay = GameObject.FindObjectOfType<SpaceToMoveDisplay>();
        scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay>();

        targetPostion = this.transform.position;
    }

    public Space StartingSpace;
    public Space currentSpace;

    public int playerID;

    StateManager stateManager;
    public PlayerManager playerManager;
    SpaceToMoveDisplay spacesDisplay;
    ScoreDisplay scoreDisplay;


    public Space[] moveQueue;
    public int moveQueueIndex;

    bool isAnimating = false;

    Vector3 targetPostion;
    Vector3 velocity = Vector3.zero;
    float smoothTime = 0.05f;
    float smoothTimeVertical = 0.1f;
    float smoothDistance = 0.1f;
    float smoothHeight = .5f;

    PlayerToken lastTokenHere;

    int spacesLeft;

    //public int cash;
    //public int netWorth;

    // Update is called once per frame
    void Update()
    {
        if (stateManager.isDoneRolling)
        {
            StartMovement();
        }
        if (!isAnimating)
        {
            //nothing for us to do
            return;
        }
        if (Vector3.Distance(
           new Vector3(this.transform.position.x, targetPostion.y, this.transform.position.z),
           targetPostion) < smoothDistance)
        {
            //we reached the target and too high up, is there still moves in the queue
            if(
                ((this.transform.position.y - smoothDistance) > targetPostion.y)
            )
            {
                //we are out out of moves, drop down
                this.transform.position = Vector3.SmoothDamp(
                    this.transform.position,
                    new Vector3(this.transform.position.x, targetPostion.y, this.transform.position.z),
                    ref velocity,
                    smoothTime);
            }
            else
            {
                //right pos, right height
                AdvanceMoveQueue();
            }
        }

        //rise up before sideways
        else if (this.transform.position.y < (smoothHeight - smoothDistance))
        {
            this.transform.position = Vector3.SmoothDamp(
                this.transform.position,
                new Vector3(this.transform.position.x, smoothHeight, this.transform.position.z),
                ref velocity,
                smoothTimeVertical);
        }
        else
        {
            this.transform.position = Vector3.SmoothDamp(
               this.transform.position,
               new Vector3(targetPostion.x, smoothHeight, targetPostion.z),
               ref velocity,
               smoothTime);
        }
    }
    public bool lastMoveForward = false;
    //bool beginningCase = true;
    public void AdvanceMoveQueue()
    {
        //Debug.Log("Adv move q");
        // passing by spaces, still moving
        // we have reached our last pos, is there another move in queue?
        if (moveQueue != null && moveQueueIndex < moveQueue.Length)
        {
            Space nextSpace = moveQueue[moveQueueIndex];
            if(nextSpace == null)
            {
                // no next space (should never happen)
                //Debug.Log("no next space");
                SetNewTargetPosition(this.transform.position + Vector3.right * 10f);
            }
            else
            {
                // TODO: bug: have to press w or s an extra time when switching directions
                if (Input.GetKeyDown(KeyCode.W))
                {
                    moveFoward();
                    currentSpace = moveQueue[moveQueueIndex-1];
                }
                else if (Input.GetKeyDown(KeyCode.S) && (spacesLeft < stateManager.DiceTotal) && moveQueueIndex > 0)
                {
                    moveBack();
                    currentSpace = moveQueue[moveQueueIndex];
                }

                spacesDisplay.setDisplay(spacesLeft);
            }
            // if passing over a suit
            if (currentSpace.name.Contains("Suit"))
            {
                StartCoroutine(suitUpdate());
            }
        }
        else
        {
            // landing on space

            //the movement queue is empty so, we are done animation
            this.isAnimating = false;
            stateManager.isDoneAnimating = true;

            //after done moving, put player in current space
            currentSpace.playerTokensHere[playerID] = this;

            //Debug.Log(currentSpace.playerCount());
            //we are above our target, check for fix positions

            //Debug.Log("current on space " + currentSpace.getPlayerCount());

            // landing on space, move over others if otheres there
            if (currentSpace.getPlayerCount() > 1)
            {
                StartCoroutine(fixOverlap(getPlayerDirection()));
            }
        }
    }

    public void moveFoward()
    {
        lastMoveForward = true;

        SetNewTargetPosition(moveQueue[moveQueueIndex].transform.position);

        if (!lastMoveForward)
        {
            moveQueueIndex = moveQueueIndex + 2;
        }
        else
        {
            moveQueueIndex++;
            //Debug.Log("move 1");
        }
        spacesLeft--;
    }
    public void moveBack()
    {
        lastMoveForward = false;

        if (lastMoveForward)
        {
            moveQueueIndex = moveQueueIndex - 2;
        }
        else
        {
            Debug.Log("move back");
            moveQueueIndex--;
        }
        spacesLeft++;

        Debug.Log("position: " + moveQueue[moveQueueIndex].transform.position);
        SetNewTargetPosition(moveQueue[moveQueueIndex].transform.position);
    }
    public void SetNewTargetPosition(Vector3 pos)
    {
        targetPostion = pos;
        velocity = Vector3.zero;
    }
    public void StartMovement()
    {
        //Debug.Log("Start Movement");
        //is this the correct player?
        if(stateManager.CurrentPlayerID != playerID)
        {
            return;
        }
        //Debug.Log("Click");
        if (!stateManager.isDoneRolling)
        {
            return;
        }
        if (stateManager.isDoneClicking)
        {
            //we already moved
            return;
        }

        int spacesToMove = stateManager.DiceTotal;

        spacesLeft = spacesToMove;

        spacesDisplay.setDisplay(spacesToMove);

        //where should we end up
        if (spacesToMove == 0)
        {
            Debug.Log("Rolled a zero somehow");
            return;
        }

        moveQueue = GetSpacesAhead(spacesToMove);
        Space finalSpace = moveQueue[moveQueue.Length - 1];

        //Debug.Log("current players on space: " + currentSpace.getPlayerCount());
        if (currentSpace.getPlayerCount() > 0)
        {
            //Debug.Log("Move guy back");

            //StartCoroutine(fixOverlap(Vector3.left));
        }

        if (finalSpace == null)
        {
            //scoring (ignore)
        }
        else
        {
            if (!CanLegallyMoveTo(finalSpace))
            {
                finalSpace = currentSpace;
                moveQueue = null;
                return;
            }
            // if there is another player token in a legal space / if landing on a space with another token
            if (finalSpace.getPlayerCount() > 0)
            {
                //fix current?
            }
        }
        
        // remove ourself from old space
        if(currentSpace != null)
        {
            currentSpace.playerTokensHere[playerID] = null;
        }
     
        moveQueueIndex = 0;
        stateManager.isDoneClicking = true;
        this.isAnimating = true;
    }
    // return the list of tiles _ moves ahead of us
    Space[] GetSpacesAhead(int spacesToMove)
    {
        //where should we end up
        if (spacesToMove == 0)
        {
            Debug.Log("Rolled a zero somehow");
            return null;
        }

        Space[] listOfSpaces = new Space[spacesToMove];
        Space finalSpace = currentSpace;

        for (int i = 0; i < spacesToMove; i++)
        {
            if (finalSpace == null)
            {
                //Debug.Log("beginning case");
                finalSpace = StartingSpace;
                //spaceToMove++;
            }
            else
            {
                if(finalSpace.NextSpaces == null || finalSpace.NextSpaces.Length == 0)
                {
                    finalSpace = null;
                }
                else if (finalSpace.NextSpaces.Length > 1)
                {
                    //branching paths

                    // continue as normal
                    finalSpace = finalSpace.NextSpaces[0];
                }
                else
                {
                    finalSpace = finalSpace.NextSpaces[0];
                }
            }

            listOfSpaces[i] = finalSpace;
        }

        return listOfSpaces;
    }
    // return final space wed land on if we moved _ spaces
    Space GetSpaceAhead(int spacesToMove)
    {
        Space[] spaces = GetSpacesAhead(spacesToMove);

        if(spaces == null)
        {
            return currentSpace;
        }
        return spaces[spaces.Length-1];
    }
    public bool CanLegallyMoveAhead(int spacesToMove)
    {
        Space space = GetSpaceAhead(spacesToMove);
        return CanLegallyMoveTo(space);
    }
    bool CanLegallyMoveTo(Space destinationSpace)
    {
        if(destinationSpace == null)
        {
            Debug.Log("trying to move off the board ???");
            return true;
        }
        //is the space empty?
        if(destinationSpace.playerTokensHere == null)
        {
            return true;
        }
        //if (destinationSpace.playerTokensHere[playerID].playerID == this.playerID)
        //{
        //    //this should never happen (we have our own token on the space we are moving to)
        //    return true;
        //}
        //if (destinationSpace.playerTokensHere[playerID].playerID != this.playerID)
        //{
        //    //landed on a space that another player is also on
        //    return true;
        //}

        return true;
    }

    IEnumerator fixOverlap(Vector3 direction)
    {
        float moveDistance = 50f;

        int playersHere = currentSpace.getPlayerCount();

        PlayerToken justArrived = currentSpace.playerTokensHere[playerID];
        //Debug.Log("Just arrived: " + justArrived);

        int justArrivedID = justArrived.playerID;

        for (int i = 0; i < currentSpace.playerTokensHere.Length; i++)
        {
            if (currentSpace.playerTokensHere[i] != null)
            {
                currentSpace.playerTokensHere[i].transform.Translate(direction * (moveDistance * Time.deltaTime));
            }
        }
        currentSpace.playerTokensHere[justArrivedID].transform.Translate(-direction * (moveDistance * Time.deltaTime));

        yield return new WaitForSeconds(1);
    }
    IEnumerator suitUpdate()
    {
        Suit thisSuit = currentSpace.GetComponent<Suit>();

        this.playerManager.suitsOwned[(int)thisSuit.suit] = true;

        // update display
        scoreDisplay.updateSuits(this, thisSuit.suit);

        yield return new WaitForSeconds(1);
    }
    public Vector3 getPlayerDirection()
    {
        int spaceNumber = 0;

        // get number from current space string name
        if (currentSpace.name.Contains("Space"))
        {
            spaceNumber = int.Parse(currentSpace.name.Substring(6).Replace("(", "").Replace(")", ""));
        }
        else if (currentSpace.name.Contains("Shop") || currentSpace.name.Contains("Bank") || currentSpace.name.Contains("Warp"))
        {
            spaceNumber = int.Parse(currentSpace.name.Replace("(", "").Replace(")", "").Substring(5, 1));
        }

        //Debug.Log("space id: " + spaceNumber);

        if (spaceNumber > 0 && spaceNumber < 10)
        {
            return Vector3.right;
        }
        else if (spaceNumber >= 10 && spaceNumber < 20)
        {
            return Vector3.back;
        }
        else if (spaceNumber >= 20 && spaceNumber < 30)
        {
            return Vector3.left;
        }
        else if (spaceNumber >= 30 || spaceNumber == 0)
        {
            return Vector3.forward;
        }
        else
        {
            Debug.Log("how");
            return Vector3.up;
        }
    }

}
