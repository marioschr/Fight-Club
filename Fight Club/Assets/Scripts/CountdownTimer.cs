﻿using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

    public class CountdownTimer : MonoBehaviourPunCallbacks // Script για την αντίστροφη μέτρηση κατά την έναρξη του αγώνα
    {
        public delegate void CountdownTimerHasExpired();

        public const string CountdownStartTime = "StartTime";

        [Header("Countdown time in seconds")] 
        public float Countdown = 5.0f;

        private bool isTimerRunning;

        private int startTime;

        [Header("Reference to a Text component for visualizing the countdown")]
        public Text Text;
        
        public static event CountdownTimerHasExpired OnCountdownTimerHasExpired;


        public void Start()
        {
            if (this.Text == null) Debug.LogError("Reference to 'Text' is not set. Please set a valid reference.", this);
        }

        public override void OnEnable()
        {
            base.OnEnable();
            Initialize();
        }


        public void Update()
        {
            if (!this.isTimerRunning) return;

            float countdown = TimeRemaining(); // Ανανέωση του χρόνου στο text για κάθε second
            this.Text.text = string.Format("Game starts in {0} seconds", countdown.ToString("n0"));

            if (countdown > 0.0f) return;

            OnTimerEnds();
        }


        private void OnTimerRuns()
        {
            this.isTimerRunning = true;
            this.enabled = true;
        }

        private void OnTimerEnds() // Όταν τελειώσει ο χρόνος ενεργοποιούμε την κίνηση των χαρακτήρων
        {
            this.isTimerRunning = false;
            this.enabled = false;
            Debug.Log("Emptying info text.", this.Text);
            this.Text.text = string.Empty;
            GameObject.Find("GUI").GetComponent<PlayerPause>().enabled = true;
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                player.GetComponent<Movement>().enabled = true;
                player.GetComponent<Fighting>().enabled = true;
            }
            if (OnCountdownTimerHasExpired != null) OnCountdownTimerHasExpired();
        }


        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            Debug.Log("CountdownTimer.OnRoomPropertiesUpdate " + propertiesThatChanged.ToStringFull());
            Initialize();
        }


        private void Initialize()
        {
            int propStartTime;
            if (TryGetStartTime(out propStartTime))
            {
                this.startTime = propStartTime;
                Debug.Log("Initialize sets StartTime " + this.startTime + " server time now: " + PhotonNetwork.ServerTimestamp + " remain: " + TimeRemaining());


                this.isTimerRunning = TimeRemaining() > 0;

                if (this.isTimerRunning)
                    OnTimerRuns();
                else
                    OnTimerEnds();
            }
        }


        private float TimeRemaining()
        {
            int timer = PhotonNetwork.ServerTimestamp - this.startTime;
            return this.Countdown - timer / 1000f;
        }


        public static bool TryGetStartTime(out int startTimestamp)
        {
            startTimestamp = PhotonNetwork.ServerTimestamp;

            object startTimeFromProps;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(CountdownStartTime, out startTimeFromProps))
            {
                startTimestamp = (int)startTimeFromProps;
                return true;
            }

            return false;
        }


        public static void SetStartTime()
        {
            int startTime = 0;
            bool wasSet = TryGetStartTime(out startTime);

            Hashtable props = new Hashtable
            {
                {CountdownTimer.CountdownStartTime, (int)PhotonNetwork.ServerTimestamp}
            };
            PhotonNetwork.CurrentRoom.SetCustomProperties(props);


            Debug.Log("Set Custom Props for Time: "+ props.ToStringFull() + " wasSet: "+wasSet);
        }
    }