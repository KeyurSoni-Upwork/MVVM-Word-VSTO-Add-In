using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MemoForms.Models
{
    /// <summary>
    /// Model class to store memo data
    /// </summary>
    public class Memo : INotifyPropertyChanged
    {
        private int _File_id;

        /// <summary>
        /// Property to store id of file
        /// </summary>
        public int File_id
        {
            get { return _File_id; }
            set { _File_id = value; OnPropertyChanged("File_id"); }
        }

        private string _SectionNumber;

        /// <summary>
        /// Property to store section number of document
        /// </summary>
        public string SectionNumber
        {
            get { return _SectionNumber; }
            set { _SectionNumber = value; OnPropertyChanged("SectionNumber"); }
        }

        private string _ParagraphText;

        /// <summary>
        /// Property to store text of paragraph from which content is selected
        /// </summary>
        public string Paragraphtext
        {
            get { return _ParagraphText; }
            set { _ParagraphText = value; OnPropertyChanged("ParagraphText"); }
        }

        private string _OriginalSelectedText;

        /// <summary>
        /// The original text from document which is selected as memo content
        /// </summary>
        public string OriginalSelectedText
        {
            get { return _OriginalSelectedText; }
            set { _OriginalSelectedText = value; OnPropertyChanged("OriginalSelectedText"); }
        }

        private string _Content;

        /// <summary>
        /// Initially it is selected from the word document, but could be modified to store as memo content
        /// </summary>
        public string Content
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged("Content"); }
        }

        private string _MemoText;

        /// <summary>
        /// Short description(Memo) about content selected and stored from the document 
        /// </summary>
        public string MemoText
        {
            get { return _MemoText; }
            set { _MemoText = value; OnPropertyChanged("MemoText"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
