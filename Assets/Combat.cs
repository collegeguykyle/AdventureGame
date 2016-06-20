using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Vectrosity;

public class Combat : MonoBehaviour {

    private static int playerRoundActions;
    private static int playerActionsRemain;
    private static int enemyRoundActions;
    private static int enemyActionsRemain;

    public static Turn[] Round;
    
    public string whoTurn;
    public enum WhoTurn { player, enemy, neutral };

    public float turnUIspacing = .1f;
    public float turnUIfromtop = 1.0f;
    public float turnUIwidth = .5f;

    public static UnityEvent NewRound = new UnityEvent();
    public static UnityEvent NewRoundReady = new UnityEvent();

    void Start () {
        
	}

    public void startNewRound()  //so can be called by unity UI
    {
        StartCoroutine(newRound());
    }

    public IEnumerator newRound()
    {
        Debug.Log("START NEW ROUND CO ROUTINE");
        playerRoundActions = 0;
        enemyRoundActions = 0;

        //GameObject newRoundText = GameObject.Find("New Round Text");
        //Animator animator = newRoundText.GetComponent<Animator>();
        //animator.Play("Round_Text");

        NewRound.Invoke();  //send new round message to units
        setRoundTurnOrder();
        setRoundUI();
        yield return new WaitForSeconds(1);
        Debug.Log("END NEW ROUND CO ROUTINE");
        NewRoundReady.Invoke();
    }

    public static void reportActions (Unit.Team team, int acts)
    {
        if (team == Unit.Team.player) playerRoundActions += acts;
        else if (team == Unit.Team.enemy) enemyRoundActions += acts;
    }

    public void setRoundTurnOrder()  //PUBLIC FOR TESTING
    {
        string firstTurn;
        string secondTurn;
        var turnsSupported = new int();
        playerActionsRemain = playerRoundActions;
        enemyActionsRemain = enemyRoundActions;
        GameObject[] oldCircles = GameObject.FindGameObjectsWithTag("TurnCircle");
        foreach (GameObject circ in oldCircles) GameObject.Destroy(circ);

        if (playerRoundActions >= enemyRoundActions)
        {
            firstTurn = "Player";
            secondTurn = "Enemy";
            turnsSupported = Mathf.FloorToInt(enemyRoundActions / 2);
        }
        else
        {
            firstTurn = "Enemy";
            secondTurn = "Player";
            turnsSupported = Mathf.FloorToInt(playerRoundActions / 2);
        }

        if (turnsSupported > 4) turnsSupported = 4;
        int totalTurns = (turnsSupported * 2) + 1;
        Round = new Turn[totalTurns];
        string who = firstTurn;
        for (int i = 0; i < totalTurns; i++)
        {
            Round[i] = new Turn();
            Round[i].Team = who;
            if (who.Equals("Player"))
            {
                if (playerActionsRemain > 1)
                {
                    playerActionsRemain -= 2;
                    Round[i].Actions = 2;
                }
                else
                {
                    playerActionsRemain--;
                    Round[i].Actions = 1;
                }

            }
            else
            {
                if (enemyActionsRemain > 1)
                {
                    enemyActionsRemain -= 2;
                    Round[i].Actions = 2;
                }
                else
                {
                    enemyActionsRemain--;
                    Round[i].Actions = 1;
                }

            }
            if (who.Equals(firstTurn)) who = secondTurn;
            else who = firstTurn;
        }

        //add in left over actions to the turns
        int[] firstOrder = new int[5];
        int[] secondOrder = new int[4];
        bool firstTurnEven = true; //Does whoever goes first have an even or odd number of turns?
        if (turnsSupported % 2 == 0) firstTurnEven = false;

        var startPoint = Mathf.FloorToInt(totalTurns / 2);

        if (firstTurnEven)
        {
            firstOrder[0] = startPoint - 1;
            firstOrder[1] = startPoint + 1;
            firstOrder[2] = startPoint - 3;
            firstOrder[3] = startPoint + 3;

            secondOrder[0] = startPoint;
            secondOrder[1] = startPoint - 2;
            secondOrder[2] = startPoint + 2;
        }
        else
        {
            firstOrder[0] = startPoint;
            firstOrder[1] = startPoint - 2;
            firstOrder[2] = startPoint + 2;
            firstOrder[3] = startPoint - 4;
            firstOrder[4] = startPoint + 4;

            secondOrder[0] = startPoint - 1;
            secondOrder[1] = startPoint + 1;
            secondOrder[2] = startPoint - 3;
            secondOrder[3] = startPoint + 3;
        }

        if (firstTurn.Equals("Player"))
        {
            int i = 0;
            while (playerActionsRemain > 0)
            {
                if (i > turnsSupported) i = 0;
                Round[firstOrder[i]].Actions++;
                playerActionsRemain--;
                i++;
            }
            i = 0;
            while (enemyActionsRemain > 0)
            {
                if (i == turnsSupported) i = 0;
                Round[secondOrder[i]].Actions++;
                enemyActionsRemain--;
                i++;
            }
        }
        else
        {
            int i = 0;
            while (enemyActionsRemain > 0)
            {
                if (i > turnsSupported) i = 0;
                Round[firstOrder[i]].Actions++;
                enemyActionsRemain--;
                i++;
            }
            i = 0;
            while (playerActionsRemain > 0)
            {
                if (i == turnsSupported) i = 0;
                Round[secondOrder[i]].Actions++;
                playerActionsRemain--;
                i++;
            }
        }
    }

    public void setRoundUI()
    {
        if (!GameObject.Find("Turn_Circles")) new GameObject("Turn_Circles");
        else Debug.Log("Turn_Circles object found");
        Transform circleGroup = GameObject.Find("Turn_Circles").transform;
        int actionNum = 0;
        float offset = ((playerRoundActions + enemyRoundActions) / 2) * turnUIwidth - turnUIspacing;
        for (int i = 0; i < Round.Length; i++)
        {
            for (int j = 0; j < Round[i].Actions; j++)
            {
                GameObject circle;
                Vector2 newlocal;
                if (Round[i].Team.Equals("Player")) circle = Canvas.Instantiate(Resources.Load("Prefabs/Player Action")) as GameObject;
                else circle = Canvas.Instantiate(Resources.Load("Prefabs/Enemy Action")) as GameObject;

                newlocal = circleGroup.transform.position;
                newlocal.x += actionNum * turnUIwidth + turnUIspacing - offset;
                circle.transform.position = newlocal;
                circle.transform.SetParent(circleGroup, true);
                actionNum++;
            }
        }
    }


}

public class Turn
{
    public string Team { get; set; }
    public int Actions { get; set; }

}
