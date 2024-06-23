
using PresentationEntity = PresentationCreatorAPI.Domain.Entites.Presentation;

namespace PresentationCreatorAPI.Application.Common.Helpers;

using Aspose.Slides;
using Aspose.Slides.Export;
using OpenXmlPowerTools;

public class PresentationFileCreator
{
    public static void CreatePresentation(PresentationEntity presentationEntity)
    {
        // Создание экземпляра объекта Presentation, представляющего файл презентации
        using (Presentation presentation = new Presentation())
        {
            // Получить первый слайд
            ISlide slide = presentation.Slides[0];

            // Добавить содержимое на слайд...
            // Получить первый слайд
            ISlide sld = presentation.Slides[0];

            // Добавьте автофигуру прямоугольного типа
            IAutoShape ashp = sld.Shapes.AddAutoShape(ShapeType.AlternateProcessFlow, 160, 75, 150, 50);

            // Добавьте TextFrame в прямоугольник
            ashp.AddTextFrame("Salomatlik");

            // Доступ к текстовому фрейму
            ITextFrame txtFrame = ashp.TextFrame;

            // Создайте объект Paragraph для текстового фрейма
            IParagraph para = txtFrame.Paragraphs[0];

            // Создать объект части для абзаца
            IPortion portion = para.Portions[0];

            // Установить текст
            portion.Text = "Aspose TextBox";



            // Читать изображение
            // EMF rasm faylini o'qish
            var emfContent = File.ReadAllBytes("wwwroot/assets/templates/1/1.png");

            // EMF rasmni Aspose.Slides presentationsiga qo'shish
            var emfImage = presentation.Images.AddImage(new MemoryStream(emfContent));

            // Добавить изображение на слайд
            presentation.Slides[0].Shapes.AddPictureFrame(ShapeType.Rectangle, 0, 0, 16, 9, emfImage);


            // Сохранить презентацию
            presentation.Save("uploads/presentations/1/NewPresentation.pptx", SaveFormat.Pptx);
        }
    }
}
