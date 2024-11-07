using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager Instance;

    [SerializeField] private GameObject[] _events;

    private Choice[] _currentChoiceSet;
    private double _morality;
    private int _numChoices;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _currentChoiceSet = new Choice[3];
            _numChoices = 0;
            return;
        }

        Destroy(gameObject);
    }

    void Start()
    {
        EventManager.GenerateChoiceEvent += OnChoiceGenerated;
        EventManager.ChoiceMadeEvent += OnChoiceMade;
    }

    public GameObject GetRandomEvent()
    {
        return _events.GetRandomElement();
    }

    private void OnChoiceMade(object sender, Choice choice)
    {
        _numChoices++;

        foreach (var c in _currentChoiceSet)
        {
            _morality = c == choice ? _morality + choice.ChoicePrice : _morality + choice.AvoidancePrice;
        }
    }

    private void OnChoiceGenerated(object sender, EventArgs e)
    {
        
    }

    void OnDestroy()
    {
        EventManager.GenerateChoiceEvent -= OnChoiceGenerated;
        EventManager.ChoiceMadeEvent -= OnChoiceMade;
    }
}
