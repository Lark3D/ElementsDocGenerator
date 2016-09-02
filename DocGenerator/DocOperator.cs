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

            ParagraphStyle header1Style = new ParagraphStyle(doc);
            header1Style.Name = "Heading 1";
            header1Style.CharacterFormat.FontName = @"Times New Roman";
            header1Style.CharacterFormat.FontSize = 16;
            header1Style.CharacterFormat.Bold = true;
            header1Style.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            header1Style.ParagraphFormat.BeforeSpacing = 30;
            doc.Styles.Add(header1Style);

            ParagraphStyle header2Style = new ParagraphStyle(doc);
            header2Style.Name = "Heading 2";
            header2Style.CharacterFormat.FontName = @"Times New Roman";
            header2Style.CharacterFormat.FontSize = 16;
            header2Style.CharacterFormat.Italic = true;
            header2Style.ParagraphFormat.LeftIndent = 36;
            header2Style.ParagraphFormat.BeforeSpacing = 0;
            doc.Styles.Add(header2Style);

            ParagraphStyle header3Style = new ParagraphStyle(doc);
            header3Style.Name = "Heading 3";
            header3Style.CharacterFormat.FontName = @"Times New Roman";
            header3Style.CharacterFormat.FontSize = 14;
            header3Style.CharacterFormat.Bold = true;
            header3Style.ParagraphFormat.LeftIndent = 36;
            header3Style.ParagraphFormat.BeforeSpacing = 12;
            doc.Styles.Add(header3Style);

            ParagraphStyle body1Style = new ParagraphStyle(doc);
            body1Style.Name = "Body Text";
            body1Style.CharacterFormat.FontName = @"Times New Roman";
            body1Style.CharacterFormat.FontSize = 14;
            body1Style.ParagraphFormat.LeftIndent = 36;
            body1Style.ParagraphFormat.BeforeSpacing = 8;
            doc.Styles.Add(body1Style);


            //Add subsection paragraphs
            Paragraph headerPG = section.AddParagraph();
            headerPG.ApplyStyle("Heading 1");
            headerPG.AppendText(element.Text + " (" + element.Code + ")").ApplyCharacterFormat(headerFormat);
            headerPG.AppendBreak(BreakType.LineBreak);
            headerPG.ApplyStyle(header1Style.Name);

            Paragraph descriptionPG = section.AddParagraph();
            descriptionPG.ApplyStyle("Heading 2");
            descriptionPG.AppendText("Описание").ApplyCharacterFormat(subSectionFormat);
            descriptionPG.AppendBreak(BreakType.LineBreak);

            Paragraph parametersPG = section.AddParagraph();
            parametersPG.ApplyStyle("Heading 2");
            parametersPG.AppendText("Параметры").ApplyCharacterFormat(subSectionFormat);

            // Add parameters paragraphs
            foreach (Parameter parameter in element.Parameters)
            {
                Paragraph paramNamePG = section.AddParagraph();
                paramNamePG.ApplyStyle("Body Text");
                paramNamePG.AppendText("#param"+parameter.Name);

                Paragraph paramTextPG = section.AddParagraph();
                paramTextPG.ApplyStyle("Heading 3");
                // Getting rid of units of measurement
                string[] paramTextSplit = parameter.Text.Split(',');
                string paramTextText = parameter.Text.Replace("," + paramTextSplit[paramTextSplit.Length - 1], "");
                paramTextPG.AppendText(paramTextText).ApplyCharacterFormat(paramTextFormat);

                Paragraph paramDescriptionPG = section.AddParagraph();
                paramDescriptionPG.ApplyStyle("Body Text");
                // Getting rid of units of measurement
                string[] paramDescriptionSplit = parameter.Description.Split(',');
                string paramDescriptionText = parameter.Description.Replace("," + paramDescriptionSplit[paramDescriptionSplit.Length - 1], "");
                paramDescriptionPG.AppendText(paramDescriptionText).ApplyCharacterFormat(paramDescriptionFormat);
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
