using UnityEngine;

/*скрипт работы логики избранной вещи.*/
public class ItemInFavorites : MonoBehaviour
{
     public int index =-1;
    // вызов функции сохранения избранного при выходе из приложения,
    // данные сохраняются на устройстве
    private void OnDestroy()
    {
        ShopLogic.Instance.SavedFavorites();
    }
    // уничтожение избранных вещей
    public void OutFavorites()
    {
        Destroy(gameObject);
    }

    //обработка нажатия на избранное
    public void ClickOnFavorite()
    {
        ShopLogic.Instance.ViewInShop(index);
    }

}
