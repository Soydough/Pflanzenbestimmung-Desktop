using System;
using System.Windows.Controls;

namespace Pflanzenbestimmung_Desktop
{
    class PflanzenVorschauTemplate
    {
        public string Bild { get; set; }

        Button deleteButton;

        public PflanzenVorschauTemplate()
        {
            deleteButton = new Button();
        }

        private void deleteButton_click(object sender, EventArgs e)
        {

        }





    }
}
