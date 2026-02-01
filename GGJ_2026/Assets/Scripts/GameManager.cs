using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PlayerStats psStart, psPlayer;

    [HideInInspector] public bool isPaused;
    [HideInInspector] public int sceneIndex;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
        isPaused = false;
        Time.timeScale = 1f;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private void Start()
    {
        PauseUI.Instance.gameObject.SetActive(false);
        PlayerMasks.Instance.currentMask = psPlayer.currentMask;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
                PauseUI.Instance.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                isPaused = false;
                PauseUI.Instance.gameObject.SetActive(false);
            }
        }
        if (psPlayer.currentHP <= 0)
        {
            ResetPlayer();
        }
        if (psPlayer.mask1 && psPlayer.mask2 && psPlayer.mask3)
        {
            // boss
        }
    }
    public void ResetPlayer()
    {
        psPlayer.currentHP = psPlayer.maxHP;
        psPlayer.currentMask = PlayerMasks.Instance.currentMask;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
