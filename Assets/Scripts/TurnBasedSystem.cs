using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

enum TurnStatus
{
    none,
    player1,
    player2
}

public class TurnBasedSystem : MonoBehaviour
{
    public static TurnBasedSystem Instance { get; private set; }

    int currentHeroIndex = 0;
    List<Hero> heroes;

    Hero currentHero;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;

        heroes = new List<Hero>();
    }

    // Start is called before the first frame update
    void Start()
    {
        var array = FindObjectsOfType<Hero>();

        foreach (var hero in array)
        {
            heroes.Add(hero);
        }

        currentHero = heroes[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !currentHero.Moving)
            EndTurn();

        
    }

    public void HeroDies(Hero p_hero)
    {
        heroes.Remove(p_hero);

        if (p_hero != currentHero)
            currentHeroIndex = heroes.IndexOf(currentHero);
        else
        {
            currentHero = null;
            EndTurn();
        }
    }

    void EndTurn()
    {
        if (currentHero != null)
            currentHero.EndTurnReset();

        currentHeroIndex++;

        if (currentHeroIndex >= heroes.Count)
            currentHeroIndex = 0;

        currentHero = heroes[currentHeroIndex];
    }

    public Hero GetCurrentHero()
    {
        return currentHero;
    }
}
