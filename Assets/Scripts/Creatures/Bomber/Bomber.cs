using Creatures;
using Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomber : Creature
{
    protected override void OnDied() => 
        Game.LevelService.Reload();
}