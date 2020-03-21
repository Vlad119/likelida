using TMPro;
using UnityEngine;

public class FonReviewScript : MonoBehaviour
{
    public GameObject[] starss;
    public int stars;
    public TMP_Text review_text;
    public TMP_Text name;

    public void OnEnable()
    {
        var AM = AppManager.Instance;
        AM.BottomBar.SetActive(true);
        name.text = AM.currentDish.name;
        review_text.text = "";
        stars = 1;
        ChangeStar1();
    }

    public void AllClose()
    {
        stars = 0;
        foreach (GameObject star in starss)
        {
            star.SetActive(false);
        }
    }

    public void ChangeStar1()
    {
        AllClose();
        {
            stars = 1;
            for (int i = 0; i < stars; i++)
                starss[i].SetActive(true);
        }
    }

    public void ChangeStar2()
    {
        AllClose();
        {
            stars = 2;
            for (int i = 0; i < stars; i++)
                starss[i].SetActive(true);
        }
    }

    public void ChangeStar3()
    {
        AllClose();
        {
            stars = 3;
            for (int i = 0; i < stars; i++)
                starss[i].SetActive(true);
        }
    }

    public void ChangeStar4()
    {
        AllClose();
        {
            stars = 4;
            for (int i = 0; i < stars; i++)
                starss[i].SetActive(true);
        }
    }

    public void ChangeStar5()
    {
        AllClose();
        {
            stars = 5;
            for (int i = 0; i < stars; i++)
                starss[i].SetActive(true);
        }
    }

    public async void SendReview()
    {
        var id = AppManager.Instance.currentDish.id;
        var DPP = AppManager.Instance.dishesPlayerPrefs;
        var review = AppManager.Instance.review;
        review.review_user_name = AppManager.Instance.userInfo.user.phone;
        review.review_text = review_text.text;
        review.review_id = id;
        review.review_rating = stars.ToString();
        DPP.userReviews.Add(review);
        string s = JsonUtility.ToJson(DPP);
        PlayerPrefs.SetString("dish", s);
        await WebHandler.Instance.SendReviewWrapper((repl) =>
        {
            AppManager.Instance.SwitchScreen(4);
        }, "&review_id=" + id + "&review_rating=" + stars.ToString() 
        + "&review_text=" + review_text.text + "&review_user_name=" + review.review_user_name);
    }
}
