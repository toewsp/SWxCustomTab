using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

namespace SWxCustomTab
{
    public class UserDefinedCommandManager
    {
        
        public SWxCustomTab ParentClass { get; set; }
        private List<UserDefinedCommand> CommandsLoadedFromXML { get; set; }
        public List<UserDefinedCommand> Commands { get; set; }

        private const string xmlFilename = "UserDefinedCommands.xml";

        public List<UserDefinedCommand> GetCommandsForDocType(int documentType)
        {

            string searchString = String.Empty;
            switch (documentType)
            {
                case 1: searchString = "Part"; break;
                case 2: searchString = "Assembly"; break;
                case 3: searchString = "Drawing"; break;
                default:
                    searchString = "none";
                    break;
            }

            return this.Commands.FindAll(x => x.ShowInDocType.ToLower() == searchString.ToLower()).ToList();

        }
        public UserDefinedCommandManager(SWxCustomTab parentClass)
        {

            this.ParentClass = parentClass;
            this.Commands = new List<UserDefinedCommand>();

        }

        public bool Initialize()
        {

            this.CommandsLoadedFromXML = CreateListFromXML();
            this.Commands = CreateListFromXML();

            if (CommandsLoadedFromXML == null || Commands == null) return false;

            return true;

        }

        public List<UserDefinedCommand> CreateListFromXML()
        {

            List<UserDefinedCommand> list = new List<UserDefinedCommand>();

            try
            {

                XmlSerializer serializer = new XmlSerializer(typeof(List<UserDefinedCommand>));

                FileStream fs = new FileStream(System.IO.Path.Combine(Helpers.AssemblyDirectory , xmlFilename), FileMode.Open, FileAccess.Read, FileShare.Read);
                TextReader reader = new StreamReader(fs);

                list = (List<UserDefinedCommand>)serializer.Deserialize(reader);

                reader.Close();

            }
            catch (Exception)
            {

                //System.Windows.Forms.MessageBox.Show("Exception:\n\n" + ex.Message.ToString());
                return null;

            }

            return list;

        }

        public void DiscardChanges()
        {

            this.Commands = new List<UserDefinedCommand>();
            this.CommandsLoadedFromXML.ForEach(x => this.Commands.Add(new UserDefinedCommand(x)));

        }

        public void AcceptChanges()
        {

            if (SaveXML())
            {

                ParentClass.RemoveCommands();
                ParentClass.CreateCommands();

                if (CommandsLoadedFromXML.Count != Commands.Count) ParentClass.swApp.SendMsgToUser2("Die Anzahl der Schaltflächen wurde geändert. Ein Neustart von SolidWorks wird empfohlen.", 2, 2);
            }

        }
        public bool SaveXML()
        {

            try
            {

                string tempFile = System.IO.Path.GetTempFileName();
                string xmlFile = System.IO.Path.Combine(Helpers.AssemblyDirectory, xmlFilename);

                XmlSerializer serializer = new XmlSerializer(typeof(List<UserDefinedCommand>));

                FileStream fs = new FileStream(tempFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
                TextWriter writer = new StreamWriter(fs);

                serializer.Serialize(fs, this.Commands);

                writer.Close();

                System.IO.File.Delete(xmlFile);
                System.IO.File.Move(tempFile, xmlFile);

            }
            catch (Exception)
            {

                System.Windows.Forms.MessageBox.Show("Beim Speichern der XML-Datei ist ein Fehler aufgetreten.\nBitte stellen Sie sicher, dass die Datei beschrieben werden darf.", "Einstellungen speichern", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return false;

            }

            return true;

        }
    }
}
