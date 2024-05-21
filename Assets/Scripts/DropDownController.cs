using UnityEngine;
using UnityEngine.UI;

public class DropDownController : MonoBehaviour
{
    public GameObject dropdown;
    public GameObject optionPrefab;
    public Sprite[] icons;

    void Start()
    {
        //for (int i = 0; i < 3; i++)
        //{
        //    GameObject option = Instantiate(optionPrefab);
        //    option.transform.SetParent(dropdown.transform);
        //    option.transform.localScale = Vector3.one;
        //    option.transform.localPosition = new Vector3(0, -i * 50, 0);

        //    Image icon = option.transform.Find("Icon").GetComponent<Image>();
        //    icon.sprite = icons[i];

        //    Text label = option.transform.Find("Label").GetComponent<Text>();
        //    label.text = "Option " + (i + 1);

        //    GameObject submenu = option.transform.Find("Submenu").gameObject;
        //    for (int j = 0; j < 3; j++)
        //    {
        //        GameObject subOption = Instantiate(optionPrefab);
        //        subOption.transform.SetParent(submenu.transform);
        //        subOption.transform.localScale = Vector3.one;
        //        subOption.transform.localPosition = new Vector3(0, -j * 50, 0);

        //        Image subIcon = subOption.transform.Find("Icon").GetComponent<Image>();
        //        subIcon.sprite = icons[j];

        //        Text subLabel = subOption.transform.Find("Label").GetComponent<Text>();
        //        subLabel.text = "Sub Option " + (j + 1);

        //        InputField inputField = subOption.transform.Find("InputField").GetComponent<InputField>();
        //        inputField.text = "Enter text here";
        //    }
        //}
    }

    void MyDropDown()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject option = Instantiate(optionPrefab);
            option.transform.SetParent(dropdown.transform);
            option.transform.localScale = Vector3.one;
            option.transform.localPosition = new Vector3(0, -i * 50, 0);

            Image icon = option.transform.Find("Icon").GetComponent<Image>();
            icon.sprite = icons[i];

            Text label = option.transform.Find("Label").GetComponent<Text>();
            label.text = "Option " + (i + 1);

            GameObject submenu = option.transform.Find("Submenu").gameObject;
            for (int j = 0; j < 3; j++)
            {
                GameObject subOption = Instantiate(optionPrefab);
                subOption.transform.SetParent(submenu.transform);
                subOption.transform.localScale = Vector3.one;
                subOption.transform.localPosition = new Vector3(0, -j * 50, 0);

                Image subIcon = subOption.transform.Find("Icon").GetComponent<Image>();
                subIcon.sprite = icons[j];

                Text subLabel = subOption.transform.Find("Label").GetComponent<Text>();
                subLabel.text = "Sub Option " + (j + 1);

                InputField inputField = subOption.transform.Find("InputField").GetComponent<InputField>();
                inputField.text = "Enter text here";
            }
        }
    }
}