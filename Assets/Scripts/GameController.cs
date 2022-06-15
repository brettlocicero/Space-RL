using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public Deck deck;
    public Player player;
    
    [Header("")]
    [SerializeField] float handWidth = 600f;
    [SerializeField] float handBaseHeight = 50f;
    [SerializeField] Transform handUI;
    [HideInInspector] public bool cardInHand;

    [Header("")]
    [SerializeField] int maxEnergy = 5;
    public int energy;
    [SerializeField] Text energyText;
    [SerializeField] Button endTurnButton;
    [SerializeField] RectTransform proceedButton;
    [SerializeField] Animator screenTrans;
    [SerializeField] GameObject combatScreenUI;
    [SerializeField] Image energyFillUI;

    [Header("")]
    public List<GameObject> enemies;
    public enum Turn { PlayerTurn, EnemyTurn };
    public Turn turn;
    [SerializeField] Animator playerTurn;
    [SerializeField] Animator enemyTurn;
    public int enemiesKilled;

    [Header("")]
    [SerializeField] Room currentRoom;
    [SerializeField] Room[] possibleRooms;

    bool transitioning;

    void Awake () => instance = this;

    void Start ()
    {
        StartCombat();
    }

    public void StartCombat () 
    {
        enemies.Clear();
        enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();

        combatScreenUI.SetActive(enemies.Count > 0);
        transitioning = false;
        proceedButton.GetComponent<Animator>().Rebind();
        proceedButton.GetComponent<Animator>().Update(0f);
        proceedButton.localPosition = new Vector3(116.1f, -103.46f, 0f);

        if (enemies.Count < 1) 
        {
            CombatOver();
            return;
        }
        
        // things needed at only of beginning of combat
        enemiesKilled = 0;
        deck.EmptyHand();
        deck.ShuffleDeck();

        energy = 0;
        AddEnergy(maxEnergy);
        InitHand();

    }

    void InitHand () 
    {
        deck.DrawHand();
        UpdateHandVisuals();
    }

    public void UpdateHandVisuals () 
    {
        int childCount = handUI.childCount;
        handWidth = 33.333f * (childCount - 1);
        for (int i = 0; i < childCount; i++) 
        {
            CardDisplay child = handUI.GetChild(i).GetComponent<CardDisplay>();

            float offsetX;
            if (i > 0)
                offsetX = Mathf.Lerp(-handWidth, handWidth, (float)i / (float)(childCount - 1));            
            else
                offsetX = -handWidth;

            Vector3 assignedPos = new Vector3(offsetX, handBaseHeight, 0f);
            child.AssignPosition(assignedPos);
        }
    }

    public void AddEnergy (int amt = 0) 
    {
        energy += amt;
        energyText.text = energy + "/" + maxEnergy;
        energyFillUI.transform.localScale = new Vector3(1f, (float)energy / (float)maxEnergy, 1f);
    }

    void UpdateTurn (Turn reqTurn) 
    {
        switch (reqTurn) 
        {
            case Turn.PlayerTurn:
                turn = reqTurn;
                endTurnButton.interactable = true;
                break;
            case Turn.EnemyTurn:
                turn = reqTurn;
                endTurnButton.interactable = false;
                break;
        }
    }

    public void StartPlayerTurn () 
    {
        if (turn == Turn.PlayerTurn) return;
        UpdateTurn(Turn.PlayerTurn);

        // Decide all enemies attack
        foreach (GameObject en in enemies) 
        {
            en.GetComponent<Enemy>().DecideNextAction();
        }

        deck.DrawHand();
        energy = maxEnergy;
        playerTurn.SetTrigger("Trigger Visual");
        AddEnergy();
    }

    public void StartEnemyTurn () 
    {
        if (turn == Turn.EnemyTurn || CheckIfCombatOver()) return;
        UpdateTurn(Turn.EnemyTurn);
        StartCoroutine(EnemyTurn());
    }

    public void RemoveEnemyFromList (GameObject enemy) => enemies.Remove(enemy);

    IEnumerator EnemyTurn () 
    {
        // play super initial sound/anims
        yield return new WaitForSeconds(1f);
        // play enemy turn anims here
        enemyTurn.SetTrigger("Trigger Visual");
        yield return new WaitForSeconds(2f);

        int enemyCount = enemies.Count;
        float maxDelay = 1f * (enemyCount - enemiesKilled);
        for (int i = 0; i < enemyCount; i++) 
        {
            float delay = 1.5f * (i - enemiesKilled);
            enemies[i].SendMessage("Attack", delay, SendMessageOptions.DontRequireReceiver);
        }

        yield return new WaitForSeconds(maxDelay + 2f);
        StartPlayerTurn();
    }

    public void CheckIfTurnOver () 
    {
        foreach (Transform cardTransform in deck.hand) 
        {
            Card c = cardTransform.GetComponent<CardDisplay>().card;
            if (c.cost <= energy) return;
        }

        StartEnemyTurn();
    }

    public void UpdateEnemyCount () 
    {
        enemiesKilled++;

        if (CheckIfCombatOver()) CombatOver();
    }

    bool CheckIfCombatOver () 
    {
        return enemiesKilled >= enemies.Count;
    }

    void CombatOver () 
    {
        print("Combat over");
        proceedButton.GetComponent<Animator>().SetTrigger("Display");
    }

    public void ResetGame () 
    {
        if (!CheckIfCombatOver() || transitioning) return;
        
        transitioning = true;
        screenTrans.SetTrigger("Transition");
        StartCoroutine(ResetGameAnimations());
    }

    IEnumerator ResetGameAnimations () 
    {
        yield return new WaitForSeconds(1f);

        Room selRoom = possibleRooms[Random.Range(0, possibleRooms.Length)];
        Destroy(currentRoom.gameObject);
        var r = Instantiate(selRoom.gameObject, selRoom.transform.position, Quaternion.identity);
        currentRoom = r.GetComponent<Room>();
        player.transform.position = currentRoom.playerSpawnPos.position;

        yield return new WaitForSeconds(1f);
        StartCombat();
    }

    void Update () 
    {
        if (Input.GetKeyDown(KeyCode.U)) UpdateHandVisuals();
        if (Input.GetKeyDown(KeyCode.D) && deck.deck.Count > 0) deck.DrawCard();
        if (Input.GetKeyDown(KeyCode.R)) deck.ShuffleDeck();
    }
}
