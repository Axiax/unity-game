using System;

namespace Assets.Script.Editor
{
    class XmlController
    {
        public string Filename { get; set; }
        public string Role { get; set; }
        public string Act { get; set; }
        public string Attr { get; set; }

        private XmlLoader xmlLoader = new XmlLoader();

        public float GetFloatXml(string act, string attr)
        {
            return (float) Convert.ToDouble(xmlLoader.LoadAct(Filename, Role, act, attr));
        }
    }
}
