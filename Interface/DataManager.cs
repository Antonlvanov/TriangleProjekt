using System.Xml;
using static Triangle.Window;

namespace Triangle.Interface
{
    public class DataManager(Context context)
    {
        private readonly Context c = context;

        public void DisplayTriangleInfo()
        {
            var triangle = c.Triangle;
            var listView = c.UI.TriangleInfoView;

            if (triangle == null || listView == null)
            {
                MessageBox.Show("No triangle data available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            listView.Items.Clear();
            listView.Items.Add(new ListViewItem(new[] { "Side A", triangle.a.ToString() }));
            listView.Items.Add(new ListViewItem(new[] { "Side B", triangle.b.ToString() }));
            listView.Items.Add(new ListViewItem(new[] { "Side C", triangle.c.ToString() }));
            listView.Items.Add(new ListViewItem(new[] { "Angle AB", $"{triangle._angleAB:F2}°" }));
            listView.Items.Add(new ListViewItem(new[] { "Angle BC", $"{triangle._angleBC:F2}°" }));
            listView.Items.Add(new ListViewItem(new[] { "Angle AC", $"{triangle._angleAC:F2}°" }));
            listView.Items.Add(new ListViewItem(new[] { "Perimeter", $"{triangle._perimeter:F2}" }));
            listView.Items.Add(new ListViewItem(new[] { "Surface", $"{triangle._surface:F2}" }));
            listView.Items.Add(new ListViewItem(new[] { "Type", triangle.TypeRusky() }));
        }

        public void SaveDataXML()
        {
            var triangle = c.Triangle;
            if (triangle == null)
            {
                MessageBox.Show("No triangle data to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string filePath = @"..\..\..\data.xml";
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                NewLineOnAttributes = false
            };

            try
            {
                XmlDocument doc = new XmlDocument();
                if (!File.Exists(filePath))
                {
                    using (XmlWriter writer = XmlWriter.Create(filePath, settings))
                    {
                        writer.WriteStartDocument();
                        writer.WriteStartElement("Triangles");
                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                    }
                }
                doc.Load(filePath);

                //index
                int index = doc.DocumentElement.SelectNodes("Triangle").Count + 1;
                XmlElement triangleElem = doc.CreateElement("Triangle");
                triangleElem.SetAttribute("Index", index.ToString());
                //date time 
                XmlElement dateTimeElem = doc.CreateElement("DateTime");
                dateTimeElem.InnerText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                triangleElem.AppendChild(dateTimeElem);

                string[] fieldNames = { "SideA", "SideB", "SideC", "AngleAB", "AngleBC", "AngleAC", "Perimeter", "Surface", "Type" };
                object[] fieldValues =
                {
                    triangle.a,
                    triangle.b,
                    triangle.c,
                    triangle._angleAB,
                    triangle._angleBC,
                    triangle._angleAC,
                    triangle._perimeter,
                    triangle._surface,
                    triangle.Type
                };

                for (int i = 0; i < fieldValues.Length; i++)
                {
                    XmlElement fieldElement = doc.CreateElement(fieldNames[i]);
                    fieldElement.InnerText = fieldValues[i]?.ToString();
                    triangleElem.AppendChild(fieldElement);
                }

                doc.DocumentElement.AppendChild(triangleElem);
                doc.Save(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
