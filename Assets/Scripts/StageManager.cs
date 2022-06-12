using UnityEngine;

public class StageManager : MonoBehaviour
{
    public TextAsset stageFile;
    private TileType[,] tileTable;
    public TileManager tilePrefab;

    // Start is called before the first frame update
    private void Start()
    {
        LoadStageFromText();
        DebugTable();
        CreateStage();
    }

    void CreateStage()
    {
        for (int y = 0; y < tileTable.GetLength(1); y++)
        {
            for (int x = 0; x < tileTable.GetLength(0); x++)
            {
                TileManager tile = Instantiate(tilePrefab);
                tile.SetType(tileTable[x,y]);
                Vector2Int position = new Vector2Int(x, y);
                tile.transform.position = (Vector2)position;
            }
        }

    }

    private void LoadStageFromText()
    {
        // 空白を区切って改行
        //  System.StringSplitOptions.RemoveEmptyEntriesは空白を削除する意味でとりあえず入れておけばOK
        string[] lines = stageFile.text.Split(new[] {'\n','\r'}, System.StringSplitOptions.RemoveEmptyEntries);
        int columns = 5;
        int rows = 5;
        tileTable = new TileType[columns, rows];
        for (int y = 0; y<rows; y++)
        {
            string[] values = lines[y].Split(new[] {','});
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

    //デバッグ用
    void DebugTable()
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
