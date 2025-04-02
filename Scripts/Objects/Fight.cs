using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Fight
{
    public enum attackType
    {
        normal,
        powerAttack
    }

    private Inhabitant attacker;
    private Inhabitant defender;

    private Monster theMonster;

    private bool fightOver = false;

    public Fight(Monster m)
    {
        this.theMonster = m;

        //initially determine who goes first
        int roll = Random.Range(0, 20) + 1;
        if (roll <= 10)
        {
            Debug.Log("Monster goes first");
            this.attacker = m;
            this.defender = Core.thePlayer;
        }
        else
        {
            Debug.Log("Player goes first");
            this.attacker = Core.thePlayer;
            this.defender = m;
        }

    }

    public bool isFightOver()
    {
        return this.fightOver;
    }

    public void takeASwing(GameObject playerGameObject, GameObject monsterGameObject, attackType type)
    {
        // Adjusted attack roll for power attack
        float attackRollModifier = 1.0f;

        if (type == attackType.powerAttack)
        {
            Debug.Log("Power attack");
            attackRollModifier = 0.75f; // 25% penalty to the attack roll
        }
        else
        {
            Debug.Log("Normal attack");
        }

        // Roll attack and apply modifier for power attack
        int attackRoll = Mathf.FloorToInt(Random.Range(0, 20) + 1 * attackRollModifier);
        Debug.Log("Attack Roll: " + attackRoll);
        Debug.Log("Defender AC: " + this.defender.getAC());

        if (attackRoll >= this.defender.getAC())
        {
            // Attack hits
            int baseDamage = Random.Range(1, 6); // Normal damage range: 1-5
            int damage = type == attackType.powerAttack ? Mathf.FloorToInt(baseDamage * 1.5f) : baseDamage; // Power attack hits 50% harder

            this.defender.takeDamage(damage);

            Debug.Log(this.attacker.getName() + " dealt " + damage + " damage");

            if (this.defender.isDead())
            {
                this.fightOver = true;
                Debug.Log(this.attacker.getName() + " killed " + this.defender.getName());
                if (this.defender is Player)
                {
                    // Player died
                    Debug.Log("Player died");
                    playerGameObject.SetActive(false); // Hide the player
                }
                else
                {
                    // Monster died
                    Debug.Log("Monster died");
                    GameObject.Destroy(monsterGameObject); // Remove the monster from the scene
                }
            }
        }
        else
        {
            Debug.Log(this.attacker.getName() + " missed " + this.defender.getName());
        }

        // Swap attacker and defender for the next turn
        Inhabitant temp = this.attacker;
        this.attacker = this.defender;
        this.defender = temp;
    }

    public void drinkPotion()
    {
        if (attacker is Player)
        {
            int maxHp = this.attacker.getMaxHp();
            int currentHp = this.attacker.getCurrHp();

            int healAmount = Mathf.CeilToInt(maxHp * 0.25f);
            int newHp = currentHp + healAmount;

            attacker.setCurrentHp(newHp);

            Debug.Log(attacker.getName() + " drank a potion and healed " + healAmount + " hp");
        }
        else
        {
            Debug.Log("Only players can drink potions");
        }
    }


        public void startFight(GameObject playerGameObject, GameObject monsterGameObject)
    {
        //should have the attacker and defender fight each until one of them dies.
        //the attacker and defender should alternate between each fight round and
        //the one who goes first was determined in the constructor.
        while (true)
        {
            int attackRoll = Random.Range(0, 20) + 1;
            if (attackRoll >= this.defender.getAC())
            {
                //attacker hits the defender
                int damage = Random.Range(1, 6); //1 to 5 damage
                this.defender.takeDamage(damage);

                if (this.defender.isDead())
                {
                    Debug.Log(this.attacker.getName() + " killed " + this.defender.getName());
                    if (this.defender is Player)
                    {
                        //player died
                        Debug.Log("Player died");
                        //end the game
                        playerGameObject.SetActive(false); //hide the player
                    }
                    else
                    {
                        //monster died
                        Debug.Log("Monster died");
                        //remove the monster from the scene
                        GameObject.Destroy(monsterGameObject); //remove the monster from the scene
                    }
                    break; //fight is over
                }
            }
            else
            {
                Debug.Log(this.attacker.getName() + " missed " + this.defender.getName());
            }
            Inhabitant temp = this.attacker;
            this.attacker = this.defender;
            this.defender = temp;
        }
    }
}
