using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UnityEngine;
/**************************
* 문자열과 일치하는 Xml을 로드한다.
* 지정된 위치에 오브젝트를 추가한다.
* 성능이 걱정돼서 게임플레이에서 사용하지 않을 예정
* 하지만 개발 단계에서 다른 인원들이 XML로 레벨디자인을 해서 건네주면 그대로 적용가능함
* 때문에 그때그때 받아서 오브젝트를 로드하는 방식으로 사용할 예정
 *************************/
public class XmlManager : MonoBehaviour
{
    public XmlDocument LoadXml(string XmlName)
    {
        if (XmlName != null)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(PathMaker(XmlName));
            Debug.Log("XmlName: " + XmlDoc.Name + " is succesfully Load!");
            return XmlDoc;
        }
        else
        {
            Debug.Log("Xml Load Fail!");
            return null;
        }
    }
    private string PathMaker(string XmlName) //Xml위치로 상대주소 만들어줌
    {
        StringBuilder XmlPath = new StringBuilder();
        XmlPath.Append("Assets\\Script\\xml_object\\");
        XmlPath.Append(XmlName);
        XmlPath.Append(".xml");
        return XmlPath.ToString();
    }
    private void xml_parsing(XmlDocument XmlDoc)
    {
        XmlNodeList PrefabNodes = XmlDoc.SelectNodes("//Prefab");
        foreach (XmlNode prefabnode in PrefabNodes)
        {
            string type = prefabnode.SelectSingleNode("Type").InnerText;
            GameObject prefab = Resources.Load<GameObject>("Prefab\\" + type);
            XmlNodeList posnodes = prefabnode.SelectNodes("XY");
            foreach (XmlNode pos in posnodes)
            {
                Instantiate(prefab, new Vector3(int.Parse(pos.SelectSingleNode("X").InnerText), int.Parse(pos.SelectSingleNode("Y").InnerText), 0), Quaternion.identity);
            }
        }
    }
}