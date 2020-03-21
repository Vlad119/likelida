using System;
using System.Collections.Generic;


[Serializable]
public class Res
{
    public List<Cats> cats = new List<Cats>();
    public List<Ingredients> ingrediens = new List<Ingredients>();
    public List<Dish> dish = new List<Dish>();
    public List<Ingrediens_cat> ingrediens_cat = new List<Ingrediens_cat>();
    public List<Company> company = new List<Company>();
    public List<string> emails = new List<string>();
}


[Serializable]
public class Ingrediens_cat
{
    public string name;
    public string tid;
    public List<Nid> nids = new List<Nid>();
}

[Serializable]
public class Nid
{
    public string nid;
    public string title;
    public bool isON;
    public bool repeat = false;
    public int count=1;
    public string img;
}


[Serializable]
public class Cats
{
    public string tid;
    public string name;
    public bool selected = false;
    public int count;
    public int popCount;
    public int newCount;
    public int likedCount;
}

[Serializable]
public class Coords
{
    public float lat;
    public float lon;
}

[Serializable]
public class Ingredients
{
    public string id;
    public string name;
    public string description;
    public List <Img> img = new  List<Img>();
}

[Serializable]
public class Company
{
    public string id;
    public string name;
    public string description;
    public string address;
    public float lat;
    public float lon;
    public int rating;
    public float distance;
}

[Serializable]
public class Dish
{
    public bool showed=false;
    public bool like;
    public string id;
    public string name;
    public string cat;
    public List<Img> img = new List<Img>();
    public List<Cooking> cooking = new List<Cooking>();
    public string description;
    public List<Ing> ing = new List<Ing>();
    public string kcal;
    public string serving;
    public string time;
    public List<Reviews> reviews = new List<Reviews>();
    public List<Pars> pars = new List<Pars>();
    public string difficult;
    public string kcal100gr;
    public bool newest;
    public string dish_url;
    public bool GetParsContains(string val)
    {
       foreach(var item in pars)
        {
            if (item.value == val)
            return true;
        }
        return false;
    }
}

[Serializable]

public class Pars
{
    public string value;
}

[Serializable]
public class Reviews
{
    public string subject;
    public string text;
    public string rating;
    public string uid;
    public string name;
    public string surname;
    public string url;
    public string review_id;
    public string review_rating;
    public string review_text;
    public string review_user_name;

    public Reviews(string _subject, string _review_id, string _review_rating,
        string _review_text, string _name, string _surname, string _url, string _review_user_name)
    {
        this.subject = _subject;
        this.review_id = _review_id;
        this.review_rating = _review_rating;
        this.review_text = _review_text;
        this.name = _name;
        this.surname = _surname;
        this.url = _url;
        this.review_user_name = _review_user_name;
    }

    public Reviews() { }
}

[Serializable]
public class Cooking
{
    public string target_id;
    public string alt;
    public string title;
    public string url;
}

[Serializable]
public class Description
{

}

[Serializable]
public class Ing
{
    public string target_id;
}

[Serializable]
public class Img
{
    public string target_id;
    public string alt;
    public string title;
    public string url;
}


