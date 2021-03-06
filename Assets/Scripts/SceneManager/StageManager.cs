using UnityEngine;

public class StageManager : MonoBehaviour
{
    public TextAsset[] stageFiles; // 複数のステージをセット
    private TileType[,] tileTable;
    public TileManager tilePrefab;
    private TileManager[,] tilesPrefab;
    public GameManager gameManager;
    public AudioClip audioClip;
    AudioSource audioSource;

    // GameManagerからクリアしたときの関数を与えられる
    public delegate void StageClear();
    public StageClear stageClear;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // パネルの設置調整
    public void CreateStage()
    {
        audioSource.enabled = true;
        // 真ん中に配置するための調整
        Vector2 halfSize;
        float tileSize = tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x; // Collider2Dのサイズを参考にしているが、スプライト自体の大きさに合わせているのでそれぞれの値が違う

        // テキストの長さが奇数・偶数に応じてパネルを真ん中に配置するようにする
        if (tileTable.GetLength(0) % 2 == 0 && tileTable.GetLength(1) % 2 == 0)
        {
            halfSize.x = tileSize * (tileTable.GetLength(0) / 2.7f);
            halfSize.y = tileSize * (tileTable.GetLength(1) / 2.7f);
        }
        else
        {
            halfSize.x = tileSize * (tileTable.GetLength(0) / 2); // Collider2Dのサイズ * 2 = halffSize。四捨五入される
            halfSize.y = tileSize * (tileTable.GetLength(1) / 2);
        }

        for (int y = 0; y < tileTable.GetLength(1); y++)
        {
            for (int x = 0; x < tileTable.GetLength(0); x++)
            {
                TileManager tile = Instantiate(tilePrefab);

                // X.Yのポジションに指定する場所を設定
                Vector2Int position = new Vector2Int(x, y);

                Vector2 setPosition = (Vector2)position * tileSize - halfSize;

                // 引数１はDEATHかALIVEか、引数２はインスタンス化したVector2Intを入れる
                tile.SetInit(tileTable[x, y], position);

                tile.clicked += ClickedTile;

                // 逆向きで配置しているので修正
                // textデータの読み込みは左上からだが、表示するときは左下から表示するになるので‐1をかけて反転させている
                setPosition.y *= -1;

                // タイルの位置を指定したX,Yポジションに設置する
                tile.transform.position = setPosition;

                tilesPrefab[x, y] = tile;
            }
        }
    }

    // ステージテキスト読み込み
    public void LoadStageFromText(int loadstage)
    {
        // 空白を区切って改行
        //  System.StringSplitOptions.RemoveEmptyEntriesは空白を削除する意味でとりあえず入れておけばOK
        string[] lines = stageFiles[loadstage].text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        int columns = lines.Length;
        int rows = lines.Length;
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

    // クリア判定
    public void ClickedTile(Vector2Int center)
    {
        ReverseTiles(center);
        if (IsClear())
        {
            audioSource.enabled = false;
            Debug.Log("Clear");
            stageClear();
        }
    }

    // タイルをクリックしたら
    private void ReverseTiles(Vector2Int center)
    {
        audioSource.PlayOneShot(audioClip);
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

    // クリア判定
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

    // パネルの削除
    public void DestroyStage()
    {
        for (int y = 0; y < tilesPrefab.GetLength(1); y++)
        {
            for (int x = 0; x < tilesPrefab.GetLength(0); x++)
            {
                Destroy(tilesPrefab[x, y].gameObject);
            }
        }
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
