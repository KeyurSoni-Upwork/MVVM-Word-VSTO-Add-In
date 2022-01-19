using MemoForms.ViewModel;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Office = Microsoft.Office.Core;

namespace MemoSaver
{
    public partial class MemoAddIn
    {
        MemoRibbon ribbon;
        protected override object RequestService(Guid serviceGuid)
        {
            if (serviceGuid == typeof(Office.IRibbonExtensibility).GUID)
            {
                if (ribbon == null)
                    ribbon = new MemoRibbon();
                return ribbon;
            }

            return base.RequestService(serviceGuid);
        }
    }

    [ComVisible(true)]
    public class MemoRibbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;

        public MemoRibbon()
        {
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("MemoSaver.MemoRibbon.xml");
        }

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit https://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        /// <summary>
        /// Open Memo Saving Dialog on "Add to Note" button click
        /// </summary>
        /// <param name="control"></param>
        public void OnSaveMemoClicked(Office.IRibbonControl control)
        {
            string textFromDoc = Globals.MemoAddIn.Application.Selection.Text;
            // If you need to store file path use this code
            //string filePath = Globals.MemoAddIn.Application.ActiveDocument.Path;
            Microsoft.Office.Interop.Word.Paragraphs paraSelected = Globals.MemoAddIn.Application.Selection.Paragraphs;

            // Launches the WPF memo saving window
            var memoWindowApp = new MemoForms.MainWindow();
            memoWindowApp.DataContext = new MemoViewModel();
            ((MemoViewModel)memoWindowApp.DataContext).TxtBoxContent = textFromDoc;
            ((MemoViewModel)memoWindowApp.DataContext).ParagraphText = paraSelected.ToString();
            ((MemoViewModel)memoWindowApp.DataContext).OriginalSelectedText = textFromDoc;
            ((MemoViewModel)memoWindowApp.DataContext).TxtBoxFile_id = 0;
            ((MemoViewModel)memoWindowApp.DataContext).SectionNumber = "Dummy section numbers";
            memoWindowApp.Show();
        }

        #endregion

        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
