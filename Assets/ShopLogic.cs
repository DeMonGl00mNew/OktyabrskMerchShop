using System.Collections;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

//скрипт, отвечающий за логику магазина
public class ShopLogic : MonoBehaviour
{
    static public ShopLogic Instance { get; private set; }
   [HideInInspector] public string stringForFavoritesList = "";

    public GameObject FavoriteItem;
    public Transform IzbrannoePanel;
    [Header("Время Скидки")]
    public int SaleTime = 60;
    [Header("Атрибуты товаров")]
    public Sprite[] view2D;
    public GameObject[] view3D;
    public string[] title;
    public int[] price;
    public int[] sale;
    [Header("Товар на сцене")]
    public Image View2DInScene;
    public TMP_Text TitleInScene;
    public TMP_Text PriceInScene;
    public GameObject SaleImage;

    private int index = 0;
    private ItemInFavorites[] ListFavorites;
    // паттерн Singletone для инициализации зависимостей
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
    // инициализация старта, организация логики скидок и отображения вещей в магащине
    void Start()
    {
        sale = new int[price.Length];
        StartCoroutine(SaleTimer(SaleTime));
        ViewInShop(index);

    }
    //сопрограмма для организации таймера скидок
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
                PriceInScene.text = "Стоимость: " + BuyPredmet.StoymostVeshi;
            }
                
        }


    }
    // функция отображения вещей в магазине 2d спратйов
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

        PriceInScene.text = "Стоимость: " + BuyPredmet.StoymostVeshi;
    }
    // функция отображения вещей в магазине 3d объектов
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
    //функция валидации и отображения следующей вещи при нажатии на кнопку следующая
    public void nextItem()
    {
        index = Mathf.Clamp(++index, 0, 5);
        ViewInShop(index);
    }
    //функция валидации и отображения предыдущей вещи при нажатии на кнопку предыдущая
    public void previouseItem()
    {
        index = Mathf.Clamp(--index, 0, 5);
        ViewInShop(index);
    }
    //функция занесения сохранения и отображения избранных вещей
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
    // сохранение избранных вещей
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
    // функция перевода из символа в целые
    private int CharToInt(char simbol)
    {
        return (int)char.GetNumericValue(simbol);
    }
    // функция загрузки избранных вещей
    public void LoadFavorites()
    {
        if (!PlayerPrefs.HasKey("SavedList"))
            return;

        stringForFavoritesList = PlayerPrefs.GetString("SavedList");

        if (stringForFavoritesList == "")
            return;
        // функция привязки 3d объектов и 2d спрайтов
        Activate3DItem(CharToInt(stringForFavoritesList[0]));
        foreach (char iChar in stringForFavoritesList)
        {
            ItemInFavorites itemInFavorites = Instantiate(FavoriteItem, IzbrannoePanel).GetComponent<ItemInFavorites>();
            itemInFavorites.index = CharToInt(iChar);
            itemInFavorites.GetComponent<Image>().sprite = view2D[CharToInt(iChar)];
        }

    }
}
