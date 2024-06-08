using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    Animator anim;
    Camera cam;
    [SerializeField] CollectibleTypes requiredKey = CollectibleTypes.Green_Key;
    [SerializeField] SpriteRenderer keySprite;
    public AudioClip openSound;
    public AudioClip lockedSound;
    public AudioClip enterSound;
    [Space]
    public GameObject roomCamera;
    [SerializeField] GameObject targetDoor;
    [Space]
    public bool isUnlocked;
    [SerializeField] bool isOpening;
    [SerializeField] bool isFinalDoor;


    void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
        if (isUnlocked)
        {
            anim.SetTrigger("startOpen");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            if (collision.gameObject.CompareTag("Player") && !isOpening)
            {
                GameObject player = collision.gameObject;
                Open(player);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isUnlocked && keySprite)
        {
            StartCoroutine(KeyFadeIn());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isUnlocked && keySprite)
        {
            StartCoroutine(KeyFadeOut());

        }
    }

    public void Open(GameObject player)
    {
        if (isUnlocked)
        {
            if (isFinalDoor)
            {
                isOpening = true;
                FinishStage();
            }
            else
            {
                StartCoroutine(MovePlayer(player));
            }
        }
        else
        {
            if (player.GetComponent<Inventory>().UseItem(requiredKey))
            {
                Unlock();
            }
            else
            {
                StartCoroutine(OpenCooldown());
                SoundManager.instance.PlaySound(lockedSound);
            }
        }
    }

    void Unlock()
    {
        SoundManager.instance.PlaySound(openSound);
        StartCoroutine(OpenCooldown());
        StartCoroutine(KeyFadeOut());
        anim.SetTrigger("open");
        isUnlocked = true;
    }

    void FinishStage()
    {
        SoundManager.instance.PlaySound(enterSound);
        cam.GetComponent<CameraController>().CameraFadeIn();
        SaveManager.instance.Finish();
        StartCoroutine(cam.GetComponent<CameraController>().WaitFade(() => NextLevel()));
    }

    void NextLevel()
    {
        if (SaveManager.instance.level_id == "Level 2")
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator MovePlayer(GameObject player)
    {
        isOpening = true;
        SoundManager.instance.PlaySound(enterSound);
        player.GetComponentInChildren<Movement>().canMove = false;
        yield return StartCoroutine(player.GetComponentInChildren<PlayerAnimation>().FadeOut());
        cam.GetComponent<CameraController>().MoveToNewRoom(targetDoor.GetComponent<Door>().roomCamera.transform);
        player.transform.position = targetDoor.transform.position;
        yield return StartCoroutine(player.GetComponentInChildren<PlayerAnimation>().FadeIn());
        player.GetComponentInChildren<Movement>().canMove = true;
        isOpening = false;
    }

    IEnumerator OpenCooldown()
    {
        isOpening = true;
        yield return new WaitForSeconds(.5f);
        isOpening = false;
    }

    IEnumerator KeyFadeIn()
    {
        keySprite.enabled = true;
        Color c = keySprite.color;
        float alpha = c.a;
        for (; alpha <= 1; alpha += 0.1f)
        {
            c.a = alpha;
            keySprite.color = c;
            yield return new WaitForSeconds(.01f);
        }
    }

    IEnumerator KeyFadeOut()
    {
        Color c = keySprite.color;
        float alpha = c.a;
        for (; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            keySprite.color = c;
            yield return new WaitForSeconds(.01f);
        }
        keySprite.enabled = false;
    }
}
