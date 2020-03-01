using System.Collections.Generic;
using UnityEngine;
using NCMB;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [SerializeField] private GameObject rankerPrefab;
    [SerializeField] private Text yourLevelNum, explain;
    [SerializeField] private Transform rankerBoard;
    [SerializeField] private InputField nameField;

    private readonly string LAST_LEVEL_UPLOAD_KEY = "lastlevelupload";

    private int level;

    private void Awake()
    {
        NCMBSettings.ApplicationKey = NCMBKey.appKey;
        NCMBSettings.ClientKey = NCMBKey.clientKey;
    }

    private void Message(string message)
    {
        explain.text = message;
    }

    public void Init()
    {
        level = Difficulty.GetLevel();
        yourLevelNum.text = level.ToString();
        for (int i = 0; i < rankerBoard.childCount; i++)
        {
            Destroy(rankerBoard.GetChild(i).gameObject);
        }
    }

    public void GetRanking()
    {
        Init();
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Ranking");
        query.OrderByDescending("Level");
        query.Limit = 30;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                //検索失敗時の処理
                Message("エラーが発生しました。");
            }
            else
            {
                //検索成功時の処理
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

    public void UploadLevel()
    {
        if (level == 1 || level <= PlayerPrefs.GetInt(LAST_LEVEL_UPLOAD_KEY))
        {
            Message("レベルを更新してください。");
            return;
        }
        string upName = nameField.text;
        if (string.IsNullOrEmpty(upName))
        {
            Message("名前を入力してください。");
            return;
        }

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Ranking");
        query.WhereEqualTo("Name", upName);
        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            //検索成功したら
            if (e == null)
            {
                //未登録
                if (objList.Count == 0)
                {
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
                    float cloudLevel = (float)System.Convert.ToDouble(objList[0]["Level"]);
                    if (level > cloudLevel)
                    {
                        objList[0]["Level"] = level;
                        objList[0].SaveAsync((NCMBException ee) =>
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

    private void Uploaded()
    {
        GetRanking();
        Message("ランキングに登録しました！");
        PlayerPrefs.SetInt(LAST_LEVEL_UPLOAD_KEY, level);
        PlayerPrefs.Save();
    }
}
