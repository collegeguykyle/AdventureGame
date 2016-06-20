using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vectrosity;
using UnityEngine.Events;

public class BattleController : MonoBehaviour{

    public int TurnNumber = 0;
    Unit.Team WhoTurn = new Unit.Team();

    private static GameObject whoSelect;
    private static float lowDist;
    public static GameObject Selected;

    public static UnityEvent ReportPosition = new UnityEvent();

    void awake ()
    {
        Combat.NewRoundReady.AddListener(newRound);
    }

    void startTurn ()
    {
        TurnNumber++;
        WhoTurn = whoTurn(TurnNumber);

        if (WhoTurn == Unit.Team.player)
        {
            //give player control.  ALready done with WhoTurn?
            SelectNearest();
            //move camera if unit not on screen
        }
        else if (WhoTurn == Unit.Team.enemy)
        {
            //start AI processing for what unit to select and do action
            //camera follows AI movements / actions
        }
        else if (WhoTurn == Unit.Team.neutral)
        {
            //do nothing for now?
        }
    }

    void newRound()
    {
        TurnNumber = 0;
        WhoTurn = whoTurn(TurnNumber);
    }

    Unit.Team whoTurn (int turn)
    {
        if (Combat.Round[turn].Team.Equals("Player"))
            return Unit.Team.player;
        else if (Combat.Round[turn].Team.Equals("Enemy"))
            return Unit.Team.enemy;
        else return Unit.Team.neutral;
    }

    public void selectUnit(GameObject who)
    {
        //disable nav collider
        //draw select circle around unit
    }

    IEnumerator SelectNearest()
    {
        lowDist = Mathf.Infinity;
        whoSelect = null;
        ReportPosition.Invoke();
        yield return new WaitForSeconds(.5f);
        if (whoSelect == null)
        {
            //no friendly units found with available actions some how
            Debug.Log("Passing Turn due to no player units found that can act");
            endTurn();
        }
        else selectUnit(whoSelect);
    }

    public static void PositionList(GameObject who, int acts, Unit.Team t)
    {
        float dist = Vector2.Distance(who.transform.position, Camera.main.transform.position);
        if (t == Unit.Team.player && acts > 0 && dist < lowDist) whoSelect = who;
    }

    void endTurn()
    {
        //take action from selected unit
        //plus up turn number
        //check for actions completed charging to execute
        //check for continued melees
        //check for room events or cut scenes to trigger
        //set next turn in motion
    }

}


