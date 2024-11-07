using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class EventManager
{
    public static EventHandler GenerateChoiceEvent;
   // public static EventHandler<Choice> ChoiceMadeEvent;
    //public static EventHandler<EventSpawn> CheckpointChangedEvent;
    public static EventHandler TeleportBegunEvent;
    public static EventHandler TeleportEndedEvent;
    public static EventHandler<GameObject> MilitaryKilledEvent;
    public static EventHandler<GameObject> CivilianKilledEvent;
    public static EventHandler<GameObject> ZombieKilledEvent;
    public static EventHandler<int> SelectedCharacterChangedEvent;

    public static void ChangeSelectedCharacter(int index)
    {
        SelectedCharacterChangedEvent?.Invoke(null, index);
    }

    public static void GenerateChoice()
    {
        GenerateChoiceEvent?.Invoke(null, EventArgs.Empty);
    }

    //public static void MakeChoice(Choice choice)
    //{
     //   ChoiceMadeEvent?.Invoke(null, choice);
    //}

    public static void BeginTeleport()
    {
        TeleportBegunEvent?.Invoke(null, EventArgs.Empty);
    }

    public static void EndTeleport()
    {
        TeleportEndedEvent?.Invoke(null, EventArgs.Empty);
    }

    //public static void ChangeCheckPoint(EventSpawn checkPoint)
    //{
    //    CheckpointChangedEvent?.Invoke(null, checkPoint);
   // }

    public static void KillCivilian(GameObject c)
    {
        CivilianKilledEvent?.Invoke(null, c);
    }

    public static void KillMilitary(GameObject m)
    {
        MilitaryKilledEvent?.Invoke(null, m);
    }

    public static void KillZombie(GameObject z)
    {
        ZombieKilledEvent?.Invoke(null, z);
    }
}
