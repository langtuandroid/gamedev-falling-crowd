using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class translateLanguages : MonoBehaviour
{
    public string rus;
    //public string UK;
    //public string Canada;
    public string Oman;
    public string France;
    public string Vietnam;
    //public string Egypt;
    public string Brazil;
    //public string Algeria;


    /*public string spain;
    public string thai;
    public string india;*/

    public bool canvass;
    // Start is called before the first frame update
    void Awake()
    {
        var lang = Application.systemLanguage;
        string newtext = "";


        if (lang == SystemLanguage.Russian && rus != "") newtext = rus;
        //if (lang == SystemLanguage.Russian && UK != "") newtext = UK;
        //if (lang == SystemLanguage.Russian && Canada != "") newtext = Canada;
        if (lang == SystemLanguage.Arabic && Oman != "") newtext = Oman;
        if (lang == SystemLanguage.French && France != "") newtext = France;
        if (lang == SystemLanguage.Vietnamese && Vietnam != "") newtext = Vietnam;
        //if (lang == SystemLanguage.Russian && Egypt != "") newtext = Egypt;
        if (lang == SystemLanguage.Portuguese && Brazil != "") newtext = Brazil;
        //if (lang == SystemLanguage.Russian && Algeria != "") newtext = Algeria;


        if (newtext != "")
        {
            if (canvass == false)
            {
                GetComponent<TextMesh>().text = newtext;

                var TextScript = gameObject.GetComponent<TextMesh>();
                var texts = TextScript.text.Replace("\\n","\n");
                texts = TextScript.text.Replace("*","\n");
                TextScript.text = texts;
            }
            else
            {
                GetComponent<Text>().text = newtext;

                var TextScript = gameObject.GetComponent<Text>();
                var texts = TextScript.text.Replace("\\n","\n");
                texts = TextScript.text.Replace("*","\n");
                TextScript.text = texts;
            }
        }
    }
}
