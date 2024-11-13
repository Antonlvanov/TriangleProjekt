using System.Xml;
using _triangle = Triangle.TriangleObject.Triangle;

namespace Triangle.Interface
{
    public class DataManager()
    {
        public void DisplayTriangleInfo(_triangle triangle, ListView listView)
        {
            if (triangle == null || listView == null)
            {
                MessageBox.Show("No triangle data available", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string[] fieldNames = { "Külg A", "Külg B", "Külg C", "Nurk AB", "Nurk BC", "Nurk AC", "Kõrgus A", "Kõrgus B", "Kõrgus C", "Perimeter", "Pindala", "Tüüp" };
            listView.Items.Clear();
            listView.Items.Add(new ListViewItem(new[] { "Külg A", triangle.outputA() }));
            listView.Items.Add(new ListViewItem(new[] { "Külg B", triangle.outputB() }));
            listView.Items.Add(new ListViewItem(new[] { "Külg C", triangle.outputC() }));
            listView.Items.Add(new ListViewItem(new[] { "Nurk AB", triangle.AngleAB.ToString("F2") + "°" }));
            listView.Items.Add(new ListViewItem(new[] { "Nurk BC", triangle.AngleBC.ToString("F2") + "°" }));
            listView.Items.Add(new ListViewItem(new[] { "Nurk AC", triangle.AngleAC.ToString("F2") + "°" }));
            listView.Items.Add(new ListViewItem(new[] { "Kõrgus A", triangle.heightA.ToString() }));
            listView.Items.Add(new ListViewItem(new[] { "Kõrgus B", triangle.heightB.ToString() }));
            listView.Items.Add(new ListViewItem(new[] { "Kõrgus C", triangle.heightC.ToString() }));
            listView.Items.Add(new ListViewItem(new[] { "Perimeter", triangle.outputP() }));
            listView.Items.Add(new ListViewItem(new[] { "Pindala", triangle.outputS() }));
            listView.Items.Add(new ListViewItem(new[] { "Tüüp", triangle.TypeEst() }));

        }

        public void SaveDataXML(_triangle triangle)
        {
            if (triangle == null)
            {
                MessageBox.Show("No triangle data to save.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string filePath = @"..\..\..\kolmnurgad.xml";

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

                string[] fieldNames = { "Side_A", "Side_B", "Side_C", "Angle_AB", "Angle_BC", "Angle_AC", "Height_A", "Height_B", "Height_C", "Perimeter", "Surface", "Type" };
                object[] fieldValues =
                {
                    triangle.outputA(),
                    triangle.outputB(),
                    triangle.outputC(),
                    triangle.AngleAB.ToString(),
                    triangle.AngleBC.ToString(),
                    triangle.AngleAC.ToString(),
                    triangle.heightA.ToString(),
                    triangle.heightB.ToString(),
                    triangle.heightC.ToString(),
                    triangle.outputP(),
                    triangle.outputS(),
                    triangle.Type
                };



                for (int i = 0; i < fieldValues.Length; i++)
                {
                    try
                    {
                        string fieldValue = fieldValues[i]?.ToString();
                        XmlElement fieldElement = doc.CreateElement(fieldNames[i]);
                        fieldElement.InnerText = System.Web.HttpUtility.HtmlEncode(fieldValue);
                        triangleElem.AppendChild(fieldElement);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error while writing field {fieldNames[i]}: {ex.Message}");
                    }
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
