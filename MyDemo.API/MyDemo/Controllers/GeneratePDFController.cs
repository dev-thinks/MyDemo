
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;
using Spire.Pdf.Fields;
using Spire.Pdf.Actions;
using Spire.Pdf.Widget;

namespace MyDemo.Controllers;


[ApiController]
[Route("api/[controller]")]
public class GeneratePDFController : ControllerBase
{
    [HttpGet("generate")]
    public IActionResult Generate()
    {
        //Create a PdfDocument object
        PdfDocument doc = new PdfDocument();

        //Add a page
        PdfPageBase page = doc.Pages.Add();

        //Initialize x and y coordinates
        float baseX = 100;
        float baseY = 30;

        //Create two brush objects
        PdfSolidBrush brush1 = new PdfSolidBrush(new PdfRGBColor(Color.Blue));
        PdfSolidBrush brush2 = new PdfSolidBrush(new PdfRGBColor(Color.Black));

        //Create a font 
        PdfFont font = new PdfFont(PdfFontFamily.TimesRoman, 12f, PdfFontStyle.Regular);

        //Add a textbox 
        page.Canvas.DrawString("TextBox:", font, brush1, new PointF(10, baseY));
        RectangleF tbxBounds = new RectangleF(baseX, baseY, 150, 15);
        PdfTextBoxField textBox = new PdfTextBoxField(page, "textbox");
        textBox.Bounds = tbxBounds;
        textBox.Text = "Hello Coderblog";
        textBox.Font = font;
        doc.Form.Fields.Add(textBox);
        baseY += 25;

        //add two checkboxes 
        page.Canvas.DrawString("CheckBox:", font, brush1, new PointF(10, baseY));
        RectangleF checkboxBound1 = new RectangleF(baseX, baseY, 15, 15);
        PdfCheckBoxField checkBoxField1 = new PdfCheckBoxField(page, "checkbox1");
        checkBoxField1.Bounds = checkboxBound1;
        checkBoxField1.Checked = false;
        page.Canvas.DrawString("Option 1", font, brush2, new PointF(baseX + 20, baseY));

        RectangleF checkboxBound2 = new RectangleF(baseX + 70, baseY, 15, 15);
        PdfCheckBoxField checkBoxField2 = new PdfCheckBoxField(page, "checkbox2");
        checkBoxField2.Bounds = checkboxBound2;
        checkBoxField2.Checked = false;
        page.Canvas.DrawString("Option 2", font, brush2, new PointF(baseX + 90, baseY));
        doc.Form.Fields.Add(checkBoxField1);
        doc.Form.Fields.Add(checkBoxField2);
        baseY += 25;

        //Add a listbox
        page.Canvas.DrawString("ListBox:", font, brush1, new PointF(10, baseY));
        RectangleF listboxBound = new RectangleF(baseX, baseY, 150, 50);
        PdfListBoxField listBoxField = new PdfListBoxField(page, "listbox");
        listBoxField.Items.Add(new PdfListFieldItem("Item 1", "item1"));
        listBoxField.Items.Add(new PdfListFieldItem("Item 2", "item2"));
        listBoxField.Items.Add(new PdfListFieldItem("Item 3", "item3")); ;
        listBoxField.Bounds = listboxBound;
        listBoxField.Font = font;
        listBoxField.SelectedIndex = 0;
        doc.Form.Fields.Add(listBoxField);
        baseY += 60;

        //Add two radio buttons
        page.Canvas.DrawString("RadioButton:", font, brush1, new PointF(10, baseY));
        PdfRadioButtonListField radioButtonListField = new PdfRadioButtonListField(page, "radio");
        PdfRadioButtonListItem radioItem1 = new PdfRadioButtonListItem("option1");
        RectangleF radioBound1 = new RectangleF(baseX, baseY, 15, 15);
        radioItem1.Bounds = radioBound1;
        page.Canvas.DrawString("Option 1", font, brush2, new PointF(baseX + 20, baseY));

        PdfRadioButtonListItem radioItem2 = new PdfRadioButtonListItem("option2");
        RectangleF radioBound2 = new RectangleF(baseX + 70, baseY, 15, 15);
        radioItem2.Bounds = radioBound2;
        page.Canvas.DrawString("Option 2", font, brush2, new PointF(baseX + 90, baseY));
        radioButtonListField.Items.Add(radioItem1);
        radioButtonListField.Items.Add(radioItem2);
        radioButtonListField.SelectedIndex = 0;
        doc.Form.Fields.Add(radioButtonListField);
        baseY += 25;

        //Add a combobox
        page.Canvas.DrawString("ComboBox:", font, brush1, new PointF(10, baseY));
        RectangleF cmbBounds = new RectangleF(baseX, baseY, 150, 15);
        PdfComboBoxField comboBoxField = new PdfComboBoxField(page, "combobox");
        comboBoxField.Bounds = cmbBounds;
        comboBoxField.Items.Add(new PdfListFieldItem("Item 1", "item1"));
        comboBoxField.Items.Add(new PdfListFieldItem("Item 2", "itme2"));
        comboBoxField.Items.Add(new PdfListFieldItem("Item 3", "item3"));
        comboBoxField.Items.Add(new PdfListFieldItem("Item 4", "item4"));
        comboBoxField.SelectedIndex = 0;
        comboBoxField.Font = font;
        doc.Form.Fields.Add(comboBoxField);
        baseY += 25;

        //Add a signature field
        page.Canvas.DrawString("Signature Field:", font, brush1, new PointF(10, baseY));
        PdfSignatureField sgnField = new PdfSignatureField(page, "sgnField");
        RectangleF sgnBounds = new RectangleF(baseX, baseY, 150, 80);
        sgnField.Bounds = sgnBounds;
        doc.Form.Fields.Add(sgnField);
        baseY += 90;

        //Add a button
        page.Canvas.DrawString("Button:", font, brush1, new PointF(10, baseY));
        RectangleF btnBounds = new RectangleF(baseX, baseY, 50, 15);
        PdfButtonField buttonField = new PdfButtonField(page, "button");
        buttonField.Bounds = btnBounds;
        buttonField.Text = "Submit";
        buttonField.Font = font;
        PdfSubmitAction submitAction = new PdfSubmitAction("https://www.e-iceblue.com/getformvalues.php");
        submitAction.DataFormat = SubmitDataFormat.Html;
        buttonField.Actions.MouseDown = submitAction;
        doc.Form.Fields.Add(buttonField);

        var pdfFile = "PDF/FillableForms.pdf";
        //Save to file
        doc.SaveToFile(pdfFile, FileFormat.PDF);

        byte[] pdfBytes = System.IO.File.ReadAllBytes(pdfFile);
        MemoryStream stream = new MemoryStream(pdfBytes);

        //Delete the file after download 
        System.IO.File.Delete(pdfFile);

        return new FileStreamResult(stream, "application/pdf");
    }
}