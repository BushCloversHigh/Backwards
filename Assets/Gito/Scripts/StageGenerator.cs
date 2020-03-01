using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    // 床、障害物のプレハブ
    [SerializeField] private GameObject planePrefab, enemyPrefab, clearBoxPrefab, cubeObjPrefab;
    // 障害物の間隔
    [SerializeField] private float interval = 5.0f;
    
    // 難易度のインスタンス
    private Difficulty difficulty;

    private float planeScaleX;

    public float GetPlaneScaleX()
    {
        return planeScaleX;
    }

    public float GetInterval()
    {
        return interval;
    }

    public void Generate()
    {
        // 難易度を持ってくる
        difficulty = GetComponent<Difficulty>();
        int column = difficulty.GetColumn();
        int row = difficulty.GetRow();
        // 最初の床を生成
        planeScaleX = 1.0f + (column - 3) * 0.3125f;
        GameObject plane = Instantiate(planePrefab);
        plane.transform.position = new Vector3(0, 0, 10f);
        plane.transform.localScale = new Vector3(planeScaleX, 1.0f, 1.0f);
        // 必要分の床を生成
        for (int i = 0; i < (row / 2) + 1; i++)
        {
            GameObject plane2 = Instantiate(planePrefab);
            plane2.transform.position = new Vector3(0, 0, -i * 10f);
            plane2.transform.localScale = new Vector3(1.0f + (column - 3) * 0.3125f, 1.0f, 1.0f);
        }
        int enemyCount = 0;
        // 行ごとに障害物を生成していく
        for(int i = 0; i < row; i++)
        {
            // この行で生成する列のブール
            bool[] columnObstacle = new bool[column];
            // この行で生成する数
            int trueCount = 0;
            // それぞれの列で、半分の確率で生成する
            for(int j = 0; j < column; j++)
            {
                columnObstacle[j] = Random.Range(0, 2) == 0 ? false : true;
                trueCount += columnObstacle[j] ? 1 : 0;
            }
            // もし全列で生成されたら、一個消す
            if(trueCount == column)
            {
                columnObstacle[Random.Range(0, column)] = false;
            }
            // もし全列で生成されなかったら、一個だけ生成
            if(trueCount == 0)
            {
                columnObstacle[Random.Range(0, column)] = true;
            }
            // trueの列に障害物を生成
            for (int j = 0; j < column; j++)
            {
                if (columnObstacle[j])
                {
                    Instantiate(enemyPrefab, new Vector3(0.75f + (j * 1.25f), 0.0f, -i * interval), Quaternion.identity);
                    enemyCount++;
                }
            }
        }

        GameObject clearBox = Instantiate(clearBoxPrefab);
        clearBox.transform.position = new Vector3(0f, 0f, -row * interval);
        clearBox.transform.localScale = new Vector3(planeScaleX * 4.0f, 1.0f, 1.0f);

        Vector3 playArea = new Vector3(0.75f + (column * 1.25f), 0, -row * interval - 10.0f);

        for(int i = 0; i < (int)(enemyCount * 1.5f); i++)
        {
            Ray ray = new Ray(new Vector3(Random.Range(-20.0f, playArea.x + 20.0f), 10.0f, Random.Range(playArea.z - 10.0f, 20.0f)), Vector3.down);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 20.0f))
            {
                if (hit.point.x > -5.0f && hit.point.x < playArea.x + 5.0f && hit.point.z > playArea.z && hit.point.z < 13.0f)
                {
                    i--;
                    continue;
                }
                else
                {
                    GameObject cube = Instantiate(cubeObjPrefab, hit.point, Quaternion.identity);
                    float r = Random.Range(0.25f, 1.5f);
                    cube.transform.localScale = Vector3.one * r;
                }
            }
        }
    }
}
