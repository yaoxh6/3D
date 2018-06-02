using UnityEngine;

// 必须是静态类才可以添加扩展方法
public static class ExtensionMethod
{
    // 扩展方法必须是静态的
    // this 必须有，RectTransform表示我要扩展的类型，rTrans表示对象名
    // 如果要带参数就在后面带上
    public static void Left(this RectTransform rTrans, int value)
    {
        rTrans.offsetMin = new Vector2(value, rTrans.offsetMin.y);
    }

    public static void Right(this RectTransform rTrans, int value)
    {
        rTrans.offsetMax = new Vector2(-value, rTrans.offsetMax.y);
    }

    public static void Bottom(this RectTransform rTrans, int value)
    {
        rTrans.offsetMin = new Vector2(rTrans.offsetMin.x, value);
    }

    public static void Top(this RectTransform rTrans, int value)
    {
        rTrans.offsetMax = new Vector2(rTrans.offsetMax.x, -value);
    }
}