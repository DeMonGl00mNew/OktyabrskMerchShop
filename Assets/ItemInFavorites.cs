using UnityEngine;

/*������ ������ ������ ��������� ����.*/
public class ItemInFavorites : MonoBehaviour
{
     public int index =-1;
    // ����� ������� ���������� ���������� ��� ������ �� ����������,
    // ������ ����������� �� ����������
    private void OnDestroy()
    {
        ShopLogic.Instance.SavedFavorites();
    }
    // ����������� ��������� �����
    public void OutFavorites()
    {
        Destroy(gameObject);
    }

    //��������� ������� �� ���������
    public void ClickOnFavorite()
    {
        ShopLogic.Instance.ViewInShop(index);
    }

}
