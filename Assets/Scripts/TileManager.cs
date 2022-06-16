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
    private StageManager stageManager;
    private Vector2Int intPosition;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //引数２のStageManagerを変数stageManagerに入れる
    public void SetInit(TileType tileType, Vector2Int position, StageManager stageManager)
    {
        intPosition = position;
        this.stageManager = stageManager;
        SetType(tileType);
    }

    private void SetType(TileType tileType)
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
        stageManager.ClickedTile(intPosition);
    }

    public void ReverseTile()
    {
        if (type == TileType.DEATH)
        {
            // 関数を呼び出してタイルと画像を変える
            SetType(TileType.ALIVE);

            // この場合だと、タイルを変更するが画像が変えられない。
            // イベント関数であれば呼ばれるが、自作で作った関数なので関数ごと呼び出さないと画像は変更されない
            //type = TileType.ALIVE;
        }
        else if (type == TileType.ALIVE)
        {
            SetType(TileType.DEATH);
            //type = TileType.DEATH;
        }
    }
}
