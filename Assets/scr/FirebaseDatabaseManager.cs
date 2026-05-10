using Firebase.Database;
using UnityEngine;
using Firebase;
using System.Threading.Tasks;
using Firebase.Extensions;

public class FirebaseDatabaseManager : MonoBehaviour
{
    private DatabaseReference reference;
    private void Awake()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

   
    public void WriteDatabase (string id, string message)
    {
        reference.Child("Users").Child(id).SetValueAsync(message).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("ghi dữ liệu thành công");
            }
            else
            {
                Debug.LogError("Lỗi ghi dữ liệu: " + task.Exception);
            }
        });
    }

    public void ReadDatabase (string id)
    {
        reference.Child("Users").Child(id).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            
             if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                
                Debug.Log("doc thanh cong: " + snapshot.Value.ToString());
            }
             else
             {
                    Debug.LogError("Lỗi đọc dữ liệu: " + task.Exception);
            }
        });
    }
}
