using UnityEngine;

public class StageManager : MonoBehaviour
{
    public TextAsset stageFile;
    private TileType[,] tileTable;
    public TileManager tilePrefab;
    private TileManager[,] tilesPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        LoadStageFromText();
        DebugTable();
        CreateStage();
    }

    // パネルの設置調整
    private void CreateStage()
    {
        // 真ん中に配置するための調整
        Vector2 halfSize;
        float tileSize = tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x; // この時のbounds.sizeは1.28。BoxCollierのサイズから引っ張っている
        halfSize.x = tileSize * (tileTable.GetLength(0) / 2); // 1.28 * 2 = 2.56。四捨五入で2.6となる
        halfSize.y = tileSize * (tileTable.GetLength(1) / 2);

        for (int y = 0; y < tileTable.GetLength(1); y++)
        {
            for (int x = 0; x < tileTable.GetLength(0); x++)
            {
                TileManager tile = Instantiate(tilePrefab);

                // X.Yのポジションに指定する場所を設定
                Vector2Int position = new Vector2Int(x, y);
                Vector2 setPosition = (Vector2)position * tileSize - halfSize;

                // 引数１はDEATHかALIVEか、引数２はインスタンス化したVector2Int、引数３はこのStageMamagerを入れる
                tile.SetInit(tileTable[x, y], position, this);

                // 逆向きで配置しているので修正
                setPosition.y *= -1;

                // タイルの位置を指定したX,Yポジションに設置する
                tile.transform.position = setPosition;

                tilesPrefab[x, y] = tile;
            }
        }
    }

    private void LoadStageFromText()
    {
        // 空白を区切って改行
        //  System.StringSplitOptions.RemoveEmptyEntriesは空白を削除する意味でとりあえず入れておけばOK
        string[] lines = stageFile.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        int columns = 5;
        int rows = 5;
        tileTable = new TileType[columns, rows]; // 列挙型の情報が入っている
        tilesPrefab = new TileManager[columns, rows]; // 画面に配置しているタイルの情報が入っている
        for (int y = 0; y < rows; y++)
        {
            string[] values = lines[y].Split(new[] { ',' });
            for (int x = 0; x < columns; x++)
            {
                if (values[x] == "0")
                {
                    tileTable[x, y] = TileType.DEATH;
                }
                if (values[x] == "1")
                {
                    tileTable[x, y] = TileType.ALIVE;
                }
            }
        }
    }

    public void ClickedTile(Vector2Int center)
    {
        ReverseTiles(center);
        if (IsClear())
        {
            Debug.Log("Clear");
            ResetStage();
        }
    }

    // 中心位置を渡される（タイルをクリックしたら）
    private void ReverseTiles(Vector2Int center)
    {
        // 上下左右を入れる
        Vector2Int[] around =
        {
            center + Vector2Int.up,
            center + Vector2Int.down,
            center + Vector2Int.left,
            center + Vector2Int.right,
        };

        // 上下左右を反転させる
        foreach (Vector2Int position in around)
        {
            // 上下左右にタイルが無かったら
            if (position.x < 0 || tilesPrefab.GetLength(0) <= position.x)
            {
                // この処理をスキップし、次の繰り返し処理をする
                continue;
            }
            if (position.y < 0 || tilesPrefab.GetLength(1) <= position.y)
            {
                continue;
            }
            tilesPrefab[position.x, position.y].ReverseTile();
        }
    }

    private bool IsClear()
    {
        for (int y = 0; y < tilesPrefab.GetLength(1); y++)
        {
            for (int x = 0; x < tilesPrefab.GetLength(0); x++)
            {
                if (tilesPrefab[x, y].type == TileType.ALIVE)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void ResetStage()
    {
        for (int y = 0; y < tilesPrefab.GetLength(1); y++)
        {
            for (int x = 0; x < tilesPrefab.GetLength(0); x++)
            {
                Destroy(tilesPrefab[x, y].gameObject);
            }
        }
        CreateStage();
    }

    //デバッグ用
    private void DebugTable()
    {
        for (int y = 0; y < 5; y++)
        {
            string debugText = "";
            for (int x = 0; x < 5; x++)
            {
                debugText += tileTable[x, y] + ",";
            }
            Debug.Log(debugText);
        }
    }
}
