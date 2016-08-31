using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using System.IO;

namespace DocGenerator
{
    public class DocOperator
    {
        public DocOperator()
        {

        }

        public void GenerateElementDoc(Element element, string filename)
        {
            //Create new document
            Document doc = new Document();

            //Add section
            Section section = doc.AddSection();

            // Declare formatting for characters
            CharacterFormat headerFormat = new CharacterFormat(doc);
            headerFormat.Bold = true;
            headerFormat.FontSize = 16;
            headerFormat.FontName = "Times New Roman";

            CharacterFormat subSectionFormat = new CharacterFormat(doc);
            subSectionFormat.Italic = true;
            subSectionFormat.FontSize = 16;
            subSectionFormat.FontName = "Times New Roman";

            CharacterFormat paramTextFormat = new CharacterFormat(doc);
            paramTextFormat.Bold = true;
            paramTextFormat.FontSize = 14;
            paramTextFormat.FontName = "Times New Roman";

            CharacterFormat paramDescriptionFormat = new CharacterFormat(doc);
            paramDescriptionFormat.FontSize = 14;
            paramDescriptionFormat.FontName = "Times New Roman";

            //Add subsection paragraphs
            Paragraph headerPG = section.AddParagraph();
            headerPG.Format.BeforeSpacing = 30;
            headerPG.Format.HorizontalAlignment = HorizontalAlignment.Center;
            headerPG.AppendText(element.Text + " (" + element.Code + ")").ApplyCharacterFormat(headerFormat);
            headerPG.AppendBreak(BreakType.LineBreak);

            Paragraph descriptionPG = section.AddParagraph();
            descriptionPG.Format.FirstLineIndent = 40;
            descriptionPG.AppendText("Описание").ApplyCharacterFormat(subSectionFormat);
            descriptionPG.AppendBreak(BreakType.LineBreak);

            Paragraph parametersPG = section.AddParagraph();
            parametersPG.Format.FirstLineIndent = 40;
            parametersPG.AppendText("Параметры").ApplyCharacterFormat(subSectionFormat);

            // Add parameters paragraphs
            foreach (Parameter parameter in element.Parameters)
            {
                Paragraph paramNamePG = section.AddParagraph();
                paramNamePG.Format.LeftIndent = 40;
                paramNamePG.AppendText("#param"+parameter.Name);

                Paragraph paramTextPG = section.AddParagraph();
                paramTextPG.Format.FirstLineIndent = 40;
                paramTextPG.Format.BeforeSpacing = 10;
                paramTextPG.AppendText(parameter.Text).ApplyCharacterFormat(paramTextFormat);

                Paragraph paramDescriptionPG = section.AddParagraph();
                paramDescriptionPG.Format.BeforeSpacing = 5;
                paramDescriptionPG.Format.FirstLineIndent = 40;
                paramDescriptionPG.AppendText(parameter.Description).ApplyCharacterFormat(paramDescriptionFormat);
            }

            // Save output files into the result directory
            // Костыль с пересохранением нужен для сохранения с правильной кодировкой/параметрами, 
            // если сохранять напрямую в .rtf, то будет абракадабра.
            DirectoryInfo resultDirectory = Directory.CreateDirectory("result");
            DirectoryInfo tempDirectory = Directory.CreateDirectory("temp");
            doc.SaveToFile(tempDirectory + @"/" + filename + ".doc", FileFormat.Doc);
            doc.LoadFromFile(tempDirectory + @"/" + filename + ".doc", FileFormat.Doc);
            doc.SaveToFile(resultDirectory + @"/" + filename + ".rtf", FileFormat.Rtf);

        }
    }
}
