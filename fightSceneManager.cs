using System;
using UnityEngine;

public class fightSceneManager : MonoBehaviour
{
    public GameObject player;
    public GameObject monster;

    private float timeSinceLastTimeDeltaTime = 0.0f;

    private Fight theFight;

    private Vector3 playerStartPos;
    private Vector3 monsterStartPos;
    private Vector3 attackMove = new Vector3(1, 0, 0);

    private bool isPlayerTurn = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.playerStartPos = this.player.transform.position;
        this.monsterStartPos = this.monster.transform.position;

        print("Player Max HP: " + Core.thePlayer.getMaxHp());
        print("Monster Max HP: " + Core.theMonster.getMaxHp());

        this.theFight = new Fight(Core.theMonster);
        print("Player AC: " + Core.thePlayer.getAC());
        print("Monster AC: " + Core.theMonster.getAC());
    }

    // Update is called once per frame
    void Update()
    {
        this.timeSinceLastTimeDeltaTime += Time.deltaTime;

        // Move the combatants (or take actions) every 0.5 seconds
        if (this.timeSinceLastTimeDeltaTime >= 0.5f)
        {
            if (!this.theFight.isFightOver())
            {
                // Player's turn to act
                if (isPlayerTurn)
                {
                    // Player can drink potion (only during their turn)
                    if (Input.GetKeyDown(KeyCode.H)) // Player drinks potion
                    {
                        this.theFight.drinkPotion();
                        // After drinking the potion, end the player's turn
                        isPlayerTurn = false;
                    }
                    else
                    {
                        this.theFight.takeASwing(this.player, this.monster, Fight.attackType.normal); // Normal attack
                        // End the player's turn after the swing
                        isPlayerTurn = false;
                    }
                }
                else
                {
                    // Monster's turn to act
                    this.theFight.takeASwing(this.player, this.monster, Fight.attackType.normal);
                    // End the monster's turn
                    isPlayerTurn = true; // Switch back to player's turn
                }
            }
            else
            {
                Debug.Log("Fight is over");
            }

            this.timeSinceLastTimeDeltaTime = 0.0f;
        }
    }
}
