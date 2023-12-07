using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Microsoft.Office.Interop.Word;

namespace RRHH5.Pages.Test
{
    public partial class WORD1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            string selectedParagraph = ddlParagraph.SelectedValue;
            string customText = txtCustomText.Text;
            string fileName = txtFileName.Text; // Obtén el nombre del archivo

            // Crea una instancia de Word
            Application wordApp = new Application();
            Document doc = wordApp.Documents.Add();

            // Agrega el contenido al documento
            Paragraph para = doc.Content.Paragraphs.Add(Type.Missing);
             
            para.Range.Text = GetParagraphText(selectedParagraph) + " " + customText;

            string fullText = GetParagraphText(selectedParagraph) + " " + customText;
            int startIndex = fullText.IndexOf("SHARONN ELIZABETH TOC PACHECO");

            para.Range.Text = fullText;
            if (startIndex != -1)
            {
                Range boldRange = para.Range.Duplicate;
                boldRange.SetRange(startIndex, startIndex + "SHARONN ELIZABETH TOC PACHECO".Length);
                boldRange.Bold = 1; // Aplicar formato negrita
            }

            // Guarda el documento con el nombre especificado
            string tempFilePath = Path.Combine(Path.GetTempPath(), fileName + ".docx");
            doc.SaveAs(tempFilePath);
            doc.Close();
            wordApp.Quit();

            // Descarga el archivo
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".docx");
            Response.WriteFile(tempFilePath);
            Response.Flush();
            Response.End();
        }

        private string GetParagraphText(string paragraphKey)
        {
            // Aquí puedes definir los textos de los párrafos según la clave
            if (paragraphKey == "Parrafo1")
            {
                return "Nosotros: Por una parte, SHARONN ELIZABETH TOC PACHECO de treinta y un (31) años, soltera, guatemalteca, Licenciada en Administración de Empresas, sexo femenino, vecino del municipio Guatemala, con domicilio en el departamento de Guatemala, me identifico con el Documento Personal de Identificación (DPI) con el Código Único de Identificación (CUI) dos mil ciento veintidós; trece mil doscientos ochenta y ocho; cero novecientos uno (2122 13288 0901), extendido por el Registro Nacional de las Personas de la República de Guatemala, comparezco en mi calidad de ADMINISTRADOR ÚNICO Y REPRESENTANTE LEGAL de la entidad EXPORTADORA ENLASA, SOCIEDAD ANÓNIMA, con";
            }
            else if (paragraphKey == "Parrafo2")
            {
                return "Este es el párrafo 2:";
            }
            // Agrega más condiciones si es necesario para otros párrafos
            return string.Empty;
        }
    }
}