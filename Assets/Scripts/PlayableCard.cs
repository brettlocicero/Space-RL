using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCard : MonoBehaviour
{
    GameController gc;
    CardDisplay cd;
    Collider2D hit;

    void Start () 
    {
        gc = GameController.instance;
        cd = GetComponent<CardDisplay>();
    }

    public void DraggingCard () 
    {
        hit = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (!hit || (int)gc.turn != 0 || gc.energy < cd.card.cost) return;

        if (hit.CompareTag("Enemy") && cd.card.targetEnemies && !cd.card.AOE)
        {
            hit.GetComponent<Enemy>().DraggingOver();
        }

        else if (hit.CompareTag("Friendly") && cd.card.targetFriendlies && !cd.card.AOE)
        {
            hit.GetComponent<Player>().DraggingOver();
        }

        else if (hit.CompareTag("Enemy") && cd.card.targetEnemies && cd.card.AOE)
        {
            foreach (GameObject e in gc.enemies) 
            {
                if (e) e.GetComponent<Enemy>().DraggingOver();
            }
        }
    }

    public void PlayCard ()
    {
        // 0 is index of gc.Turn.PlayerTurn
        if (!hit || (int)gc.turn != 0 || gc.energy < cd.card.cost) return;

        bool playedCard = false;

        if (hit.CompareTag("Enemy") && cd.card.targetEnemies)
        {
            print("Played " + cd.card.cardName + " on " + hit.gameObject.name);

            if (cd.card.AOE) 
            {
                foreach (GameObject e in gc.enemies) 
                    if (e) e.GetComponent<Enemy>().TakeDamage(cd.card.damage);

            
                if (cd.card.useMeanPos) 
                {
                    Vector3 meanPos = GetMeanVector(gc.enemies);
                    var obj = Instantiate(cd.card.gameFX, meanPos, cd.card.gameFX.transform.rotation); 
                    Destroy(obj.gameObject, cd.card.animLength);
                }

                else 
                {
                    var obj = Instantiate(cd.card.gameFX, Vector3.zero, cd.card.gameFX.transform.rotation); 
                    Destroy(obj.gameObject, cd.card.animLength);
                }

                if (cd.card.stun) 
                {
                    foreach (GameObject e in gc.enemies) 
                        if (e) e.GetComponent<Enemy>().Stun();
                }
            }

            else 
            {
                hit.GetComponent<Enemy>().TakeDamage(cd.card.damage);
                Vector3 rot = new Vector3(0f, 0f, Random.Range(cd.card.fxZRotationRange.x, cd.card.fxZRotationRange.y));
                var obj = Instantiate(cd.card.gameFX, hit.transform.position, Quaternion.Euler(rot)); 
                Destroy(obj.gameObject, cd.card.animLength);

                if (cd.card.stun) 
                {
                    hit.GetComponent<Enemy>().Stun();
                }
            }

            playedCard = true;
        }

        else if (hit.CompareTag("Friendly") && cd.card.targetFriendlies)
        {
            print("Played " + cd.card.cardName + " on " + hit.gameObject.name);

            if (cd.card.AOE) 
            {
                foreach (GameObject e in gc.enemies) 
                    if (e) e.GetComponent<Player>().TakeDamage(cd.card.damage);

                if (cd.card.useMeanPos) 
                {
                    Vector3 meanPos = GetMeanVector(gc.enemies);
                    var obj = Instantiate(cd.card.gameFX, meanPos, cd.card.gameFX.transform.rotation); 
                    Destroy(obj.gameObject, cd.card.animLength);
                }

                else 
                {
                    var obj = Instantiate(cd.card.gameFX, Vector3.zero, cd.card.gameFX.transform.rotation); 
                    Destroy(obj.gameObject, cd.card.animLength);
                }
            }

            else 
            {
                hit.GetComponent<Player>().TakeDamage(cd.card.damage);
                Vector3 rot = new Vector3(0f, 0f, Random.Range(cd.card.fxZRotationRange.x, cd.card.fxZRotationRange.y));
                var obj = Instantiate(cd.card.gameFX, hit.transform.position, Quaternion.Euler(rot)); 
                Destroy(obj.gameObject, cd.card.animLength);
            }
            
            playedCard = true;
        }

        if (!playedCard) return;

        for (int i = 0; i < cd.card.drawCards; i++) gc.deck.DrawCard();
        
        gc.AddEnergy(-cd.card.cost);

        CinemachineShake.instance.ShakeCamera(cd.card.shakeCamAmt, cd.card.shakeCamTime, cd.card.shakePriority);
        int siblingIndex = transform.GetSiblingIndex();
        gc.deck.RemoveCertainCardFromHand(siblingIndex);
        
        transform.SetParent(null);
        gc.UpdateHandVisuals();
        Destroy(gameObject);

        // parsing hand
        gc.CheckIfTurnOver();
    }

    Vector3 GetMeanVector (List<GameObject> positions)
    {
        if (positions.Count == 0)
            return Vector3.zero;
    
        Vector3 meanVector = Vector3.zero;
    
        foreach (GameObject pos in positions)
            meanVector += pos.transform.position;
                
        return (meanVector / positions.Count);
    }
}
