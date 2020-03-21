using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class ExtendMetods
{
    public static Sprite FastCreate(this Sprite _sprite, Texture2D texture)
    {
       _sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        return _sprite;
    }
    
    public static async Task ScaleStepTo(this Transform _vectorSelf, Vector3 targetVector, int steps)
    {
        if (steps == 0) return;
        var dx = (targetVector.x - _vectorSelf.localScale.x) / steps;
        var dy = (targetVector.y - _vectorSelf.localScale.y) / steps;
        var dz = (targetVector.z - _vectorSelf.localScale.z) / steps;
        for (int i = 0; i < steps; i++)
        {
            _vectorSelf.localScale += new Vector3(dx, dy, dz);
         //   targetObj.localScale = _vectorSelf;
            await new WaitForUpdate();
        }
        return;
    }
    public static async Task ColorStepTo(this Graphic _colorSelf, Color targetColor, int steps)
    {
        if (steps == 0) return;
        var da = (targetColor.a - _colorSelf.color.a) / (float)steps;
        var db = (targetColor.b - _colorSelf.color.b) / (float)steps;
        var dg = (targetColor.g - _colorSelf.color.g) / (float)steps;
        var dr = (targetColor.r - _colorSelf.color.r) / (float)steps;
        for (int i = 0; i < steps; i++)
        {
            _colorSelf.color += new Color(dr,dg,db,da);
            await new WaitForUpdate();
        }
        return;
    }
}

[Serializable]
public class UserToken
{
    public string access_token;
    //public User user;
    public string message;
}

[Serializable]
public class Message
{
    public string message;
}

