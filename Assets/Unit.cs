using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Vectrosity;
using System.Collections.Generic;


public class Unit : MonoBehaviour {

    public int actions = 2;
	public int maxActions = 2;
    public float maxMoveDist = 10;
    public enum Team { player, enemy, neutral };
    public Team team = Team.neutral;

    void Start()
    {
        Combat.NewRound.AddListener(sendActions);
        BattleController.ReportPosition.AddListener(reportPosition);
        
    }

    void sendActions()
    {
        //if unit is still alive send maxActions to game control to be used in Round.
        Combat.reportActions(team, maxActions);
    }

    void reportPosition()
    {
        BattleController.PositionList(gameObject, actions, team);
    }


}
