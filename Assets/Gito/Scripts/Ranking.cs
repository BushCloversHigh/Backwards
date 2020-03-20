using System.Collections.Generic;
using UnityEngine;
using NCMB;
using UnityEngine.UI;

// ランキングを管理するクラス
public class Ranking : MonoBehaviour
{
    // ランカー
    [SerializeField] private GameObject rankerPrefab;
    // 到達レベル、説明のテキスト
    [SerializeField] private Text yourLevelNum, explain;
    // ランキングを掲載するところ
    [SerializeField] private Transform rankerBoard;
    // 名前を入力
    [SerializeField] private InputField nameField;
    // アップロードした最後の値の保存キー
    private readonly string LAST_LEVEL_UPLOAD_KEY = "lastlevelupload";
    // レベル
    private int level;

    private void Awake()
    {
        // NCMBのキーを設定
        NCMBSettings.ApplicationKey = NCMBKey.appKey;
        NCMBSettings.ClientKey = NCMBKey.clientKey;
    }

    // 説明の文字を変更する
    private void Message(string message)
    {
        explain.text = message;
    }

    // ランキングを初期化
    public void Init()
    {
        // レベルを取得し、テキストに反映
        level = Difficulty.GetLevel();
        yourLevelNum.text = level.ToString();
        // ボードをきれいにする
        for (int i = 0; i < rankerBoard.childCount; i++)
        {
            Destroy(rankerBoard.GetChild(i).gameObject);
        }
    }

    // ランキングをサーバーから取得する
    public void GetRanking()
    {
        // いったん初期化
        Init();
        // 取得
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Ranking");
        query.OrderByDescending("Level");
        query.Limit = 30;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                Message("エラーが発生しました。");
            }
            else
            {
                // ランカーオブジェクトを生成し、値を設定
                int r = 0;
                foreach (NCMBObject obj in objList)
                {
                    r++;
                    int l = System.Convert.ToInt32(obj["Level"]);
                    string n = System.Convert.ToString(obj["Name"]);
                    GameObject ranker = Instantiate(rankerPrefab, rankerBoard);
                    ranker.GetComponent<Ranker>().Set(r, n, l);
                }
            }
        });
    }

    // アップロード
    public void UploadLevel()
    {
        // レベルを更新してないとアップロードさせない
        if (level == 1 || level <= PlayerPrefs.GetInt(LAST_LEVEL_UPLOAD_KEY))
        {
            Message("レベルを更新してください。");
            return;
        }
        // 名前を入力してないとアップロードさせない
        string upName = nameField.text;
        if (string.IsNullOrEmpty(upName))
        {
            Message("名前を入力してください。");
            return;
        }
        // アップロードする
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Ranking");
        query.WhereEqualTo("Name", upName);
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e == null)
            {
                if (objList.Count == 0)
                {
                    // 新規登録
                    NCMBObject obj = new NCMBObject("Ranking");
                    obj["Name"] = upName;
                    obj["Level"] = level;
                    obj.SaveAsync((NCMBException ee) =>
                    {
                        if (ee == null)
                        {
                            Uploaded();
                        }
                        else
                        {
                            Message("エラーが発生しました。");
                        }
                    });
                }
                else
                {
                    // 念のためサーバの値より大きいときのみ更新
                    float cloudLevel = (float)System.Convert.ToDouble(objList[0]["Level"]);
                    if (level > cloudLevel)
                    {
                        objList[0]["Level"] = level;
                        objList[0].SaveAsync((NCMBException ee) =>
                        {
                            if (ee == null)
                            {
                                // アップロード成功
                                Uploaded();
                            }
                            else
                            {
                                Message("エラーが発生しました。");
                            }
                        });
                    }
                    else
                    {
                        // ランキングを更新
                        GetRanking();
                    }
                }
            }
            else
            {
                Message("エラーが発生しました。");
            }
        });
    }

    // アップロード成功時
    private void Uploaded()
    {
        // ランキングを更新
        GetRanking();
        Message("ランキングに登録しました！");
        // このレベルを保存
        PlayerPrefs.SetInt(LAST_LEVEL_UPLOAD_KEY, level);
        PlayerPrefs.Save();
    }
}
