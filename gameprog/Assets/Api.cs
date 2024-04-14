using UnityEngine;
using UnityEngine.Networking;
using System.Collections; 
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ApiTester : MonoBehaviour
{
    private string apiUrl = "http://localhost:3000/api/rewards/";

    public TMP_InputField usernameInputField;

    private string username;

    public void GetRewards()
    {
       
        username = PlayerPrefs.GetString("PlayerName");
    
        string url = apiUrl + username;

        StartCoroutine(SendRequest(url));
    }

    IEnumerator SendRequest(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erreur lors de la récupération des récompenses: " + www.error);
            }
            else
            {
                Debug.Log("Récompenses récupérées pour " + username + ": " + www.downloadHandler.text);

                string items = www.downloadHandler.text;
                string[] itemsParse = items.Split("\"");
                int numberOfItem = itemsParse.Count();
                string inventory = PlayerPrefs.GetString("Inventory");
                if (inventory != "")
                {
                    inventory += ",";
                }
                for (var i = 5; i < numberOfItem;i++) 
                {        
                    inventory += itemsParse[i] + ",";
                    i += 7;
                }
                PlayerPrefs.SetString("Inventory", inventory.Substring(0, inventory.Length - 1));
                MarkRewardAsClaimed();
            }
        }
    }

    void MarkRewardAsClaimed()
    {
        string claimUrl = apiUrl + "claim/" + username;

        StartCoroutine(SendClaimRequest(claimUrl));
    }

    IEnumerator SendClaimRequest(string url)
    {
        using (UnityWebRequest claimRequest = UnityWebRequest.PostWwwForm(url, ""))
        {
            yield return claimRequest.SendWebRequest();

            if (claimRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erreur lors de la réclamation de la récompense: " + claimRequest.error);
            }
            else
            {
                Debug.Log("Récompense réclamée avec succès pour " + username);

                DeleteReward();
            }
        }
    }

    void DeleteReward()
    {
        string deleteUrl = apiUrl + "delete/" + username;

        StartCoroutine(SendDeleteRequest(deleteUrl));
    }

    IEnumerator SendDeleteRequest(string url)
    {
        using (UnityWebRequest deleteRequest = UnityWebRequest.Delete(url))
        {
            yield return deleteRequest.SendWebRequest();

            if (deleteRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erreur lors de la suppression de la récompense: " + deleteRequest.error);
            }
            else
            {
                Debug.Log("Récompense supprimée avec succès pour " + username);
            }
        }
    }
}
