using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviewPrefScript : MonoBehaviour
{
    public RawImage avatar;
    public TMP_Text review;
    public TMP_Text name;
    public GameObject[] starss;
    public int stars;
    public Reviews rev;


    public async void CreatePref(Reviews reviews)
    {
        if (rev==null)
            await new WaitForSeconds(.01f);
        rev = reviews;
        name.text = reviews.review_user_name;
        review.text = reviews.text;
        Stars(reviews);
    }

    public async void CreatePref2(Reviews reviews)
    {
        if (rev == null)
            await new WaitForSeconds(.01f);
        rev = reviews;
        name.text = reviews.review_user_name;
        review.text = reviews.review_text;
        Stars2(reviews);
    }

    public void Open()
    {
        var AM = AppManager.Instance;
        foreach (var dish in AM.res.dish)
        {
            if (dish.id == rev.review_id)
            {
                AM.currentDish = dish;
                AM.SwitchScreen(4);
            }
        }
    }

    //async Task<Texture> LoadImg(string endpoint)
    //{
    //    using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(endpoint))
    //    {
    //        await request.SendWebRequest();
    //        var texture = DownloadHandlerTexture.GetContent(request);
    //        return texture;
    //    }
    //}

    public void Stars2(Reviews reviews)
    {
        if (reviews.review_rating == "1")
        { stars = 1; Star1(); }
        if (reviews.review_rating == "2")
        { stars = 2; Star2(); }
        if (reviews.review_rating == "3")
        { stars = 3; Star3(); }
        if (reviews.review_rating == "4")
        { stars = 4; Star4(); }
        if (reviews.review_rating == "5")
        { stars = 5; Star5(); }
    }

    public void Stars(Reviews reviews)
    {
        if (reviews.rating == "1")
        { stars = 1; Star1(); }
        if (reviews.rating == "2")
        { stars = 2; Star2(); }
        if (reviews.rating == "3")
        { stars = 3; Star3(); }
        if (reviews.rating == "4")
        { stars = 4; Star4(); }
        if (reviews.rating == "5")
        { stars = 5; Star5(); }
    }

    public void Star1()
    {
        for (int i = 0; i < stars; i++)
            starss[i].SetActive(true);
    }

    public void Star2()
    {
        for (int i = 0; i < stars; i++)
            starss[i].SetActive(true);
    }

    public void Star3()
    {
        for (int i = 0; i < stars; i++)
            starss[i].SetActive(true);
    }
    public void Star4()
    {
        for (int i = 0; i < stars; i++)
            starss[i].SetActive(true);
    }

    public void Star5()
    {
        for (int i = 0; i < stars; i++)
            starss[i].SetActive(true);
    }
}
