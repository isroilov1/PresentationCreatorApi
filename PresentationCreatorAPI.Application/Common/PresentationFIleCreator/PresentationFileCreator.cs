
using PresentationEntity = PresentationCreatorAPI.Domain.Entites.Presentation;

namespace PresentationCreatorAPI.Application.Common.Helpers;

using Aspose.Slides;
using Aspose.Slides.Export;

public class PresentationFileCreator
{
    public static void CreatePresentation(PresentationEntity presentationEntity)
    {
        // Create presentation
        using (var p = new Presentation())
        {
            // Read image
            var svgContent = File.ReadAllText("image.svg");

            // Add image to image collection
            //var emfImage = p.Images.AddFromSvg(svgContent);

            var emfContent = File.ReadAllBytes("wwwroot/assets/templates/1/1.png");

            // EMF rasmni Aspose.Slides presentationsiga qo'shish
            var emfImage = p.Images.AddImage(new MemoryStream(emfContent));

            // Add image to slide
            p.Slides[0].Shapes.AddPictureFrame(ShapeType.Rectangle, 0, 0, emfImage.Width, emfImage.Height, emfImage);

            // Save presentation
            p.Save("presentation.pptx", SaveFormat.Pptx);
        }
        }
    }
