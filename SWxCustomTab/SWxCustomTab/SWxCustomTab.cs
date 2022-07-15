using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidWorks.Interop.swpublished;
using System.Diagnostics;

namespace SWxCustomTab
{
    
    [ComVisible(true)]
    [Guid("ffe65dbc-40eb-40cf-9fb6-0f260b93e61c")]
    [DisplayName("SWx Custom Tab")]
    [Description("Erzeugt eine benutzerdefinierte Toolbar in SolidWorks")]
    public class SWxCustomTab : SwAddin
    {

        #region Registration

        private const string ADDIN_KEY_TEMPLATE = @"SOFTWARE\SolidWorks\Addins\{{{0}}}";
        private const string ADDIN_STARTUP_KEY_TEMPLATE = @"Software\SolidWorks\AddInsStartup\{{{0}}}";
        private const string ADD_IN_TITLE_REG_KEY_NAME = "Title";
        private const string ADD_IN_DESCRIPTION_REG_KEY_NAME = "Description";

        [ComRegisterFunction]
        public static void RegisterFunction(Type t)
        {
            try
            {
                var addInTitle = "";
                var loadAtStartup = true;
                var addInDesc = "";

                var dispNameAtt = t.GetCustomAttributes(false).OfType<DisplayNameAttribute>().FirstOrDefault();

                if (dispNameAtt != null)
                {
                    addInTitle = dispNameAtt.DisplayName;
                }
                else
                {
                    addInTitle = t.ToString();
                }

                var descAtt = t.GetCustomAttributes(false).OfType<DescriptionAttribute>().FirstOrDefault();

                if (descAtt != null)
                {
                    addInDesc = descAtt.Description;
                }
                else
                {
                    addInDesc = t.ToString();
                }

                var addInkey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(String.Format(ADDIN_KEY_TEMPLATE, t.GUID));

                addInkey.SetValue(null, 0);

                addInkey.SetValue(ADD_IN_TITLE_REG_KEY_NAME, addInTitle);
                addInkey.SetValue(ADD_IN_DESCRIPTION_REG_KEY_NAME, addInDesc);

                var addInStartupkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(
                    string.Format(ADDIN_STARTUP_KEY_TEMPLATE, t.GUID));

                addInStartupkey.SetValue(null, Convert.ToInt32(loadAtStartup), Microsoft.Win32.RegistryValueKind.DWord);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Beim Registrieren des Add-Ins ist ein Fehler aufgetreten:\n\n" + ex.Message.ToString(), "Registrierung", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
            }
        }
        [ComUnregisterFunction]
        public static void UnregisterFunction(Type t)
        {

            try
            {

                Microsoft.Win32.Registry.LocalMachine.DeleteSubKey(String.Format(ADDIN_KEY_TEMPLATE, t.GUID));
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKey(String.Format(ADDIN_STARTUP_KEY_TEMPLATE, t.GUID));

            }
            catch (Exception ex)
            {

                MessageBox.Show("Beim Deregistrieren des Add-Ins ist ein Fehler aufgetreten:\n\n" + ex.Message.ToString(), "Deregistrierung", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

        }

        #endregion

        public SldWorks swApp;
        public int cookie;
        public ModelDoc2 swModelDoc;
        public Frame swFrame;

        public CommandManager CmdMgr;
        public CommandGroup CmdGrp = null;
        public CommandTab cmdTab = null;
        public CommandTabBox cmdBox = null;

        public List<CommandTab> CommandTabs = null;

        public UserDefinedCommandManager UserDefinedCommandManager;

        public bool ConnectToSW(object ThisSW, int Cookie)
        {


            try
            {
                
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

                this.swApp = ThisSW as SldWorks;
                this.cookie = Cookie;
                swApp.SetAddinCallbackInfo(0, this, cookie);

                swApp.AddMenu((int)swDocumentTypes_e.swDocNONE, "SWxCustomTab", 0);
                swApp.AddMenuItem5((int)swDocumentTypes_e.swDocNONE, this.cookie, "Einstellungen@SWxCustomTab", 0, "OpenSettings", "", "Öffnet die Einstellungen zu SWxCustomTab", new string[] { String.Empty });

                this.UserDefinedCommandManager = new UserDefinedCommandManager(this);
                if (!this.UserDefinedCommandManager.Initialize()) throw new Exception(String.Format("Die Datei 'UserDefinedCommands.xml' konnte nicht gelesen oder verarbeitet werden.", Helpers.AssemblyDirectory));
                this.UserDefinedCommandManager.Commands.ForEach(c => c.SetParent(this));

                CreateCommands();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Beim Laden des Add-Ins 'SWxCustomTab' ist ein Fehler aufgetreten:\n\n" + ex.Message.ToString(), "Add-In laden", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;

            }

            return true;

        }

        public bool DisconnectFromSW()
        {

            try
            {

                RemoveCommands();

                swApp.RemoveMenu((int)swDocumentTypes_e.swDocNONE, "SWxCustomTab", "");

                swApp.RemoveCallback(cookie);

                System.Runtime.InteropServices.Marshal.ReleaseComObject(swApp);
                swApp = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Beim Entladen des Add-Ins 'SWxCustomTab' ist ein Fehler aufgetreten:\n\n" + ex.Message.ToString(), "Add-In entaden", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;

            }

            return true;

        }

        public void RemoveCommands()
        {

            foreach (CommandTab tab in CommandTabs)
            {
                foreach (CommandTabBox tabBox in tab.CommandTabBoxes())
                {
                    tab.RemoveCommandTabBox(tabBox);
                }

                CmdMgr.RemoveCommandTab(tab);

            }

            CmdMgr.RemoveCommandGroup2(2022, true);
            CmdMgr = null;
            cmdTab = null;

        }
        public void OpenSettings()
        {

            UserDefinedCommandDesigner userDefinedCommandDesigner = new UserDefinedCommandDesigner(this);
            userDefinedCommandDesigner.ShowDialog();

        }

        public void CreateCommands()
        {

            string title = "SWx Custom Tab";
            string toolTip = "Benutzerdefinierte Toolsammlung";
            string hint = "Beinhaltet gesammelte Hilfsfunktionen und Makros";

            int errors = 0;
            CommandTabs = new List<CommandTab>();
            CmdMgr = swApp.GetCommandManager(cookie);
            CmdGrp = CmdMgr.CreateCommandGroup2(2022, title, toolTip, hint, -1, true, ref errors);

            int[] IconSizes = { 20, 32, 40, 64, 96, 128 };
            List<string> IconListPaths = new List<string>();
            List<string> MainIconListPaths = new List<string>();

            foreach (var iconSize in IconSizes)
            {

                var path = string.Format(Helpers.AssemblyDirectory + "\\SWxCustomTab_icons_{0}.png", iconSize);
                if (System.IO.File.Exists(path)) IconListPaths.Add(path);

                var pathMainIcon = string.Format(Helpers.AssemblyDirectory + "\\PTLogo_{0}.png", iconSize);
                if (System.IO.File.Exists(path)) MainIconListPaths.Add(pathMainIcon);

            }

            CmdGrp.IconList = IconListPaths.ToArray();
            CmdGrp.MainIconList = MainIconListPaths.ToArray();

            foreach (UserDefinedCommand cmd in this.UserDefinedCommandManager.Commands)
            {

                int index = this.UserDefinedCommandManager.Commands.IndexOf(cmd);
                CmdGrp.AddCommandItem2(cmd.Name, index, cmd.Description, cmd.Title, Convert.ToInt32(cmd.ImageIndex), String.Format("RunUserDefinedCommand({0})", index.ToString()), "", 0, (int)swCommandItemType_e.swToolbarItem);

            }

            CmdGrp.HasMenu = false;
            CmdGrp.HasToolbar = true;
            CmdGrp.Activate();

            // i = 1    swPart
            // i = 2    swAssembly
            // i = 3    swDocument
            for (int i = 1; i < 4; i++)
            {

                List<UserDefinedCommand> UDFsForThisDocType = this.UserDefinedCommandManager.GetCommandsForDocType(i);

                CommandTab ct = CmdMgr.GetCommandTab(i, title);
                if (ct != null) CmdMgr.RemoveCommandTab(ct);

                if (UDFsForThisDocType.Count > 0)
                {

                    cmdTab = CmdMgr.AddCommandTab(i, title);
                    CommandTabs.Add(cmdTab);

                    cmdBox = cmdTab.AddCommandTabBox();

                    int[] cmdIDs = new int[UDFsForThisDocType.Count()];
                    int[] TextType = new int[UDFsForThisDocType.Count()];

                    int j = 0;
                    foreach (UserDefinedCommand cmd in UDFsForThisDocType)
                    {
                        int index = this.UserDefinedCommandManager.Commands.IndexOf(cmd);
                        cmdIDs[j] = CmdGrp.get_CommandID(index);
                        TextType[j] = (int)swCommandTabButtonTextDisplay_e.swCommandTabButton_TextBelow;
                        j++;
                    }

                    cmdBox.AddCommands(cmdIDs, TextType);

                }

            }

        }

        public void RunUserDefinedCommand(string arg)
        {

            try
            {

                int index = Convert.ToInt32(arg);
                UserDefinedCommand cmd = this.UserDefinedCommandManager.Commands[index];

                cmd.Run();

            }
            catch (Exception)
            {

            }

        }

    }

}
