using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PlayerStats psStart, psPlayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            return;
        }
        Destroy(gameObject);

        //psPlayer.SetUp(psStart, psPlayer);

        PlayerMasks.Instance.currentMask = psPlayer.currentMask;
    }
    private void Update()
    {
        if (psPlayer.currentHP <= 0)
        {
            ResetPlayer();
        }
    }
    public void ResetPlayer()
    {
        psPlayer.currentHP = psPlayer.maxHP;
        psPlayer.currentMask = PlayerMasks.Instance.currentMask;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ChangeBiom()
    {
        // keep current mask
    }
}
