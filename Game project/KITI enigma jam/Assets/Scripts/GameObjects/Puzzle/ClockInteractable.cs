﻿using System;
using System.Linq;
using UnityEngine;

namespace GameObjects.Puzzle
{
    [Serializable]
    public class HideByClock
    {
        [SerializeField] internal int[] statesToShowIn;
        [SerializeField] internal GameObject gameObject;
    }
    
    public class ClockInteractable : Interactable
    {
        [SerializeField] private GameObject[] clockStates;
        [SerializeField] private int state;
        [SerializeField] private HideByClock[] hideByClockObjects;

        private void Start()
        {
            clockStates[state].SetActive(true);
            for (int i = 0; i < clockStates.Length; i++)
            {
                if (i != state)
                {
                    clockStates[i].SetActive(false);
                }
            }
            UpdateHiddenObjects();
        }

        public override void OnInteract()
        {
            clockStates[state].SetActive(false);
            state = (state + 1) % clockStates.Length;
            clockStates[state].SetActive(true);
            UpdateHiddenObjects();
        }

        private void UpdateHiddenObjects()
        {
            foreach (var hideByClockObject in hideByClockObjects)
            {
                var shouldShow = hideByClockObject.statesToShowIn.Contains(state);
                hideByClockObject.gameObject.SetActive(shouldShow);
            }
        }

        public override void OnFocus()
        {
        }

        public override void OnLoseFocus()
        {
        }
    }
}