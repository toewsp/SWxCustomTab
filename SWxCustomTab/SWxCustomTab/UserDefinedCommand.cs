using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using SWxCustomTab.Shared;
using SolidWorks.Interop.swconst;
using System.Xml.Serialization;

namespace SWxCustomTab
{
    public class UserDefinedCommand
    {

        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Browsable(false)]
        public string ShowInDocType { get; set; }
        public int ImageIndex { get; set; }
        public string Filename { get; set; }
        public string ModuleName { get; set; }
        public string FunctionName { get; set; }
        public bool WaitForExit { get; set; }

        public string Arguments { get; set; }
        public string CommandType { get; set; }

        private SWxCustomTab Parent;

        [XmlIgnore()]
        public EnumShowInDocType DocType
        {
            get
            {

                switch (ShowInDocType.ToLower())
                {

                    case "part": return EnumShowInDocType.Teil;
                    case "assembly": return EnumShowInDocType.Baugruppe;
                    case "drawing": return EnumShowInDocType.Zeichnung;
                    default: return EnumShowInDocType.Ohne;

                }

            }
            set
            {

                switch (value)
                {

                    case EnumShowInDocType.Teil: ShowInDocType = "Part"; break;
                    case EnumShowInDocType.Baugruppe: ShowInDocType = "Assembly"; break;
                    case EnumShowInDocType.Zeichnung: ShowInDocType = "Drawing"; break;
                    case EnumShowInDocType.Ohne: ShowInDocType = "none"; break;
                    default: ShowInDocType = "none"; break;

                }

            }
        }

        public UserDefinedCommand()
        {

        }

        public UserDefinedCommand(UserDefinedCommand copyFrom)
        {

            foreach (PropertyInfo pi in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {

                pi.SetValue(this, pi.GetValue(copyFrom));

            }

            this.Parent = copyFrom.Parent;

        }

        public void SetParent(object parentClass)
        {

            this.Parent = parentClass as SWxCustomTab;

        }

        public void Run()
        {

            if (Parent is null) return;

            try
            {
                switch (this.CommandType.ToLower())
                {
                    case "macro":

                        int errors = 0;
                        bool macroRunSuccess = this.Parent.swApp.RunMacro2(this.Filename, this.ModuleName, this.FunctionName, (int)swRunMacroOption_e.swRunMacroUnloadAfterRun, out errors);

                        break;

                    case "process":

                        ProcessStartInfo psi = new ProcessStartInfo(this.Filename, this.Arguments);

                        Process p = Process.Start(psi);
                        if (this.WaitForExit) p.WaitForExit();

                        break;

                    case "dll":

                        List<object> actions = new List<object>();
                        Assembly myDll = Assembly.LoadFrom(this.Filename);
                        Type[] types = myDll.GetTypes();

                        for (int i = 0; i < types.Length; i++)
                        {
                            Type type = myDll.GetType(types[i].FullName);
                            if (type.GetInterface(typeof(IUserDefinedDLL).FullName) != null)
                            {

                                IUserDefinedDLL obj = Activator.CreateInstance(type) as IUserDefinedDLL;
                                if (obj != null)
                                {

                                    obj.swApp = Parent.swApp;
                                    obj.Start();

                                    GC.Collect();
                                    GC.WaitForPendingFinalizers();
                                    GC.Collect();

                                }

                            }
                        }

                        break;

                    default: break;

                }

            }
            catch (Exception ex)
            {

                Parent.swApp.SendMsgToUser2(String.Format("Fehler bei der Ausführung von '{0}':\n", this.Name) + ex.Message, (int)swMessageBoxIcon_e.swMbStop, (int)swMessageBoxBtn_e.swMbOk);

            }

        }

    }
}
