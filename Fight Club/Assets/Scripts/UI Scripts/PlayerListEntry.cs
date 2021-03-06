using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerListEntry : MonoBehaviour // Script Που ρυθμίζει τον παίχτη όταν μπει σε ένα room
{
    [Header("UI References")] public Text PlayerNameText;

    public Image PlayerColorImage;
    public Button PlayerReadyButton;
    public GameObject PlayerReadyImage;

    private int ownerId;
    private bool isPlayerReady;
    private int playerPrefab;
    
    #region UNITY

    public void OnEnable()
    {
        PlayerNumbering.OnPlayerNumberingChanged += OnPlayerNumberingChanged;
    }

    public void Start()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (p.ActorNumber == ownerId)
            {
                PlayerColorImage.color = GameConstants.GetColor(p.GetPlayerNumber());
            }
        }
        if (PhotonNetwork.LocalPlayer.ActorNumber != ownerId)
        {
            PlayerReadyButton.gameObject.SetActive(false);
        }
        else
        {
            Hashtable initialProps = new Hashtable()
            {
                {GameConstants.PLAYER_READY, isPlayerReady},
            };
            PhotonNetwork.LocalPlayer.SetCustomProperties(initialProps);
            PhotonNetwork.LocalPlayer.SetScore(0);

            PlayerReadyButton.onClick.AddListener(() =>
            {
                isPlayerReady = !isPlayerReady;
                SetPlayerReady(isPlayerReady);

                Hashtable props = new Hashtable() {{GameConstants.PLAYER_READY, isPlayerReady}};
                PhotonNetwork.LocalPlayer.SetCustomProperties(props);

                if (PhotonNetwork.IsMasterClient)
                {
                    FindObjectOfType<MultiplayerMenu>().LocalPlayerPropertiesUpdated();
                }
            });
        }
    }

    public void OnDisable()
    {
        PlayerNumbering.OnPlayerNumberingChanged -= OnPlayerNumberingChanged;
    }

    #endregion

    public void Initialize(int playerId, string playerName)
    {
        ownerId = playerId;
        PlayerNameText.text = playerName;
    }

    private void OnPlayerNumberingChanged()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (p.ActorNumber == ownerId)
            {
                PlayerColorImage.color = GameConstants.GetColor(p.GetPlayerNumber());
            }
        }   
    }

    public void SetPlayerReady(bool playerReady)
    {
        PlayerReadyButton.GetComponent<ButtonManager>().buttonText = playerReady ? "Cancel" : "Ready?";
        PlayerReadyButton.GetComponent<ButtonManager>().UpdateUI();
        PlayerReadyImage.SetActive(playerReady);
    }
}