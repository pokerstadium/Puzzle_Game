using UnityEngine;

public enum TileType
{
    DEATH,
    ALIVE
}

public class TileManager : MonoBehaviour
{
    public TileType type;
    private SpriteRenderer spriteRenderer;
    public Sprite deathSprite;
    public Sprite aliveSprite;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void SetType(TileType tileType)
    {
        type = tileType;
        SetImage(type);
    }

    private void SetImage(TileType type)
    {
        if (type == TileType.DEATH)
        {
            spriteRenderer.sprite = deathSprite;
        }
        else if (type == TileType.ALIVE)
        {
            spriteRenderer.sprite = aliveSprite;
        }
    }

    public void OnTile()
    {
        ReverseTile();
    }

    private void ReverseTile()
    {
        if (type == TileType.DEATH)
        {
            SetType(TileType.ALIVE);
        }
        else if (type == TileType.ALIVE)
        {
            SetType(TileType.DEATH);
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
