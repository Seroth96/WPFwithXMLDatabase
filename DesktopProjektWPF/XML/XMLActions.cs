using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DesktopProjektWPF.Model;

namespace DesktopProjektWPF.XML
{
    public static class XMLActions
    {
        public static string Filename { get; set; } = "test.xml";
        public static ObservableCollection<Kierunek> Read(string filename = "")
        {
            if (filename == "")
            {
                filename = Filename;
            }
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Kierunek>));
            FileStream fs = new FileStream(filename, FileMode.Open);
            ObservableCollection<Kierunek> kierunk = new ObservableCollection<Kierunek>();
            try
            {
                kierunk = (ObservableCollection<Kierunek>)serializer.Deserialize(fs);
            }
            catch
            {

            }
            fs.Close();
            return kierunk;
        }

        public static void Save(ObservableCollection<Kierunek> kierunek, string filename = "")
        {
            if (filename == "")
            {
                filename = Filename;
            }
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Kierunek>));
            TextWriter writer = new StreamWriter(filename);
            serializer.Serialize(writer, kierunek);
            writer.Close();
        }
    }
}
