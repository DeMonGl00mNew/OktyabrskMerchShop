using System.Collections;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//������, ���������� �� ������ ��������
public class ShopLogic : MonoBehaviour
{
    static public ShopLogic Instance { get; private set; }
   [HideInInspector] public string stringForFavoritesList = "";

    public GameObject FavoriteItem;
    public Transform IzbrannoePanel;
    [Header("����� ������")]
    public int SaleTime = 60;
    [Header("�������� �������")]
    public Sprite[] view2D;
    public GameObject[] view3D;
    public string[] title;
    public int[] price;
    public int[] sale;
    [Header("����� �� �����")]
    public Image View2DInScene;
    public TMP_Text TitleInScene;
    public TMP_Text PriceInScene;
    public GameObject SaleImage;

    private int index = 0;
    private ItemInFavorites[] ListFavorites;
    // ������� Singletone ��� ������������� ������������
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    // ������������� ������, ����������� ������ ������ � ����������� ����� � ��������
    void Start()
    {
        sale = new int[price.Length];
        StartCoroutine(SaleTimer(SaleTime));
        ViewInShop(index);

    }
    //����������� ��� ����������� ������� ������
    IEnumerator SaleTimer(int second)
    {
        while (true)
        {
            int randomNumber = Random.Range(0, sale.Length);
            Array.Clear(sale, 0, sale.Length);

            sale[randomNumber] = 1;

            if (index == randomNumber)
                SaleImage.SetActive(true);
                yield return new WaitForSeconds(second);
            if (index == randomNumber) 
            {
                SaleImage.SetActive(false);
                BuyPredmet.StoymostVeshi = price[index];
                PriceInScene.text = "���������: " + BuyPredmet.StoymostVeshi;
            }
                
        }


    }
    // ������� ����������� ����� � �������� 2d ��������
    public void ViewInShop(int i)
    {
        View2DInScene.sprite = view2D[i];
        Activate3DItem(i);
        TitleInScene.text = title[i];

        if (sale[index] >0)
        {
            BuyPredmet.StoymostVeshi = price[i] / 2;
            SaleImage.SetActive(true);
        }

        else
        {
            SaleImage.SetActive(false);
            BuyPredmet.StoymostVeshi = price[i];
        }

        PriceInScene.text = "���������: " + BuyPredmet.StoymostVeshi;
    }
    // ������� ����������� ����� � �������� 3d ��������
    private void Activate3DItem(int index)
    {
        for (int i = 0; i < view3D.Length; i++)
        {
            if (i == index)
                view3D[i].SetActive(true);
            else
                view3D[i].SetActive(false);
        }
    }
    //������� ��������� � ����������� ��������� ���� ��� ������� �� ������ ���������
    public void nextItem()
    {
        index = Mathf.Clamp(++index, 0, 5);
        ViewInShop(index);
    }
    //������� ��������� � ����������� ���������� ���� ��� ������� �� ������ ����������
    public void previouseItem()
    {
        index = Mathf.Clamp(--index, 0, 5);
        ViewInShop(index);
    }
    //������� ��������� ���������� � ����������� ��������� �����
    public void ToFavorites()
    {

        ListFavorites = IzbrannoePanel.GetComponentsInChildren<ItemInFavorites>();

        foreach (var item in ListFavorites)
        {
            if (item.index == index)
                return;
        }

        ItemInFavorites itemInFavorites = Instantiate(FavoriteItem, IzbrannoePanel).GetComponent<ItemInFavorites>();
        itemInFavorites.index = index;
        itemInFavorites.GetComponent<Image>().sprite = view2D[index];
        SavedFavorites();
    }
    // ���������� ��������� �����
    public void SavedFavorites()
    {
        stringForFavoritesList = "";

        ListFavorites = IzbrannoePanel.GetComponentsInChildren<ItemInFavorites>();

        if (ListFavorites.Length != 0)
        {

            foreach (var item in ListFavorites)
            {
                stringForFavoritesList += item.index.ToString();

            }
        }
        PlayerPrefs.SetString("SavedList", stringForFavoritesList);
        PlayerPrefs.Save();
    }
    // ������� �������� �� ������� � �����
    private int CharToInt(char simbol)
    {
        return (int)char.GetNumericValue(simbol);
    }
    // ������� �������� ��������� �����
    public void LoadFavorites()
    {
        if (!PlayerPrefs.HasKey("SavedList"))
            return;

        stringForFavoritesList = PlayerPrefs.GetString("SavedList");

        if (stringForFavoritesList == "")
            return;
        // ������� �������� 3d �������� � 2d ��������
        Activate3DItem(CharToInt(stringForFavoritesList[0]));
        foreach (char iChar in stringForFavoritesList)
        {
            ItemInFavorites itemInFavorites = Instantiate(FavoriteItem, IzbrannoePanel).GetComponent<ItemInFavorites>();
            itemInFavorites.index = CharToInt(iChar);
            itemInFavorites.GetComponent<Image>().sprite = view2D[CharToInt(iChar)];
        }

    }
}
