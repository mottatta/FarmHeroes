using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreTree : MonoBehaviour
{
    public Color colorAvailable;
    public Color colorUnavailable;
    public SpriteRenderer bodyRenderer;
    public GameObject treePrefab;
    public int priceMoney;
    public int priceWood;
    public int priceFood;
    int colliders = 0;
    LevelManager levelManager;
    public AudioClip sfxRoot;
    public AudioClip sfxCantRoot;
    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("levelManager").GetComponent<LevelManager>();
        MakeAvailable();
    }

    void Update()
    {
        MoveToMouse();
    }

    void MoveToMouse()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = transform.position.z;
        transform.position = newPos;
    }

    private void OnMouseDown()
    {
        if (colliders <= 0)
        {
            Root();
        }
        else CantRoot();
    }

    public void SetPrices(int m, int w, int f)
    {
        priceMoney = m;
        priceWood = w;
        priceFood = f;
    }

    void Root()
    {
        if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(sfxRoot);
        GameObject tree = Instantiate(treePrefab);
        tree.transform.position = transform.position;
        levelManager.PurchaseComplete(priceMoney, priceWood, priceFood);
        Destroy(gameObject);
    }

    void CantRoot()
    {
        if (SoundManager.GetInstance()) SoundManager.GetInstance().PlaySFX(sfxCantRoot);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "gamePlayArea")
        {
            colliders++;
            MakeUnavailable();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "gamePlayArea")
        {
            colliders--;
            if (colliders <= 0) MakeAvailable();
        }
    }

    void MakeAvailable()
    {
        bodyRenderer.color = colorAvailable;
    }

    void MakeUnavailable()
    {
        bodyRenderer.color = colorUnavailable;
    }
}
